﻿using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class GenderCreateDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
