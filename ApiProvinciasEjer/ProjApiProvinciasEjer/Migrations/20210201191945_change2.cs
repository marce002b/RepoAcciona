using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjApiProvinciasEjer.Migrations
{
    public partial class change2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipios_Provincias_ProvinciaId",
                table: "Municipios");

            migrationBuilder.DropIndex(
                name: "IX_Municipios_ProvinciaId",
                table: "Municipios");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Provincias",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "ProvinciaId1",
                table: "Municipios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_ProvinciaId1",
                table: "Municipios",
                column: "ProvinciaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipios_Provincias_ProvinciaId1",
                table: "Municipios",
                column: "ProvinciaId1",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipios_Provincias_ProvinciaId1",
                table: "Municipios");

            migrationBuilder.DropIndex(
                name: "IX_Municipios_ProvinciaId1",
                table: "Municipios");

            migrationBuilder.DropColumn(
                name: "ProvinciaId1",
                table: "Municipios");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Provincias",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_ProvinciaId",
                table: "Municipios",
                column: "ProvinciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipios_Provincias_ProvinciaId",
                table: "Municipios",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
