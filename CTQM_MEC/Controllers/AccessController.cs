using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CTQM_Car.Data;
using CTQM_MEC.Data;
using CTQM_MEC.Models;

namespace CTQM_MEC.Controllers
{
    public class AccessController : Controller
    {
        private readonly CTQMDbContext _context;
        public AccessController(CTQMDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(VMLogin modelLogin)
		{
            var CKh = await _context.KhachHangs.ToListAsync();
            for (int i = 0; i < CKh.Count; i++)
            {
                if (CKh[i].Email == modelLogin.Email && CKh[i].Password == modelLogin.Password)
                {
                    ViewData["UserLoginName"] = CKh[i].TenKhachHang;
                    ViewBag.Message = modelLogin;
                    return View("2FA");
                }
            }
            return View("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            try
            {
                var CKh = await _context.KhachHangs.ToListAsync();
                VMLogin tmp = new VMLogin();
                for (int i = 0; i < CKh.Count; i++)
                {
                    if (CKh[i].Email == modelLogin.Email && CKh[i].Password == modelLogin.Password)
                    {
                        tmp.Email = CKh[i].Email;
                        tmp.Password = CKh[i].Password;
                        tmp.Name = CKh[i].TenKhachHang;
                        break;
                    }
                }
                if (tmp != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                       new Claim (ClaimTypes.NameIdentifier, tmp.Name),
                       new Claim (ClaimTypes.Role, "Customer"),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = modelLogin.KeepLogedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                };

                ViewData["ValidateMessage"] = "user not found";
                return View();
            }
            catch
            {
                ViewData["ValidateMessage"] = "user not found";
                return View();
            }
        }
    }
}
