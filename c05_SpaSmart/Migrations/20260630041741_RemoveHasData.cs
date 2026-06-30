using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace c05_SpaSmart.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GoiDichVus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GoiDichVus",
                columns: new[] { "Id", "GiaTien", "MoTa", "TenDichVu", "ThoiLuongPhut" },
                values: new object[] { 1, 500000m, "Thư giãn toàn thân", "Massage Body 60 phút", 60 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DiemTichLuy", "FullName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[,]
                {
                    { 1, 0, "Quản trị viên", "$2a$11$6hdJLGS4ZkGwolEx7ss3N.uAVf43jkLYcJvO2hPbNqtdy8SvaDVuu", "0123456789", 1, "admin" },
                    { 2, 100, "Nguyễn Văn Khách", "$2a$11$.JnD9k/UGiBUHHHiO9GuEuzdWHwkqIZ2n.1tISyTijxuRAPNfSU3K", "0987654321", 2, "khachhang" }
                });
        }
    }
}
