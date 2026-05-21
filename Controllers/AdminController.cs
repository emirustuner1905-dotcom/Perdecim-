using Microsoft.AspNetCore.Mvc;
using PerdeCim.Models;
using System.Linq;

namespace PerdeCim.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        // --- Oturum Kontrolü ---
        private bool IsAuthorized()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("sessionAcik"));
        }

        // ================= PERDE MODÜLÜ =================

        // Sende ana panel burası (Klasörün: PerdeSil/index.cshtml)
        public IActionResult Index()
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");

            var urunler = _context.Perdeler.ToList();
            // Seninki gibi PerdeSil klasöründeki index.cshtml'i premium liste olarak açar
            return View("~/Views/PerdeSil/index.cshtml", urunler);
        }

        // Klasörün: PerdeEkle/index.cshtml
        public IActionResult PerdeEkle()
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");
            return View("~/Views/PerdeEkle/index.cshtml");
        }

        [HttpPost]
        public IActionResult PerdeEkle(Perde p)
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");

            if (p != null)
            {
                _context.Perdeler.Add(p);
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); // Listeye geri döner
        }

        public IActionResult Silme(int id)
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");

            var silinecek = _context.Perdeler.Find(id);
            if (silinecek != null)
            {
                _context.Perdeler.Remove(silinecek);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ================= KULLANICI MODÜLÜ =================

        // Klasörün: Kullanici/index.cshtml
        public IActionResult Kullanicilar()
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");

            var kullanicilar = _context.Users.ToList();
            return View("~/Views/Kullanici/index.cshtml", kullanicilar);
        }

        // Klasörün: Kullanici/Guncelle.cshtml
        public IActionResult KullaniciGuncelle(int id)
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");

            var user = _context.Users.Find(id);
            if (user == null) return RedirectToAction("Kullanicilar");

            return View("~/Views/Kullanici/Guncelle.cshtml", user);
        }

        [HttpPost]
        public IActionResult KullaniciGuncelle(User u)
        {
            if (!IsAuthorized()) return RedirectToAction("Index", "Login");

            var eskiUser = _context.Users.Find(u.userId);
            if (eskiUser != null)
            {
                eskiUser.userName = u.userName;
                eskiUser.userPass = u.userPass;
                _context.SaveChanges();
            }
            return RedirectToAction("Kullanicilar");
        }
    }
}