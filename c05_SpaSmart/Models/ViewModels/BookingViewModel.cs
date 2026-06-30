using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace c05_SpaSmart.Models.ViewModels
{
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn ngày giờ đặt lịch")]
        [DataType(DataType.DateTime)]
        public DateTime NgayGioDat { get; set; } = DateTime.Now.AddHours(2);

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một dịch vụ")]
        public List<int> DichVuIds { get; set; } = new List<int>();

        public int? KyThuatVienId { get; set; }

        public string? GhiChu { get; set; }
    }
}
