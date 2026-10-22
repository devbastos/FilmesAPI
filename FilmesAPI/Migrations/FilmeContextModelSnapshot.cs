using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FilmesAPI.Migrations;

[DbContext(typeof(FilmeContext))]
public sealed class FilmeContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity("FilmesAPI.Models.Filme", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd();
            entity.Property<string>("Titulo").IsRequired().HasMaxLength(200);
            entity.Property<string>("Diretor").IsRequired().HasMaxLength(200);
            entity.Property<string>("Genero").HasMaxLength(100);
            entity.Property<int>("Duracao");
            entity.HasKey("Id");
            entity.ToTable("Filmes");
        });
    }
}