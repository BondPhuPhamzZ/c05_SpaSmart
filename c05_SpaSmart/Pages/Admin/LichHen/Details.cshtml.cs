using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace c05_SpaSmart.Pages.Admin.LichHen
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public DetailsModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        public c05_SpaSmart.Models.LichHen LichHen { get; set; } = default!;
        public List<string> PhuLieuSuDungList { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LichHens == null)
            {
                return NotFound();
            }

            var lichhen = await _context.LichHens
                .Include(l => l.User)
                .Include(l => l.KyThuatVien)
                .Include(l => l.ChiTietLichHens)
                    .ThenInclude(c => c.GoiDichVu)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (lichhen == null)
            {
                return NotFound();
            }
            else 
            {
                LichHen = lichhen;

                // Load danh sách phụ liệu tiêu hao cho bill này để show trước khi thanh toán
                if(LichHen.TrangThai == TrangThaiLichHen.ChoXacNhan || LichHen.TrangThai == TrangThaiLichHen.DaPhucVuXong || LichHen.TrangThai == TrangThaiLichHen.DaThanhToan)
                {
                    foreach (var ct in LichHen.ChiTietLichHens)
                    {
                        var danhMuc = await _context.DanhMucPhuLieus
                            .Include(d => d.PhuLieu)
                            .Where(d => d.DichVuId == ct.DichVuId).ToListAsync();
                        foreach(var dm in danhMuc)
                        {
                            PhuLieuSuDungList.Add($"- {dm.PhuLieu.TenPhuLieu}: {dm.SoLuongTieuHao} đơn vị");
                        }
                    }
                }
            }

            ViewData["KtvRanh"] = new SelectList(await _context.KyThuatViens.Where(k => k.TrangThaiKTV == TrangThaiKTV.SanSang).ToListAsync(), "Id", "HoTen");
            
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string actionType, int? selectedKtvId)
        {
            var lichHen = await _context.LichHens
                .Include(l => l.ChiTietLichHens)
                .Include(l => l.User)
                .Include(l => l.KyThuatVien)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lichHen == null) return NotFound();

            if (actionType == "DuyetLich")
            {
                if(lichHen.KyThuatVienId == null)
                {
                    if (selectedKtvId.HasValue)
                    {
                        lichHen.KyThuatVienId = selectedKtvId.Value;
                    }
                    else
                    {
                        // Lấy ngẫu nhiên 1 KTV rảnh
                        var randomKtv = await _context.KyThuatViens.FirstOrDefaultAsync(k => k.TrangThaiKTV == TrangThaiKTV.SanSang);
                        if (randomKtv != null) lichHen.KyThuatVienId = randomKtv.Id;
                    }
                }
                lichHen.TrangThai = TrangThaiLichHen.DaXacNhan;
                TempData["SuccessMessage"] = "Duyệt lịch và gán KTV thành công (MS03).";
            }
            else if (actionType == "PhucVu")
            {
                lichHen.TrangThai = TrangThaiLichHen.DangPhucVu;
                // Set KTV bận
                if (lichHen.KyThuatVienId.HasValue)
                {
                    var ktv = await _context.KyThuatViens.FindAsync(lichHen.KyThuatVienId);
                    if (ktv != null) ktv.TrangThaiKTV = TrangThaiKTV.DangBan;
                }
            }
            else if (actionType == "Xong")
            {
                lichHen.TrangThai = TrangThaiLichHen.DaPhucVuXong;
            }
            else if (actionType == "HuyLich")
            {
                lichHen.TrangThai = TrangThaiLichHen.DaHuy;
                // Free KTV
                if (lichHen.KyThuatVienId.HasValue)
                {
                    var ktv = await _context.KyThuatViens.FindAsync(lichHen.KyThuatVienId);
                    if (ktv != null) ktv.TrangThaiKTV = TrangThaiKTV.SanSang;
                }
                TempData["SuccessMessage"] = "Hủy lịch thành công.";
            }
            else if (actionType == "ThanhToan")
            {
                // QĐ02: Xét ưu đãi (Tổng bill > 2tr giảm 10%)
                if (lichHen.TongTien >= 2000000)
                {
                    lichHen.TienGiamGia = lichHen.TongTien * 0.10m;
                }

                lichHen.TongThanhToan = lichHen.TongTien - lichHen.TienGiamGia;

                // QĐ03: Định mức phụ liệu - Trừ Kho
                foreach (var ct in lichHen.ChiTietLichHens)
                {
                    var danhMucs = await _context.DanhMucPhuLieus
                        .Include(d => d.PhuLieu)
                        .Where(d => d.DichVuId == ct.DichVuId).ToListAsync();
                    
                    foreach(var dm in danhMucs)
                    {
                        if(dm.PhuLieu.TonKho < dm.SoLuongTieuHao)
                        {
                            TempData["ErrorMessage"] = $"Lỗi: Tồn kho phụ liệu {dm.PhuLieu.TenPhuLieu} không đủ để trừ (MS06). Vui lòng kiểm tra lại.";
                            return RedirectToPage(new { id = id });
                        }
                        dm.PhuLieu.TonKho -= dm.SoLuongTieuHao;
                        _context.Entry(dm.PhuLieu).State = EntityState.Modified;
                    }
                }

                // Cập nhật điểm tích lũy khách hàng (VD: 100k = 1 điểm)
                if (lichHen.User != null)
                {
                    int diemCong = (int)(lichHen.TongThanhToan / 100000);
                    lichHen.User.DiemTichLuy += diemCong;
                }

                lichHen.TrangThai = TrangThaiLichHen.DaThanhToan;
                lichHen.NgayLapHoaDon = DateTime.Now;

                // Giải phóng KTV
                if (lichHen.KyThuatVienId.HasValue)
                {
                    var ktv = await _context.KyThuatViens.FindAsync(lichHen.KyThuatVienId);
                    if (ktv != null) ktv.TrangThaiKTV = TrangThaiKTV.SanSang;
                }

                TempData["SuccessMessage"] = "Thanh toán hoàn tất, hóa đơn đang được in ra (MS05). Đã tự động tính ưu đãi và trừ kho.";
            }

            await _context.SaveChangesAsync();
            return RedirectToPage(new { id = id });
        }
    }
}
