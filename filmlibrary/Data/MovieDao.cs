using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace FilmLibrary.Data
{
    public static class MovieDao
    {
        public static async Task<MovieDetails?> GetMovieDetailsAsync(int movieId, int userId)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT 
                    m.id,
                    m.title,
                    m.year,
                    m.synopsis,
                    m.poster_path,
                    m.trailer_youtube_id,
                    GROUP_CONCAT(DISTINCT g.name ORDER BY g.name SEPARATOR ', ') AS genres,
                    ROUND(AVG(r.rating), 2) AS avg_rating,
                    ur.rating AS my_rating,
                    ums.status AS my_status
                FROM movies m
                LEFT JOIN movie_genres mg ON mg.movie_id = m.id
                LEFT JOIN genres g ON g.id = mg.genre_id
                LEFT JOIN ratings r ON r.movie_id = m.id
                LEFT JOIN ratings ur ON ur.movie_id = m.id AND ur.user_id = @uid
                LEFT JOIN user_movie_status ums ON ums.movie_id = m.id AND ums.user_id = @uid
                WHERE m.id = @mid
                GROUP BY m.id, m.title, m.year, m.synopsis, m.poster_path, m.trailer_youtube_id, ur.rating, ums.status;
            ";
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@uid", userId);

            await using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                return new MovieDetails
                {
                    Id = rd.GetInt32(0),
                    Title = rd.GetString(1),
                    Year = rd.IsDBNull(2) ? null : rd.GetInt16(2),
                    Synopsis = rd.IsDBNull(3) ? "" : rd.GetString(3),
                    PosterPath = rd.IsDBNull(4) ? null : rd.GetString(4),
                    TrailerId = rd.IsDBNull(5) ? null : rd.GetString(5),
                    GenresCsv = rd.IsDBNull(6) ? "" : rd.GetString(6),
                    AvgRating = rd.IsDBNull(7) ? (double?)null : rd.GetDouble(7),
                    MyRating = rd.IsDBNull(8) ? (int?)null : rd.GetInt32(8),
                    MyStatus = rd.IsDBNull(9) ? null : rd.GetString(9)
                };
            }
            return null;
        }

        public static async Task<List<CommentItem>> GetCommentsAsync(int movieId)
        {
            var list = new List<CommentItem>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT c.text, u.username, c.created_at
                FROM comments c
                JOIN users u ON u.id = c.user_id
                WHERE c.movie_id = @mid
                ORDER BY c.created_at DESC;
            ";
            cmd.Parameters.AddWithValue("@mid", movieId);

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new CommentItem
                {
                    Text = rd.GetString(0),
                    UserName = rd.GetString(1),
                    CreatedAt = rd.GetDateTime(2)
                });
            }
            return list;
        }


        //to search by g,t
        public static async Task<List<MovieItem>> SearchMoviesAsync(string searchTerm, string? genre, string? status, int userId)
        {
            var list = new List<MovieItem>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
        SELECT DISTINCT m.id, m.title, m.year, m.poster_path
        FROM movies m
        LEFT JOIN movie_genres mg ON mg.movie_id = m.id
        LEFT JOIN genres g ON g.id = mg.genre_id
        LEFT JOIN user_movie_status ums ON ums.movie_id = m.id AND ums.user_id = @uid
        WHERE (@title IS NULL OR m.title LIKE @title)
          AND (@genre IS NULL OR g.name = @genre)
          AND (@status IS NULL OR ums.status = @status)
        ORDER BY m.title ASC;
    ";

            // parameters
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@title", string.IsNullOrWhiteSpace(searchTerm) ? DBNull.Value : $"%{searchTerm}%");
            cmd.Parameters.AddWithValue("@genre", string.IsNullOrWhiteSpace(genre) ? DBNull.Value : genre);
            cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(status) ? DBNull.Value : status);

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new MovieItem
                {
                    Id = rd.GetInt32(0),
                    Title = rd.GetString(1),
                    Year = rd.IsDBNull(2) ? (int?)null : rd.GetInt16(2),
                    PosterPath = rd.IsDBNull(3) ? null : rd.GetString(3)
                });
            }

            return list;
        }


        public static async Task AddCommentAsync(int userId, int movieId, string text)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO comments (user_id, movie_id, text, created_at) VALUES (@uid, @mid, @txt, NOW());";
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@txt", text);

            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task UpsertRatingAsync(int userId, int movieId, int rating)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO ratings (user_id, movie_id, rating) 
                VALUES (@uid, @mid, @rat)
                ON DUPLICATE KEY UPDATE rating = @rat;
            ";
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@rat", rating);

            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task SetUserStatusAsync(int userId, int movieId, string status)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO user_movie_status (user_id, movie_id, status)
                VALUES (@uid, @mid, @stat)
                ON DUPLICATE KEY UPDATE status = @stat;
            ";
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@mid", movieId);
            cmd.Parameters.AddWithValue("@stat", status);

            await cmd.ExecuteNonQueryAsync();
        }

        
        public static async Task<List<MovieItem>> SearchMoviesAsync(string searchTerm)
        {
            var list = new List<MovieItem>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT id, title, year, poster_path
                FROM movies
                WHERE title LIKE @term
                ORDER BY title ASC;
            ";
            cmd.Parameters.AddWithValue("@term", "%" + searchTerm + "%");

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new MovieItem
                {
                    Id = rd.GetInt32(0),
                    Title = rd.GetString(1),
                    Year = rd.IsDBNull(2) ? (int?)null : rd.GetInt16(2),
                    PosterPath = rd.IsDBNull(3) ? null : rd.GetString(3)
                });
            }
            return list;
        }



        
        public static async Task<List<GenreItem>> GetGenresAsync()
        {
            var list = new List<GenreItem>();
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name FROM genres ORDER BY name ASC;";

            await using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new GenreItem
                {
                    Id = rd.GetInt32(0),
                    Name = rd.GetString(1)
                });
            }
            return list;
        }


        // Helper classes
        public class MovieDetails
        {
            public int Id { get; set; }
            public string Title { get; set; } = "";
            public int? Year { get; set; }
            public string Synopsis { get; set; } = "";
            public string? PosterPath { get; set; }
            public string? TrailerId { get; set; }
            public string GenresCsv { get; set; } = "";
            public double? AvgRating { get; set; }
            public int? MyRating { get; set; }
            public string? MyStatus { get; set; }
        }

        public class CommentItem
        {
            public string Text { get; set; } = "";
            public string UserName { get; set; } = "";
            public DateTime CreatedAt { get; set; }
            public override string ToString() => $"{UserName} ({CreatedAt:g}): {Text}";
        }

        public class MovieItem
        {
            public int Id { get; set; }
            public string Title { get; set; } = "";
            public int? Year { get; set; }
            public string? PosterPath { get; set; }
        }


      
        public class GenreItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }


    }
}

