    using KutuphaneSatis.DTOs.Request.UserRequest;
using KutuphaneSatis.Enums;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace KutuphaneSatis.Controllers
{

    
    public class UserController : Controller
    {
        private readonly IUserLRService _userService;



        public UserController(IUserLRService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequest request)
        {
            // 1. Servisten işlemi yapmasını istiyoruz. 
            // Artık servisimiz sadece durum değil, yeni oluşturulan ID'yi de dönüyor.
            var result = _userService.CreateUser(request);

            switch (result.Status) // result.Status = Başarı durumu (Enum)
            {
                case RegisterEnums.Success:
                    // 2. ID'yi elde ettiğimiz için hemen sepeti (Cart) oluşturuyoruz!
                    _userService.CreateCart(result.UserId); // result.UserId = Yeni kullanıcının ID'si

                    // 3. Başka bir Action'ı (Login) çağırmak yerine, Yaka Kartını (Cookie) doğrudan burada basıyoruz.
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, "StandartKullanici")
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // 4. Çerezi tarayıcıya gönderiyoruz (Kullanıcı şu an sisteme giriş yapmış sayıldı)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // 5. Mutlu son: Her şey hazır, maceraya atılması için ana sayfaya yolluyoruz.
                    return Redirect("/Home/Index");

                case RegisterEnums.EmailInUse:
                    // Hata durumunda View'a mesaj gönderiyoruz
                    ModelState.AddModelError("", "Email Kullanımda.");
                    return View("Register");
            }

            // Beklenmeyen bir durum olursa diye varsayılan dönüş
            return View();
        }

        [HttpGet]

            public IActionResult Register() { return View("Register"); }



            
            [HttpGet]
            public IActionResult Login() { return View("Login"); }

            [HttpPost]
            public async Task<IActionResult> Login([FromForm] UserLoginRequest userLoginRequest)
            {
                // Servis artık bize hem durumu hem kullanıcıyı dönüyor
                var result = _userService.Check(userLoginRequest);

                switch (result.Status)
                {
                    case LoginEnums.Success:
                        // 1. TARAYICIYA KAYDEDİLECEK BİLGİLER (KİMLİK KARTI)
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.UserInfo.Id.ToString()),
                    new Claim(ClaimTypes.Email, result.UserInfo.Email),
                    new Claim(ClaimTypes.Name, result.UserInfo.Name),
                    // İleride sisteme "Admin" eklediğinde rolünü buradan vereceksin:
                    new Claim(ClaimTypes.Role, "StandartKullanici")
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // 2. ÇEREZİ OLUŞTUR VE TARAYICIYA GÖNDER (Giriş yapıldı!)
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));

                        return Redirect("/Home/Index"); // Başarılıysa ana sayfaya yolla

                    case LoginEnums.WrongPassword:
                        ModelState.AddModelError("", "Hatalı Şifre");
                        return View("Login");

                    case LoginEnums.UserNotFound:
                        ModelState.AddModelError("", "E-posta'ya ait bir kullanıcı bulunamadı.");
                        return View("Login");
                }

                return View();
            }
        [HttpGet]

        public async Task<IActionResult> LogOut(string logoutId) 
        
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Redirect("/Home/Index");


        }


        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);


            if (string.IsNullOrEmpty(userEmail))
            {
                // Bir şeyler ters gittiyse ve e-posta yoksa tekrar logine yolla
                return RedirectToAction("Login");
            }

            var userProfileDto = _userService.GetProfileByEmail(userEmail);

            if (userProfileDto == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }


            return View(userProfileDto);

        }

        
    
    
    }
}
