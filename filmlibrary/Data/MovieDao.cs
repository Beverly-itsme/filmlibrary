using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary.Data
{
    public class MovieDetailsDto
    {
        public string Title { get; set; } = "";
        public int? Year { get; set; }
        public string Synopsis { get; set; } = "";
        public string? PosterPath { get; set; }
        public string Genres { get; set; } = "";
        public double? AvgRating { get; set; }
        public string? MyStatus { get; set; }
        public int? MyRating { get; set; }
    }

    public static class MovieDao
    {
        public static async Task<List<Genre>> GetGenresAsync()
        {
            var result = new List<Genre>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name FROM genres ORDER BY name;";
            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                result.Add(new Genre
                {
                    Id = rd.GetInt32(0),
                    Name = rd.GetString(1)
                });
            }
            return result;
        }

        /// <summary>
        /// List/Search movies with optional filters: title (contains), genreId (exact), status for a specific user.
        /// </summary>
        public static async Task<List<MovieListItem>> SearchMoviesAsync(
            int currentUserId,
            string? titleContains = null,
            int? genreId = null,
            string? status = null // want_to_watch | watching | watched
        )
        {
            var list = new List<MovieListItem>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var sb = new StringBuilder(@"
                SELECT 
                    m.id,
                    m.title,
                    m.year,
                    GROUP_CONCAT(DISTINCT g.name ORDER BY g.name SEPARATOR ', ') AS genres,
                    ROUND(AVG(r.rating), 2) AS avg_rating,
                    ums.status AS user_status
                FROM movies m
                LEFT JOIN movie_genres mg ON mg.movie_id = m.id
                LEFT JOIN genres g ON g.id = mg.genre_id
                LEFT JOIN ratings r ON r.movie_id = m.id
                LEFT JOIN user_movie_status ums 
                    ON ums.movie_id = m.id AND ums.user_id = @uid
                WHERE 1=1
            ");

            var cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@uid", currentUserId);

            if (!string.IsNullOrWhiteSpace(titleContains))
            {
                sb.Append(" AND m.title LIKE @title ");
                cmd.Parameters.AddWithValue("@title", "%" + titleContains + "%");
            }

            if (genreId.HasValue)
            {
                sb.Append(" AND EXISTS (SELECT 1 FROM movie_genres x WHERE x.movie_id = m.id AND x.genre_id = @gid) ");
                cmd.Parameters.AddWithValue("@gid", genreId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sb.Append(" AND ums.status = @stat ");
                cmd.Parameters.AddWithValue("@stat", status);
            }

            sb.Append(@"
                GROUP BY m.id, m.title, m.year, ums.status
                ORDER BY m.title ASC;
            ");

            cmd.CommandText = sb.ToString();

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new MovieListItem
                {
                    Id = rd.GetInt32(0),
                    Title = rd.GetString(1),
                    Year = rd.IsDBNull(2) ? (int?)null : rd.GetInt16(2),
                    GenresCsv = rd.IsDBNull(3) ? "" : rd.GetString(3),
                    AvgRating = rd.IsDBNull(4) ? (double?)null : Convert.ToDouble(rd.GetValue(4)),
                    UserStatus = rd.IsDBNull(5) ? null : rd.GetString(5)
                });
            }
            return list;
        }

        public static async Task<MovieDetailsDto?> GetMovieDetailsAsync(int movieId, int userId)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var sql = @"
                SELECT m.title, m.year, m.synopsis, m.poster_path,
                       GROUP_CONCAT(DISTINCT g.name ORDER BY g.name SEPARATOR ', ') AS genres,
                       ROUND(AVG(r.rating), 1) AS avg_rating,
                       ums.status AS my_status,
                       ur.rating AS my_rating
                FROM movies m
                LEFT JOIN movie_genres mg ON m.id = mg.movie_id
                LEFT JOIN genres g ON mg.genre_id = g.id
                LEFT JOIN ratings r ON m.id = r.movie_id
                LEFT JOIN user_movie_status ums 
                    ON m.id = ums.movie_id AND ums.user_id = @uid
                LEFT JOIN ratings ur
                    ON m.id = ur.movie_id AND ur.user_id = @uid
                WHERE m.id = @mid
                GROUP BY m.id, ums.status, ur.rating;
            ";

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@uid", userId);

            await using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                return new MovieDetailsDto
                {
                    Title = rd.GetString("title"),
                    Year = rd.IsDBNull("year") ? null : rd.GetInt16("year"),
                    Synopsis = rd.IsDBNull("synopsis") ? "" : rd.GetString("synopsis"),
                    PosterPath = rd.IsDBNull("poster_path") ? null : rd.GetString("poster_path"),
                    Genres = rd.IsDBNull("genres") ? "" : rd.GetString("genres"),
                    AvgRating = rd.IsDBNull("avg_rating") ? null : rd.GetDouble("avg_rating"),
                    MyStatus = rd.IsDBNull("my_status") ? null : rd.GetString("my_status"),
                    MyRating = rd.IsDBNull("my_rating") ? null : rd.GetInt32("my_rating")
                };
            }
            return null;
        }

        public static async Task UpsertRatingAsync(int userId, int movieId, int rating)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            var sql = @"
                INSERT INTO ratings (user_id, movie_id, rating)
                VALUES (@uid, @mid, @rate)
                ON DUPLICATE KEY UPDATE rating = @rate;
            ";

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@rate", rating);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task AddCommentAsync(int userId, int movieId, string text)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            var sql = @"
                INSERT INTO comments (user_id, movie_id, comment_text, created_at)
                VALUES (@uid, @mid, @txt, NOW());
            ";

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@txt", text);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<List<string>> GetCommentsAsync(int movieId)
        {
            var list = new List<string>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            var sql = @"
                SELECT CONCAT(u.username, ': ', c.comment_text) AS display
                FROM comments c
                JOIN users u ON c.user_id = u.id
                WHERE c.movie_id = @mid
                ORDER BY c.created_at DESC;
            ";

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@mid", movieId);

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(rd.GetString("display"));
            }
            return list;
        }

        public static async Task SetUserStatusAsync(int userId, int movieId, string status)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            var sql = @"
                INSERT INTO user_movie_status (user_id, movie_id, status)
                VALUES (@uid, @mid, @stat)
                ON DUPLICATE KEY UPDATE status = @stat;
            ";

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@stat", status);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<List<string>> GetWatchLinksAsync(int movieId)
        {
            var list = new List<string>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            var sql = "SELECT url FROM watch_links WHERE movie_id = @mid;";

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@mid", movieId);

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(rd.GetString("url"));
            }
            return list;
        }
    }
}
