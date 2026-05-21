using Microsoft.AspNetCore.Mvc;
using PerdeCim.Models;
using System.Linq;
using Microsoft.AspNetCore.Http; // Hata almanı engelleyen kütüphane

namespace PerdeCim.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        // --- GİRİŞ İŞLEMLERİ ---
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(int SaticiId, string SaticiAdi)
        {
            var satici = _context.Saticilar.FirstOrDefault(s => 
                s.SaticiId == SaticiId && 
                s.SaticiAdi.ToLower() == SaticiAdi.ToLower().Trim());

            if (satici != null)
            {
                HttpContext.Session.SetString("saticiAdi", satici.SaticiAdi ?? "Bilinmiyor");
                HttpContext.Session.SetInt32("saticiId", satici.SaticiId);
                return RedirectToAction("Index", "Home"); 
            }

            ViewBag.Hata = "Girdiğiniz ID veya Satıcı Adı eşleşmiyor!";
            return View();
        }

        // --- KAYIT İŞLEMLERİ (404 hatasını çözen kısım) ---
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Satici satici)
        {
            if (satici != null)
            {
                _context.Saticilar.Add(satici);
                _context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(satici);
        }

        // --- ÇIKIŞ İŞLEMİ ---
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}