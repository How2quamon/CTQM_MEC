using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CTQM_Car.Data;
using CTQM_MEC.Data;
using System.Security.Cryptography.X509Certificates;
using PayPal.Core;
using PayPal.v1.Payments;
using BraintreeHttp;
using Microsoft.AspNetCore.Authorization;
using CTQM_MEC.Models;
using System.ComponentModel.DataAnnotations;
using Twilio.TwiML.Voice;

namespace CTQM_MEC.Controllers
{
    public class HoaDonsController : Controller
    {
        private readonly CTQMDbContext _context;
        private readonly string _clientId;
        private readonly string _secretKey;


        public double TyGia = 23300;
        public HoaDonsController(CTQMDbContext context, IConfiguration config)
        {
            _context = context;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
        }

        public async Task<IActionResult> PaypalCheckout(int? id)
        {
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);

            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            double tong = 0;
            var GH = from x in _context.Giaodichs
                     where x.MaKhachHang == id
                     select x;
            List<GiaoDich> GHList = GH.ToList();
            for(int i = 0; i < GHList.Count; i++)
            {
		        var Car = await _context.Xe.FindAsync(GHList[i].MaXe);
                tong += Car.GiaThanh;
                itemList.Items.Add(new Item()
                {
                    Name = Car.TenXe,
                    Currency = "USD",
                    Price = Math.Round(Car.GiaThanh / TyGia, 2).ToString(),
                    Quantity = "1",
                    Sku = "sku",
                    Tax = "0"
                });
            }
            var total = Math.Round(tong/ TyGia, 2).ToString();
            #endregion
            var paypalOrderId = DateTime.Now.Ticks;
            var hostName = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostName}/hoadons/CheckoutFail",
                    ReturnUrl = $"{hostName}/hoadons/InHoaDon/{id}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            var request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }
                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/hoadons/CheckoutFail");
            }

        }

        public IActionResult CheckoutFail()
        {
            return View();
        }
        public IActionResult CheckoutSuccess()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
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
                    //Paypal
                    // Hoá đơn dựa vào mã khách hàng khớp vd 1 
                    // 2 xe A B
                    // 1 A B
                    // A 1
                    // B 1
                }
                return View("CheckoutSuccess");
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
