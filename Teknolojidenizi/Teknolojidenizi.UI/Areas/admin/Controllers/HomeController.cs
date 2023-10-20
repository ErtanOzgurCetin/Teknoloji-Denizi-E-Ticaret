using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;
using Teknolojidenizi.UI.Tools;

namespace Teknolojidenizi.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class HomeController : Controller
    {
        IRepository<Admin> repoAdmin;
        public HomeController(IRepository<Admin> _repoAdmin)
        {
            repoAdmin = _repoAdmin;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous, Route("/admin/login")]
        public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous, Route("/admin/login"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            string md5Password = GeneralTools.GetMD5(password);
            Admin admin = repoAdmin.GetBy(x => x.UserName == username && x.Password == md5Password);
            if (admin != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()),
                    new Claim(ClaimTypes.Name, admin.NameSurname)
                };
                ClaimsIdentity claimsidentity = new ClaimsIdentity(claims, "TeknolojideniziAuth");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsidentity), new AuthenticationProperties() { IsPersistent = true });
                return Redirect("/admin");
            }
            else TempData["bilgi"] = "Kullanıcı adı ya da parola hatalı";

            return RedirectToAction("Login");
        }
        [Route("/admin/logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
                }
    }
}
