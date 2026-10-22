using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using FilmesAPI.Data;

#nullable disable

namespace FilmesAPI.Migrations;

[DbContext(typeof(FilmeContext))]
[Migration("20261022000000_InitialPomelo")]
public sealed class InitialPomelo : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Filmes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", 1),
                Titulo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                Diretor = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                Genero = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                Duracao = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Filmes", x => x.Id))
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    protected override void Down(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropTable(name: "Filmes");
}