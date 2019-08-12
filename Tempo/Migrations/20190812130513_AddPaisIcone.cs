using Microsoft.EntityFrameworkCore.Migrations;

namespace Tempo.Migrations
{
    public partial class AddPaisIcone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Temperatura",
                table: "Cidade",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "Icone",
                table: "Cidade",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Cidade",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icone",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Cidade");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperatura",
                table: "Cidade",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
