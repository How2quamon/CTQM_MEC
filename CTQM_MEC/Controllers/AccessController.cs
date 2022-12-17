using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CTQM_MEC.Data;
using CTQM_MEC.Models;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

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

        public void SendVeri(string phoneN)
        {
            if (phoneN != null)
			{
                string accountSid = "ACbd49423279a8f607d1b7e235e303629a";
                string authToken = "f6a8dc88a634dcd4a168087b29d8a3f6";
                TwilioClient.Init(accountSid, authToken);
                var verification = VerificationResource.Create(
                    to: phoneN.Trim(),
                    channel: "sms",
                    pathServiceSid: "VA26e179a04ed3bb7c3e14fccab91c873d"
                );
			}
            //Console.WriteLine(verification.Status);
        }

        public bool CheckVeri(string code, string phoneN)
        {
            if (code != null && phoneN != null)
			{
                string accountSid = "ACbd49423279a8f607d1b7e235e303629a";
                string authToken = "f6a8dc88a634dcd4a168087b29d8a3f6";

                TwilioClient.Init(accountSid, authToken);
                var verificationCheck = VerificationCheckResource.Create(
                    to: phoneN.Trim(),
                    code: code,
                    pathServiceSid: "VA26e179a04ed3bb7c3e14fccab91c873d"
                );
                if (verificationCheck.Status == "approved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
			}
            return false;
            //Console.WriteLine(verificationCheck.Status);
        }

        [HttpPost]
        public async Task<IActionResult> Check(VMLogin modelLogin)
        {
            var CKh = await _context.KhachHangs.ToListAsync();
            for (int i = 0; i < CKh.Count; i++)
            {
                if (CKh[i].Email == modelLogin.Email && CKh[i].Password == modelLogin.Password && CKh[i].SDT != null)
                {
                    ViewData["UserLoginName"] = CKh[i].TenKhachHang;
                    ViewBag.Message = modelLogin;
                    string phone = "+84" + CKh[i].SDT;
                    ViewBag.Phone = phone;
                    SendVeri(phone);
                    return View("2FA");
                }
            }
            ViewData["ValidateMessage"] = "user not found";
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin, string? smscode)
        {
            try
            {
                var CKh = await _context.KhachHangs.ToListAsync();
                VMLogin tmp = new VMLogin();
                string phone = "";
                for (int i = 0; i < CKh.Count; i++)
                {
                    if (CKh[i].Email == modelLogin.Email && CKh[i].Password == modelLogin.Password)
                    {
                        tmp.Email = CKh[i].Email;
                        tmp.Password = CKh[i].Password;
                        tmp.Name = CKh[i].TenKhachHang;
                        phone = "+84" + CKh[i].SDT;
                        break;
                    }
                }
                if (tmp != null && smscode != null && CheckVeri(smscode, phone))
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
