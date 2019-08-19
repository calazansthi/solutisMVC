using Microsoft.EntityFrameworkCore.Migrations;

namespace Tempo.Migrations
{
    public partial class SimplifyCidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condicao",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "Icone",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "Temperatura",
                table: "Cidade");

            migrationBuilder.AddColumn<int>(
                name: "CidadeId",
                table: "Cidade",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CidadeId",
                table: "Cidade");

            migrationBuilder.AddColumn<string>(
                name: "Condicao",
                table: "Cidade",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icone",
                table: "Cidade",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Cidade",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Temperatura",
                table: "Cidade",
                nullable: true);
        }
    }
}
