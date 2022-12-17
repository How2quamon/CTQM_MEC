using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CTQM_Car.Data;
using CTQM_MEC.Data;

namespace CTQM_MEC.Controllers
{
    public class GiaoDichesController : Controller
    {
        private readonly CTQMDbContext _context;

        public GiaoDichesController(CTQMDbContext context)
        {
            _context = context;
        }

        // GET: GiaoDiches
        public async Task<IActionResult> Index()
        {
            var cTQMDbContext = _context.Giaodichs.Include(g => g.KhachHang).Include(g => g.Xe);
            return View(await cTQMDbContext.ToListAsync());
        }

        // GET: GiaoDiches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Giaodichs == null)
            {
                return NotFound();
            }

            var giaoDich = await _context.Giaodichs
                .Include(g => g.KhachHang)
                .Include(g => g.Xe)
                .FirstOrDefaultAsync(m => m.MaGiaoDich == id);
            if (giaoDich == null)
            {
                return NotFound();
            }

            return View(giaoDich);
        }

        // GET: GiaoDiches/Create
        public IActionResult Create()
        {
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhachHang", "GiayPhepLaiXe");
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe");
            return View();
        }

        // POST: GiaoDiches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGiaoDich,MaKhachHang,MaXe,SoLuongMua,TongTien")] GiaoDich giaoDich)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaoDich);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhachHang", "GiayPhepLaiXe", giaoDich.MaKhachHang);
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe", giaoDich.MaXe);
            return View(giaoDich);
        }

        // GET: GiaoDiches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Giaodichs == null)
            {
                return NotFound();
            }

            var giaoDich = await _context.Giaodichs.FindAsync(id);
            if (giaoDich == null)
            {
                return NotFound();
            }
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhachHang", "GiayPhepLaiXe", giaoDich.MaKhachHang);
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe", giaoDich.MaXe);
            return View(giaoDich);
        }

        // POST: GiaoDiches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaGiaoDich,MaKhachHang,MaXe,SoLuongMua,TongTien")] GiaoDich giaoDich)
        {
            if (id != giaoDich.MaGiaoDich)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaoDich);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaoDichExists(giaoDich.MaGiaoDich))
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
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhachHang", "GiayPhepLaiXe", giaoDich.MaKhachHang);
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe", giaoDich.MaXe);
            return View(giaoDich);
        }

        // GET: GiaoDiches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Giaodichs == null)
            {
                return NotFound();
            }

            var giaoDich = await _context.Giaodichs
                .Include(g => g.KhachHang)
                .Include(g => g.Xe)
                .FirstOrDefaultAsync(m => m.MaGiaoDich == id);
            if (giaoDich == null)
            {
                return NotFound();
            }

            return View(giaoDich);
        }

        // POST: GiaoDiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Giaodichs == null)
            {
                return Problem("Entity set 'CTQMDbContext.Giaodichs'  is null.");
            }
            var giaoDich = await _context.Giaodichs.FindAsync(id);
            if (giaoDich != null)
            {
                _context.Giaodichs.Remove(giaoDich);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaoDichExists(int id)
        {
          return _context.Giaodichs.Any(e => e.MaGiaoDich == id);
        }
    }
}
