using System;

namespace FilmLibrary.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public override string ToString() => Name;
    }

    // Row used for the movie list + filters
    public class MovieListItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int? Year { get; set; }
        public string GenresCsv { get; set; } = "";   // "Action, Drama"
        public double? AvgRating { get; set; }        // 0..5
        public string? UserStatus { get; set; }       // want_to_watch/watching/watched/null
        public override string ToString() => Year.HasValue ? $"{Title} ({Year})" : Title;
    }

    // Minimal current user (you likely already have this)
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Role { get; set; } = "user";
        public string Email { get; set; } = "";
    }
}

