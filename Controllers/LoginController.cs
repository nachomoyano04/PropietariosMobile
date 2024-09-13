using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyetoInmobiliaria.Models;

    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticación(Usuario user)
        {
            RepositorioLogin _repoLogin = new RepositorioLogin();

            Usuario u = _repoLogin.Validar(new LoginViewModel { Email = user.Email , Password = user.Password });
            if (u != null){
                ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identidad.AddClaim(ClaimTypes.Name, u.Nombre);
                identidad.AddClaim(ClaimTypes.Role, u.Rol);

                await  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad));

                return RedirectToaction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToaction("Index","Login");
        }
}
