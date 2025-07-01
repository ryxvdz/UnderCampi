// RockCampinas.Api/Models/NoticiaShow.cs
using System;
using System.Collections.Generic; // Para ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockCampinas.Api.Models
{
    public class NoticiaShow
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Titulo { get; set; } // Adicione 'required' aqui

        [Required]
        public required string Descricao { get; set; } // Adicione 'required' aqui

        public DateTime DataShow { get; set; }

        [MaxLength(255)]
        public string? Local { get; set; } // Já estava anulável, ok

        [MaxLength(255)]
        public string? Endereco { get; set; }

        [MaxLength(255)]
        public string? Bandas { get; set; }

        [Column(TypeName = "numeric(10, 2)")]
        public decimal? PrecoIngresso { get; set; }

        [MaxLength(500)]
        public string? LinkIngressos { get; set; }

        [MaxLength(500)]
        public string? UrlImagemCapa { get; set; }

        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;

        public bool Ativo { get; set; } = true;

        public int AutorId { get; set; }
       public Usuario? Autor { get; set; }
    }
}