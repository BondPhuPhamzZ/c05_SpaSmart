using System.ComponentModel.DataAnnotations;

namespace c05_SpaSmart.Models.Enums
{
    public enum TrangThaiLichHen
    {
        [Display(Name = "Chờ xác nhận")]
        ChoXacNhan = 1,
        [Display(Name = "Đã xác nhận")]
        DaXacNhan = 2,
        [Display(Name = "Đang phục vụ")]
        DangPhucVu = 3,
        [Display(Name = "Đã phục vụ xong")]
        DaPhucVuXong = 4,
        [Display(Name = "Đã thanh toán")]
        DaThanhToan = 5,
        [Display(Name = "Đã hủy")]
        DaHuy = 6
    }
}
