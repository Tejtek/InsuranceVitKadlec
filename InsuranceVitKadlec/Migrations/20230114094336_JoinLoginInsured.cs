using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceVitKadlec.Migrations
{
    public partial class JoinLoginInsured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Insured",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Insured");
        }
    }
}
