using PerdeCim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PerdeCim.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // Giriş yapıldıktan sonra yönlendirilen panel ana sayfası (veya kontrol noktası)
        public ActionResult Index()
        {
            // Session'dan hem oturum durumunu hem de rolü kontrol ediyoruz
            string? sessionAcik = HttpContext.Session.GetString("sessionAcik");
            string? userRole = HttpContext.Session.GetString("userRole");

            // Oturum açılmamışsa YA DA açan kişi "Admin" değilse giriş sayfasına geri fırlat
            if (string.IsNullOrEmpty(sessionAcik) || userRole != "Admin")
            {
                return RedirectToAction("login", "Login");
            }

            // Eğer kişi Admin ise başarıyla paneli görebilir (Örn: Kullanıcı listesine yönlendirilebilir)
            return RedirectToAction("Index", "Kullanici");
        }

        // Formun post edildiği yer (Giriş Kontrolü)
        [HttpPost]
        public ActionResult SignIn(User u)
        {
            // CRITICAL FIX: .FirstOrDefault() kullanarak veritabanından direkt eşleşen TEK BİR kullanıcıyı çekiyoruz
            var loggedInUser = _context.Users.FirstOrDefault(k => k.userName == u.userName && k.userPass == u.userPass);

            if (loggedInUser != null)
            {
                // CRITICAL FIX: Giriş yapan kullanıcının rolü "Admin" mi?
                if (loggedInUser.userRole == "Admin")
                {
                    // Her şey yolunda; Session verilerini dolduruyoruz
                    HttpContext.Session.SetString("sessionAcik", loggedInUser.userName!);
                    HttpContext.Session.SetString("userRole", loggedInUser.userRole!);

                    // Admin paneline (Index aksiyonuna) gönder
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    // Kullanıcı var ama rolü Admin değilse (Örn: Personel ise) içeri alma!
                    ViewBag.Hata = "Bu panele giriş yetkiniz bulunmamaktadır! Sadece yöneticiler girebilir.";
                    return View("login");
                }
            }
            else
            {
                // Kullanıcı adı veya şifre yanlışsa
                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
                return View("login");
            }
        }

        // Çıkış yapma işlemi
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // Tüm session verilerini sıfırlar
            return RedirectToAction("login", "Login");
        }

        // Sadece giriş formunun açıldığı sayfa (Views/Login/login.cshtml veya Index.cshtml)
        public ActionResult login()
        {
            return View();
        }
    }
}