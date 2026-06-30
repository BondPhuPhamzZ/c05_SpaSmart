using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c05_SpaSmart.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoiDichVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiLuongPhut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoiDichVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KyThuatViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChuyenMon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThaiKTV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyThuatViens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhuLieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhuLieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HangSanXuat = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TonKho = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuLieus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DiemTichLuy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucPhuLieus",
                columns: table => new
                {
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    PhuLieuId = table.Column<int>(type: "int", nullable: false),
                    SoLuongTieuHao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucPhuLieus", x => new { x.DichVuId, x.PhuLieuId });
                    table.ForeignKey(
                        name: "FK_DanhMucPhuLieus_GoiDichVus_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "GoiDichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DanhMucPhuLieus_PhuLieus_PhuLieuId",
                        column: x => x.PhuLieuId,
                        principalTable: "PhuLieus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichHens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayGioDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienGiamGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapHoaDon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    KyThuatVienId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichHens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichHens_KyThuatViens_KyThuatVienId",
                        column: x => x.KyThuatVienId,
                        principalTable: "KyThuatViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LichHens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuChis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LyDoChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnhChungTu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuChis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuChis_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietLichHens",
                columns: table => new
                {
                    LichHenId = table.Column<int>(type: "int", nullable: false),
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    GiaTienLucDat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietLichHens", x => new { x.LichHenId, x.DichVuId });
                    table.ForeignKey(
                        name: "FK_ChiTietLichHens_GoiDichVus_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "GoiDichVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietLichHens_LichHens_LichHenId",
                        column: x => x.LichHenId,
                        principalTable: "LichHens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietLichHens_DichVuId",
                table: "ChiTietLichHens",
                column: "DichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucPhuLieus_PhuLieuId",
                table: "DanhMucPhuLieus",
                column: "PhuLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_LichHens_KyThuatVienId",
                table: "LichHens",
                column: "KyThuatVienId");

            migrationBuilder.CreateIndex(
                name: "IX_LichHens_UserId",
                table: "LichHens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuChis_UserId",
                table: "PhieuChis",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietLichHens");

            migrationBuilder.DropTable(
                name: "DanhMucPhuLieus");

            migrationBuilder.DropTable(
                name: "PhieuChis");

            migrationBuilder.DropTable(
                name: "LichHens");

            migrationBuilder.DropTable(
                name: "GoiDichVus");

            migrationBuilder.DropTable(
                name: "PhuLieus");

            migrationBuilder.DropTable(
                name: "KyThuatViens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
