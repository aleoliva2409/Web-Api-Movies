﻿namespace WebAPIMovies.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
