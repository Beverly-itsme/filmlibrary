using System.Threading.Tasks;
using MySqlConnector;

namespace FilmLibrary.Data
{
    public static class MovieAdminDao
    {
        public static async Task<int> AddMovieAsync(string title, int? year, string? synopsis, string? posterPath, string? trailerId)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO movies (title, year, synopsis, poster_path, trailer_youtube_id)
                VALUES (@t, @y, @s, @p, @tr);
                SELECT LAST_INSERT_ID();";
            cmd.Parameters.AddWithValue("@t", title);
            cmd.Parameters.AddWithValue("@y", (object?)year ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@s", (object?)synopsis ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@p", (object?)posterPath ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@tr", (object?)trailerId ?? System.DBNull.Value);
            var id = Convert.ToInt64(await cmd.ExecuteScalarAsync());
            return (int)id;
        }

        public static async Task UpdateMovieAsync(int id, string title, int? year, string? synopsis, string? posterPath, string? trailerId)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE movies
                   SET title=@t, year=@y, synopsis=@s, poster_path=@p, trailer_youtube_id=@tr
                 WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@t", title);
            cmd.Parameters.AddWithValue("@y", (object?)year ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@s", (object?)synopsis ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@p", (object?)posterPath ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@tr", (object?)trailerId ?? System.DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task DeleteMovieAsync(int id)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM movies WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}

