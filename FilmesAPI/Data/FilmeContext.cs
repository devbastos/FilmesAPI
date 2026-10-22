using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;

public sealed class FilmeContext(DbContextOptions<FilmeContext> options) : DbContext(options)
{
    public DbSet<Filme> Filmes => Set<Filme>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(f => f.Diretor).IsRequired().HasMaxLength(200);
            entity.Property(f => f.Genero).HasMaxLength(100);
        });
    }
}