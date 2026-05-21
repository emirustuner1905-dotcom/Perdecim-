using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PerdeCim.Models;
using Microsoft.AspNetCore.Http; // Session işlemleri için şart

namespace PerdeCim.Controllers
{
    public class PerdeSilController : Controller
    {
        private readonly DataContext _context;

        public PerdeSilController(DataContext context)
        {
            _context = context;
        }

        // Listeleme Sayfası (Admin ve Personel görebilir)
        public ActionResult Index()
        {
            // GÜVENLİK: Oturum açılmamışsa listeyi gösterme, login'e at
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionAcik")))
            {
                return RedirectToAction("login", "Login");
            }

            var liste = _context.Perdeler.ToList();
            return View(liste);
        }

        // Detay Sayfası (Admin ve Personel görebilir)
        public ActionResult Detay(int id)
        {
            // GÜVENLİK: Oturum açılmamışsa detay sayfasını gösterme
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionAcik")))
            {
                return RedirectToAction("login", "Login");
            }

            var perde = _context.Perdeler.FirstOrDefault(x => x.PerdeId == id);
            if (perde == null) return RedirectToAction("Index");
            return View(perde);
        }

        // Silme İşlemi (SADECE ADMİNLER YAPABİLİR)
        public ActionResult Sil(int id)
        {
            // CRITICAL PROTECTION: Giriş yapan kişinin rolü Admin değilse silme yetkisi yok!
            string? userRole = HttpContext.Session.GetString("userRole");
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionAcik")) || userRole != "Admin")
            {
                return RedirectToAction("login", "Login");
            }

            // Eğer Admin ise silme işlemini yap
            var silinecek = _context.Perdeler.FirstOrDefault(x => x.PerdeId == id);
            if (silinecek != null)
            {
                _context.Perdeler.Remove(silinecek);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}