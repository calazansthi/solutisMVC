using Microsoft.EntityFrameworkCore.Migrations;

namespace Tempo.Migrations
{
    public partial class Modelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cidade",
                table: "Cidade");

            migrationBuilder.RenameTable(
                name: "Cidade",
                newName: "Cidades");

            migrationBuilder.AddColumn<string>(
                name: "Condicao",
                table: "Cidades",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cidades",
                table: "Cidades",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cidades",
                table: "Cidades");

            migrationBuilder.DropColumn(
                name: "Condicao",
                table: "Cidades");

            migrationBuilder.RenameTable(
                name: "Cidades",
                newName: "Cidade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cidade",
                table: "Cidade",
                column: "Id");
        }
    }
}
