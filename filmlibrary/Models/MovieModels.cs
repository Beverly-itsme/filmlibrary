using System;

namespace FilmLibrary.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public override string ToString() => Name;
    }

   
    public class MovieListItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int? Year { get; set; }
        public string GenresCsv { get; set; } = "";   
        public double? AvgRating { get; set; }        
        public string? UserStatus { get; set; }       
        public override string ToString() => Year.HasValue ? $"{Title} ({Year})" : Title;
    }

    
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Role { get; set; } = "user";
        public string Email { get; set; } = "";
    }
}

