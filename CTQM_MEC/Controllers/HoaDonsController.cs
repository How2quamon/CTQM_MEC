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

        // GET: HoaDons
        public async Task<IActionResult> Index()
        {
            var cTQMDbContext = _context.HoaDons.Include(h => h.GiaoDich);
            return View(await cTQMDbContext.ToListAsync());
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
                    hd.MaGiaoDich = GHList[i].MaGiaoDich;
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
                return RedirectToAction("Profile", "KhachHangs");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: HoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.GiaoDich)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDons/Create
        public IActionResult Create()
        {
            ViewData["MaGiaoDich"] = new SelectList(_context.Giaodichs, "MaGiaoDich", "MaGiaoDich");
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHoaDon,MaGiaoDich,NgayThanhToan,PhuongThucThanhToan,TongSoLuong,ThanhTien")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaGiaoDich"] = new SelectList(_context.Giaodichs, "MaGiaoDich", "MaGiaoDich", hoaDon.MaGiaoDich);
            return View(hoaDon);
        }

        // GET: HoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["MaGiaoDich"] = new SelectList(_context.Giaodichs, "MaGiaoDich", "MaGiaoDich", hoaDon.MaGiaoDich);
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHoaDon,MaGiaoDich,NgayThanhToan,PhuongThucThanhToan,TongSoLuong,ThanhTien")] HoaDon hoaDon)
        {
            if (id != hoaDon.MaHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.MaHoaDon))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaGiaoDich"] = new SelectList(_context.Giaodichs, "MaGiaoDich", "MaGiaoDich", hoaDon.MaGiaoDich);
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.GiaoDich)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
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
