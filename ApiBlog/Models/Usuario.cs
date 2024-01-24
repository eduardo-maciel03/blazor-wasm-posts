﻿using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
