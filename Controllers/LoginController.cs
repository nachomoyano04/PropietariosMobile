
using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;


public class LoginController: Controller{
    public IActionResult Index(){
        return View();
    }
    public IActionResult Crear(){
        return View();
    }
   /* public IActionResult Autenticar(Usuario usuario){
        // TO DO: implementar logica de autenticacion
        // Autenticación de usuario
        if (ModelState.IsValid)
        {
            RepositorioUsuario repositorio = new RepositorioUsuario();
            Usuario usuarioEncontrado = repositorio.Verificar(usuario);
            if (usuarioEncontrado == null)
            {
                TempData["Error"] = "Email o contraseña incorrectos";
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index");                  
                    
            }
            
        }
        return View("Index",usuario);
      
    }*/
    [HttpPost]
    public async Task<IActionResult> Autenticar(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Aquí deberías validar el usuario contra tu base de datos
            RepositorioLogin repositorio = new RepositorioLogin();
            Usuario usuarioEncontrado = repositorio.Verificar(model);
            if (model.Email == usuarioEncontrado.Email && model.Password == usuarioEncontrado.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioEncontrado.Email),
                    new Claim(ClaimTypes.Role, usuarioEncontrado.Rol)
                    
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Datos invalidos");
        }

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}