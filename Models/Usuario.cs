// RockCampinas.Api/Models/Usuario.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RockCampinas.Api.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Nome { get; set; } 

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; } 

        [Required]
        [MaxLength(255)]
        public required string SenhaHash { get; set; } 

        [Required]
        [MaxLength(255)]
        public required string Salt { get; set; } 

        [Required]
        [MaxLength(50)]
        public required string Role { get; set; } 

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public bool Ativo { get; set; } = true;

        public ICollection<NoticiaShow>? NoticiasPublicadas { get; set; }
    }
}