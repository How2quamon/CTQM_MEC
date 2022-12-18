using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CTQM_Car.Data;
using CTQM_MEC.Data;
using CTQM_MEC.Models;

namespace CTQM_MEC.Controllers
{
    public class XesController : Controller
    {
        private readonly CTQMDbContext _context;

        public XesController(CTQMDbContext context)
        {
            _context = context;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Xe.ToListAsync());
        }

        // GET: XeList
        public async Task<IActionResult> Product(string? type)
        {
            XeModelView ProductXe = new XeModelView();
            var Sxe = from x in _context.Xe
                      select x;
            if (type != null)
            {
                Sxe = Sxe.Where(s => s.HangXe.Contains(type));
                ProductXe.DanhSachXe = await Sxe.ToListAsync();
                ProductXe.type = type;
                return View(ProductXe);
            }
            ProductXe.DanhSachXe = await Sxe.ToListAsync();
            ProductXe.type = "New Arrival";
            return View(ProductXe);
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Shopping(int? id)
        {
            if (id == null || _context.Xe == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ShopModelView ShopView = new ShopModelView();
            ShopView.ListXe = await _context.Xe.ToListAsync();
            ShopView.XeDetails = xe;
            ShopView.MoreInfo = await _context.ThongTinChiTietXes.FindAsync(id);
            ShopView.SoLuongTT = xe.SoLuong;
            return View(ShopView);
        }

        public async Task<IActionResult> Search(string? searching)
        {
            if (searching == null) return NotFound();
            var Sxe = from x in _context.Xe
                      select x;
            if (!String.IsNullOrEmpty(searching))
            {
                Sxe = Sxe.Where(s => s.TenXe.Contains(searching));
                return View("Product", await Sxe.ToListAsync());
            }
            return NotFound();
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Xe == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .FirstOrDefaultAsync(m => m.MaXe == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Xes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaXe,TenXe,HangXe,PhanKhuc,LoaiDongCo,SoLuong,GiaThanh,MoTa")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(xe);
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Xe == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaXe,TenXe,HangXe,PhanKhuc,LoaiDongCo,SoLuong,GiaThanh,MoTa")] Xe xe)
        {
            if (id != xe.MaXe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.MaXe))
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
            return View(xe);
        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Xe == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .FirstOrDefaultAsync(m => m.MaXe == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Xe == null)
            {
                return Problem("Entity set 'CTQMDbContext.Xe'  is null.");
            }
            var xe = await _context.Xe.FindAsync(id);
            if (xe != null)
            {
                _context.Xe.Remove(xe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
          return _context.Xe.Any(e => e.MaXe == id);
        }
    }
}
