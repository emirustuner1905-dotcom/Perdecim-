using Microsoft.AspNetCore.Mvc;
using PerdeCim.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PerdeCim.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        // --- ANA SAYFALAR ---
        public ActionResult Index() => View(_context.Perdeler.ToList());
        public ActionResult Modeller() => View(_context.Perdeler.ToList());
        public ActionResult Iletisim() => View();
        public ActionResult Konum() => View();

        // --- YARDIM VE BİLGİLENDİRME ---
        public ActionResult KargoTakip() => View();
        public ActionResult Garanti() => View();
        public ActionResult OlcuAlma() => View();
        public ActionResult SSS() => View();
        public ActionResult OdemeSecenekleri() => View();

        public ActionResult PerdeDetay(int id)
        {
            var perde = _context.Perdeler.Find(id);
            return perde == null ? NotFound() : View(perde);
        }

        // --- NUMUNE İSTE ---
        public ActionResult NumuneIste(int id)
        {
            ViewBag.PerdeId = id;
            return View();
        }

        [HttpPost]
        public ActionResult NumuneIste(int PerdeId, string Adres)
        {
            ViewBag.Mesaj = "Numune talebiniz başarıyla alındı!";
            return View();
        }

        // --- SEPET İŞLEMLERİ ---
        private List<Perde> GetSepet()
        {
            var json = HttpContext.Session.GetString("Sepet");
            return string.IsNullOrEmpty(json) ? new List<Perde>() : JsonSerializer.Deserialize<List<Perde>>(json);
        }

        private void SaveSepet(List<Perde> sepet)
        {
            HttpContext.Session.SetString("Sepet", JsonSerializer.Serialize(sepet));
        }

        public ActionResult Sepetim()
        {
            var sepet = GetSepet();
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest" 
                ? PartialView("_SepetPartial", sepet) 
                : View(sepet);
        }

        public ActionResult SepeteEkle(int id)
        {
            var perde = _context.Perdeler.Find(id);
            if (perde == null) return NotFound();

            var sepet = GetSepet();
            if (!sepet.Any(x => x.PerdeId == id))
            {
                sepet.Add(perde);
                SaveSepet(sepet);
            }
            return Ok();
        }

        public ActionResult SepettenSil(int id)
{
    var sepet = GetSepet();
    sepet.RemoveAll(x => x.PerdeId == id);
    SaveSepet(sepet);
    
    // Çok önemli: Burası PartialView dönmeli
    return PartialView("_SepetPartial", sepet);
}
    }
}