using MySqlConnector;
using BCrypt.Net;


namespace FilmLibrary.AdminSeeder
{
    internal class Program
    {
        
        private const string AdminUsername = "admin";
        private const string AdminEmail = "admin@example.com";
        private const string AdminPlainPassword = "123456"; 

        private static async Task Main()
        {
           
            var cs = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3307,
                Database = "movie_db",
                UserID = "Shelcia",
                Password = "1234",
                SslMode = MySqlSslMode.None
            }.ToString();

            
            string hash = BCrypt.Net.BCrypt.HashPassword(AdminPlainPassword);

            try
            {
                await using var conn = new MySqlConnection(cs);
                await conn.OpenAsync();

                
                await using (var checkCmd = conn.CreateCommand())
                {
                    checkCmd.CommandText = @"SELECT COUNT(*) FROM users WHERE username=@u OR email=@e;";
                    checkCmd.Parameters.AddWithValue("@u", AdminUsername);
                    checkCmd.Parameters.AddWithValue("@e", AdminEmail);

                    var count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());
                    if (count > 0)
                    {
                        Console.WriteLine("Admin user already exists. Updating password & role to admin...");

                        await using var upd = conn.CreateCommand();
                        upd.CommandText = @"
                            UPDATE users 
                            SET password_hash=@p, role='admin'
                            WHERE username=@u OR email=@e;";
                        upd.Parameters.AddWithValue("@p", hash);
                        upd.Parameters.AddWithValue("@u", AdminUsername);
                        upd.Parameters.AddWithValue("@e", AdminEmail);

                        int rows = await upd.ExecuteNonQueryAsync();
                        Console.WriteLine(rows > 0
                            ? "Admin updated successfully."
                            : "No rows updated (unexpected).");
                        return;
                    }
                }

                //admin
                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO users (username, email, password_hash, role)
                        VALUES (@u, @e, @p, 'admin');";
                    cmd.Parameters.AddWithValue("@u", AdminUsername);
                    cmd.Parameters.AddWithValue("@e", AdminEmail);
                    cmd.Parameters.AddWithValue("@p", hash);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine(rows == 1
                        ? "Admin user inserted with BCrypt password!"
                        : "Insert did not affect 1 row (unexpected).");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
