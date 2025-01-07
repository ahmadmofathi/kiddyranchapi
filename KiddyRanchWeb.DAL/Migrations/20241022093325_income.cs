using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiddyRanchWeb.DAL.Migrations
{
    /// <inheritdoc />
    public partial class income : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IncomeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IncomeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncomeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncomeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAmount = table.Column<int>(type: "int", nullable: true),
                    TotalPaymentAmount = table.Column<int>(type: "int", nullable: true),
                    RemainingAmount = table.Column<int>(type: "int", nullable: true),
                    RemainingDeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nextPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incomes");
        }
    }
}
