using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PerdeCim.Models;
using Microsoft.AspNetCore.Http; // Session kullanımı için gerekli

namespace PerdeCim.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly DataContext _context;

        public KullaniciController(DataContext context)
        {
            _context = context;
        }

        // Yardımcı Metot: Admin kontrolünü tek yerden yapıyoruz
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("sessionAcik") != null && 
                   HttpContext.Session.GetString("userRole") == "Admin";
        }

        // 1. KULLANICI LİSTELEME
        public ActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("login", "Login");

            var kullanicilar = _context.Users.ToList();
            return View(kullanicilar);
        }
        
        // 2. YENİ KULLANICI KAYIT
        [HttpPost]
        public ActionResult Kayit(User kullanici)
        {
            if (!IsAdmin()) return RedirectToAction("login", "Login");

            if (kullanici != null)
            {
                _context.Users.Add(kullanici);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Kullanici");
        }

        // 3. KULLANICI SİLME
        public ActionResult Sil(int id)
        {
            if (!IsAdmin()) return RedirectToAction("login", "Login");

            var silinecek = _context.Users.Find(id);
            if (silinecek != null)
            {
                _context.Users.Remove(silinecek);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Kullanici");
        }

        // 4. GÜNCELLEME SAYFASI
        public ActionResult Guncelle(int id)
        {
            if (!IsAdmin()) return RedirectToAction("login", "Login");

            var guncellenecek = _context.Users.Find(id);
            if (guncellenecek == null) 
            {
                return RedirectToAction("Index", "Kullanici");
            }
            return View(guncellenecek);
        }

        // 5. VERİTABANI GÜNCELLEME KAYDI
        [HttpPost]
        public ActionResult Guncelleme(User kullanici)
        {
            if (!IsAdmin()) return RedirectToAction("login", "Login");

            var guncellenecek = _context.Users.Find(kullanici.userId);
            if (guncellenecek != null)
            {
                guncellenecek.userName = kullanici.userName;
                guncellenecek.userPass = kullanici.userPass;
                guncellenecek.userRole = kullanici.userRole; // Rol güncellemesini de ekledik
                
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Kullanici");
        }
    }
}