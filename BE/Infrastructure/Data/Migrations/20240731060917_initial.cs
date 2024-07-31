using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DepartmentCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Mã phòng ban", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentName = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Tên phòng ban", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Người tạo", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày tạo"),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Người sửa", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày sửa"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Mô tả", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                },
                comment: "Danh sách phòng ban")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    PositionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PositionCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Mã chức vụ", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PositionName = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Tên chức vụ", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Người tạo", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày tạo"),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Người sửa", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày sửa"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Mô tả", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionId);
                },
                comment: "Danh sách chức vụ")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DepartmentId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PositionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EmployeeCode = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: false, comment: "Mã nhân viên", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Họ tên nhân viên", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true, comment: "Ngày sinh"),
                    Gender = table.Column<int>(type: "int", nullable: true, comment: "Giới tính 0-Nam, 1-Nữ, 2-Khác"),
                    IdentityNumber = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "Số CMTND, CCCD", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdentityDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Ngày cấp"),
                    IndentityPlace = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Nơi cấp", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(255)", nullable: true, comment: "Địa chỉ", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Số điện thoại", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LandlineNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Số điện thoại cố định", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Địa chỉ email", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAccount = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Tài khoản ngân hàng", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Tên ngân hàng", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Branch = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Chi nhánh", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Người tạo", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày tạo"),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "Người sửa", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày sửa"),
                    Salary = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "Lương")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Employee_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "PositionId");
                },
                comment: "Danh sách nhân viên")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "UK_Department_DepartmentCode",
                table: "Department",
                column: "DepartmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Department_DepartmentName",
                table: "Department",
                column: "DepartmentName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IDX_Employee_PositionId",
                table: "Employee",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "UK_Employee_Address",
                table: "Employee",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Employee_EmployeeCode",
                table: "Employee",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Employee_FullName",
                table: "Employee",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Employee_IdentityNumber",
                table: "Employee",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Employee_PhoneNumber",
                table: "Employee",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Position_PositionCode",
                table: "Position",
                column: "PositionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Position_PositionName",
                table: "Position",
                column: "PositionName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
