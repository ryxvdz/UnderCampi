// RockCampinas.Api/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using RockCampinas.Api.Models; // Certifique-se de que esta linha está aqui

namespace RockCampinas.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Mapeia suas classes de modelo para tabelas no banco de dados
        public DbSet<NoticiaShow> NoticiasShows { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação de 1 para muitos (Um Usuário tem muitas NoticiasShows)
            modelBuilder.Entity<NoticiaShow>()
                .HasOne(ns => ns.Autor)
                .WithMany(u => u.NoticiasPublicadas)
                .HasForeignKey(ns => ns.AutorId)
                .OnDelete(DeleteBehavior.Restrict); // Previne deletar usuário com notícias

            // Garantir que o email do usuário seja único
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}