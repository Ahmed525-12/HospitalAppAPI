using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalInfrastructure.IdentityContext.Migrations
{
    public partial class adduploadimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Accounts");
        }
    }
}
