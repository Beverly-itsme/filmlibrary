using MySqlConnector;

namespace FilmLibrary.Data
{
    public static class DbConfig
    {
        private static readonly string ConnectionString = "Server=localhost;Port=3307;Database=movie_db;Uid=Shelcia;Pwd=1234;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}

