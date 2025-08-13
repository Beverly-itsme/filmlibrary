using System.Threading.Tasks;
using MySqlConnector;
using BCrypt.Net;

namespace FilmLibrary.Data
{
    public class UserDao
    {
        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO users (username, email, password_hash, role)
                VALUES (@u, @e, @p, 'user');";
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@p", hash);

            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                return rows == 1;
            }
            catch (MySqlException ex) when (ex.Number == 1062) // Duplicate entry
            {
                return false;
            }
        }

        public async Task<User?> AuthenticateAsync(string usernameOrEmail, string password)
        {
            await using var conn = DbConfig.GetConnection();
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT id, username, email, password_hash, role 
                FROM users 
                WHERE username = @input OR email = @input
                LIMIT 1;";
            cmd.Parameters.AddWithValue("@input", usernameOrEmail);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                string storedHash = reader.GetString("password_hash");

                if (!BCrypt.Net.BCrypt.Verify(password, storedHash))
                    return null;

                return new User
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    Email = reader.GetString("email"),
                    Role = reader.GetString("role")
                };
            }

            return null;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "user";  // default role
    }
}


