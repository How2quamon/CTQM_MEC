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
    public class ThongTinChiTietXesController : Controller
    {
        private readonly CTQMDbContext _context;

        public ThongTinChiTietXesController(CTQMDbContext context)
        {
            _context = context;
        }

        // GET: ThongTinChiTietXes
        public async Task<IActionResult> Index()
        {
            var cTQMDbContext = _context.ThongTinChiTietXes.Include(t => t.Xe);
            return View(await cTQMDbContext.ToListAsync());
        }

        // GET: ThongTinChiTietXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThongTinChiTietXes == null)
            {
                return NotFound();
            }

            var thongTinChiTietXe = await _context.ThongTinChiTietXes
                .Include(t => t.Xe)
                .FirstOrDefaultAsync(m => m.MaThongTin == id);
            if (thongTinChiTietXe == null)
            {
                return NotFound();
            }

            return View(thongTinChiTietXe);
        }

        // GET: ThongTinChiTietXes/Create
        public IActionResult Create()
        {
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe");
            return View();
        }

        // POST: ThongTinChiTietXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThongTin,MaXe,Head1,Title1,Head2,Title2,Head3,Title3")] ThongTinChiTietXe thongTinChiTietXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongTinChiTietXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe", thongTinChiTietXe.MaXe);
            return View(thongTinChiTietXe);
        }

        // GET: ThongTinChiTietXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThongTinChiTietXes == null)
            {
                return NotFound();
            }

            var thongTinChiTietXe = await _context.ThongTinChiTietXes.FindAsync(id);
            if (thongTinChiTietXe == null)
            {
                return NotFound();
            }
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe", thongTinChiTietXe.MaXe);
            return View(thongTinChiTietXe);
        }

        // POST: ThongTinChiTietXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThongTin,MaXe,Head1,Title1,Head2,Title2,Head3,Title3")] ThongTinChiTietXe thongTinChiTietXe)
        {
            if (id != thongTinChiTietXe.MaThongTin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongTinChiTietXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongTinChiTietXeExists(thongTinChiTietXe.MaThongTin))
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
            ViewData["MaXe"] = new SelectList(_context.Xe, "MaXe", "HangXe", thongTinChiTietXe.MaXe);
            return View(thongTinChiTietXe);
        }

        // GET: ThongTinChiTietXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThongTinChiTietXes == null)
            {
                return NotFound();
            }

            var thongTinChiTietXe = await _context.ThongTinChiTietXes
                .Include(t => t.Xe)
                .FirstOrDefaultAsync(m => m.MaThongTin == id);
            if (thongTinChiTietXe == null)
            {
                return NotFound();
            }

            return View(thongTinChiTietXe);
        }

        // POST: ThongTinChiTietXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThongTinChiTietXes == null)
            {
                return Problem("Entity set 'CTQMDbContext.ThongTinChiTietXes'  is null.");
            }
            var thongTinChiTietXe = await _context.ThongTinChiTietXes.FindAsync(id);
            if (thongTinChiTietXe != null)
            {
                _context.ThongTinChiTietXes.Remove(thongTinChiTietXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongTinChiTietXeExists(int id)
        {
          return _context.ThongTinChiTietXes.Any(e => e.MaThongTin == id);
        }
    }
}
