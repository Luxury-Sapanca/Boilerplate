using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Data.Migrations
{
    public partial class SoftDeleteFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dummies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Dummies_IsDeleted",
                table: "Dummies",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dummies_IsDeleted",
                table: "Dummies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dummies");
        }
    }
}
