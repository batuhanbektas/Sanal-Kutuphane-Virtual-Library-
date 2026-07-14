using KutuphaneSatis.DTOs.Request.UserRequest;
using KutuphaneSatis.Enums;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Register([FromForm] UserRegisterRequest request)
        {
            var result = _userService.CreateUser(request);

            switch (result.Status)
            {
                case RegisterEnums.Success:
                    _userService.CreateCart(result.UserId);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                        new Claim(ClaimTypes.Email, request.Email),
                        new Claim(ClaimTypes.Name, result.Name.ToString()),
                        new Claim(ClaimTypes.Role, "StandartKullanici")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return Redirect("/Home/Index");

                case RegisterEnums.EmailInUse:
                    ModelState.AddModelError("", "Email Kullanımda.");
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
            var result = _userService.Check(userLoginRequest);

            switch (result.Status)
            {
                case LoginEnums.Success:
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.UserInfo.Id.ToString()),
                        new Claim(ClaimTypes.Email, result.UserInfo.Email),
                        new Claim(ClaimTypes.Name, result.UserInfo.Name)
                    };

                    if (result.UserInfo.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "StandartKullanici"));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return Redirect("/Home/Index");

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