using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CTQM_Car.Data;
using CTQM_MEC.Data;
using Microsoft.AspNetCore.Authorization;
using CTQM_MEC.Models;

namespace CTQM_MEC.Controllers
{
    public class HoaDonsController : Controller
    {
        private readonly CTQMDbContext _context;

        public HoaDonsController(CTQMDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> ThanhToan(int? id)
        {
            CartModelView cmv = new CartModelView();
            if (id != null)
            {
                cmv.MaKhachHang = id;
                double tong = 0;
                var GH = from x in _context.Giaodichs
                         where x.MaKhachHang == id
                         select x;
                List<GiaoDich> GHList = GH.ToList();
                if (GHList.Count > 0)
                {
                    List<Xe> XeList = new List<Xe>();
                    for (int i = 0; i < GHList.Count; i++)
                    {
                        var Car = await _context.Xe.FindAsync(GHList[i].MaXe);
                        tong += Car.GiaThanh;
                        XeList.Add(Car);
                    }
                    cmv.ListXe = XeList;
                    // + 30tr tiền thuế :)
                    cmv.TongTien = tong + (10000000 * GHList.Count);
                    return View("ThanhToan", cmv);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InHoaDon(int? id)
        {
            if (id != null)
            {
                var GH = from x in _context.Giaodichs
                         where x.MaKhachHang == id
                         select x;
                List<GiaoDich> GHList = GH.ToList();
                for (int i = 0; i < GHList.Count;i++)
                {
                    HoaDon hd = new HoaDon();
                    hd.MaKhachHang = GHList[i].MaKhachHang;
                    hd.MaXe = GHList[i].MaXe;
                    hd.TongSoLuong = GHList[i].SoLuongMua;
                    hd.ThanhTien = GHList[i].TongTien + 10000000;
                    hd.NgayThanhToan = DateTime.Today;
                    hd.PhuongThucThanhToan = "PayPal";
                    _context.Add(hd);
                    var giaoDich = await _context.Giaodichs
                    .FirstOrDefaultAsync(m => m.MaGiaoDich == GHList[i].MaGiaoDich);
                    if (giaoDich != null)
                    {
                        _context.Giaodichs.Remove((GiaoDich)giaoDich);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Profile", "KhachHangs", new {id = id});
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoaDons == null)
            {
                return Problem("Entity set 'CTQMDbContext.HoaDons'  is null.");
            }
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
          return _context.HoaDons.Any(e => e.MaHoaDon == id);
        }
    }
}
