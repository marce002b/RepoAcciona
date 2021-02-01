using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjApiProvinciasEjer.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_Pcia",
                table: "Provincias",
                newName: "Nombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Provincias",
                newName: "Nombre_Pcia");
        }
    }
}
