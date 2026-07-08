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

        public IActionResult Register([FromForm] UserRegisterRequest request)
        {
            var result = _userService.CreateUser(request);

            switch (result) 
            {
                case RegisterEnums.Success:
                    return Redirect("/Home/Index");

                case RegisterEnums.EmailInUse:
                    ModelState.AddModelError("", "Email Kullanimda.");
                    return View("Register");
            }

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
