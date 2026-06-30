using System.ComponentModel.DataAnnotations;

namespace c05_SpaSmart.Models.Enums
{
    public enum TrangThaiKTV
    {
        [Display(Name = "Sẵn sàng")]
        SanSang = 1,
        [Display(Name = "Đang bận")]
        DangBan = 2,
        [Display(Name = "Nghỉ phép")]
        NghiPhep = 3
    }
}
