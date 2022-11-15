using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BECompany.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Company_CompanyId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_CompanyId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Department");

            migrationBuilder.CreateTable(
                name: "CompanyDepartment",
                columns: table => new
                {
                    CompaniesCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentsDepartmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDepartment", x => new { x.CompaniesCompanyId, x.DepartmentsDepartmentId });
                    table.ForeignKey(
                        name: "FK_CompanyDepartment_Company_CompaniesCompanyId",
                        column: x => x.CompaniesCompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDepartment_Department_DepartmentsDepartmentId",
                        column: x => x.DepartmentsDepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartment_DepartmentsDepartmentId",
                table: "CompanyDepartment",
                column: "DepartmentsDepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDepartment");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Department",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Company_CompanyId",
                table: "Department",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");
        }
    }
}
