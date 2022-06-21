﻿using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class ReviewCreateDTO
    {
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }
    }
}
