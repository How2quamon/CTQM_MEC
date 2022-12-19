using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CTQM_MEC.Data;
using CTQM_Car.Data;
using CTQM_MEC.Models;
using Microsoft.AspNetCore.Authorization;

namespace CTQM_MEC.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly CTQMDbContext _context;

        public KhachHangsController(CTQMDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Profile(int? id)
        {
            ProfileModelView pmv = new ProfileModelView();
            if (id != null)
            {
                pmv.MaKhachHang = id;
                var khachHang = await _context.KhachHangs.FindAsync(id);
                pmv.TenKhachHang = khachHang.TenKhachHang;
                var HD = from x in _context.HoaDons
                         where x.MaKhachHang == id
                         select x;
                List<HoaDon> HDList = HD.ToList();
                List<Xe> XeList = new List<Xe>();
                for (int i = 0; i < HDList.Count; i++)
                {
                    var Car = await _context.Xe.FindAsync(HDList[i].MaXe);
                    XeList.Add(Car);
                }
                pmv.ListXe = XeList;
                return View("HoSo", pmv);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(VMLogin newKH)
        {
            if (newKH != null)
            {
                KhachHang kh = new KhachHang();
                kh.TenKhachHang = newKH.NewName;
                kh.SDT = newKH.NewPhone;
                kh.DiaChi = "";
                kh.NgaySinh = DateTime.Today;
                kh.GiayPhepLaiXe = "";
                kh.Email = newKH.NewEmail;
                kh.Password = newKH.Password;
                _context.Add(kh);
                await _context.SaveChangesAsync();
                return View("Access", "Login");
            }
            return View("Access", "Register");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileModelView pmv)
        {
            ViewData["BProfileMessage"] = "Fail to update your Profile";
            if (pmv.MaKhachHang != null && pmv != null)
            {
                var khachHang = await _context.KhachHangs.FindAsync(pmv.MaKhachHang);
                if (pmv.newName != null) khachHang.TenKhachHang = pmv.newName;
                if (pmv.newPhone != null) khachHang.SDT = pmv.newPhone;
                if (pmv.newAddress != null) khachHang.DiaChi = pmv.newAddress;
                if (pmv.newBirthDay != null) khachHang.NgaySinh = (DateTime)pmv.newBirthDay;
                if (pmv.newBangLai != null) khachHang.GiayPhepLaiXe = pmv.newBangLai;
                _context.Update(khachHang);
                await _context.SaveChangesAsync();
                ViewData["GProfileMessage"] = "Updated your Profie";
            }
            return RedirectToAction("Profile", new {id = pmv.MaKhachHang});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPass(ProfileModelView pmv)
        {
            ViewData["BPassMessage"] = "Fail to update your Password";
            if (pmv.MaKhachHang != null && pmv != null)
            {
                var khachHang = await _context.KhachHangs.FindAsync(pmv.MaKhachHang);
                if (pmv.oldPass == khachHang.Password)
                {
                    if (pmv.newPass != null) khachHang.Password = pmv.newPass;
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                    ViewData["GPassMessage"] = "Update your Password";
                }
            }
            return View("Profile", new {id = pmv.MaKhachHang });
        }

        // GET: KhachHangs
        public async Task<IActionResult> Index()
        {
              return View(await _context.KhachHangs.ToListAsync());
        }

        // GET: KhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KhachHangs == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhachHang,TenKhachHang,SDT,DiaChi,NgaySinh,GiayPhepLaiXe")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KhachHangs == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKhachHang,TenKhachHang,SDT,DiaChi,NgaySinh,GiayPhepLaiXe")] KhachHang khachHang)
        {
            if (id != khachHang.MaKhachHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.MaKhachHang))
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
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KhachHangs == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KhachHangs == null)
            {
                return Problem("Entity set 'CTQMDbContext.KhachHangs'  is null.");
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
          return _context.KhachHangs.Any(e => e.MaKhachHang == id);
        }
    }
}
