using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP24Entities.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receivables",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyCode = table.Column<string>(type: "TEXT", nullable: false),
                    IssueDate = table.Column<string>(type: "TEXT", nullable: false),
                    OpeningValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaidValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    DueDate = table.Column<string>(type: "TEXT", nullable: false),
                    ClosedDate = table.Column<string>(type: "TEXT", nullable: true),
                    Cancelled = table.Column<bool>(type: "INTEGER", nullable: true),
                    DebtorName = table.Column<string>(type: "TEXT", nullable: false),
                    DebtorReference = table.Column<string>(type: "TEXT", nullable: false),
                    DebtorAddress1 = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorAddress2 = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorTown = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorState = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorZip = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorCountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    DebtorRegistrationNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivables", x => x.Reference);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receivables");
        }
    }
}
