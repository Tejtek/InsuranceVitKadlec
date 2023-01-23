using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceVitKadlec.Migrations
{
    public partial class InsertTerminationReasonInsurendesInsurences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TerminationReason",
                table: "InsuredesInsurences",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerminationReason",
                table: "InsuredesInsurences");
        }
    }
}
