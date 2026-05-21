using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Veritabanı işlemleri için gerekli
using System.Linq; // Sorgulama işlemleri için gerekli
using PerdeCim.Models;
using Microsoft.AspNetCore.Http; // Session okuyabilmek için gerekli

namespace PerdeCim.Controllers
{
    public class PerdeEkleController : Controller
    {
        private readonly DataContext _context;

        public PerdeEkleController(DataContext context)
        {
            _context = context;
        }

        // Perde Ekleme Sayfası (Formun görüntülendiği yer)
        public ActionResult Index()
        {
            // 1. GÜVENLİK ADIMI: Giriş yapılmış mı ve giriş yapan kişi Admin mi?
            string? userRole = HttpContext.Session.GetString("userRole");
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionAcik")) || userRole != "Admin")
            {
                // Yetkisi yoksa direkt Giriş Sayfasına fırlatıyoruz
                return RedirectToAction("login", "Login");
            }

            return View();
        }

        [HttpPost] // Formdan gelen verileri yakalamak için şart
        public ActionResult Ekle(Perde perde)
        {
            // 2. GÜVENLİK ADIMI: Formu aşsa bile post işleminde Admin olup olmadığını tekrar check ediyoruz (API/Post güvenliği)
            string? userRole = HttpContext.Session.GetString("userRole");
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionAcik")) || userRole != "Admin")
            {
                return RedirectToAction("login", "Login");
            }

            // Veritabanı işlemlerine devam
            if (perde != null)
            {
                _context.Perdeler.Add(perde);
                _context.SaveChanges();
            }

            // Ürün eklendikten sonra silme ve listeleme yaptığın Index'e gider
            return RedirectToAction("Index", "PerdeSil");
        }
    }
}