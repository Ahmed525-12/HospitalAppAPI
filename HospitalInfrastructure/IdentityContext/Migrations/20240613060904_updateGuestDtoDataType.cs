using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalInfrastructure.IdentityContext.Migrations
{
    public partial class updateGuestDtoDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IdentityCardNumber",
                table: "Guest",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdentityCardNumber",
                table: "Guest",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
