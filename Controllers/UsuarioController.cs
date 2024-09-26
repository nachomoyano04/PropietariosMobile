using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProyetoInmobiliaria.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

[Authorize]
public class UsuarioController: Controller{  
    private readonly ILogger _logger;
    private readonly RepositorioUsuario repo = new RepositorioUsuario();
    
    public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        
    [Authorize(Roles = "Administrador")]
    public IActionResult Index(){
        List<Usuario> usuarios = repo.Listar();
        return View(usuarios);
    }
    [AllowAnonymous]
    public IActionResult Crear(){        
        return View();
    }
    [Authorize(Roles = "Administrador")]
    public IActionResult Detalle(int id){
        Usuario u = repo.Obtener(id);
        if(u != null){
            return View(u);
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult CambiarPassword(int id){
        Usuario u = repo.Obtener(id);
        if(u != null){
            var UsuarioEditar = new UsuarioEditar{
                IdUsuario = u.IdUsuario,
                Apellido = u.Apellido,
                Rol = u.Rol,
                Email = u.Email,
                Nombre = u.Nombre
            };
            return View("CambiarPassword", UsuarioEditar);
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Editar(int id){
        int IdUsuario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var rol = User.FindFirst(ClaimTypes.Role).Value;
        if(id == IdUsuario || rol == "Administrador"){
            Usuario u = repo.Obtener(id);
            if(u != null){
                var UsuarioEditar = new UsuarioEditar{
                    Apellido = u.Apellido,
                    Avatar = u.Avatar,
                    Email = u.Email,
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Rol = u.Rol,
                };
                return View(UsuarioEditar);
            }
        }
        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GuardarEditar(UsuarioEditar usuario){
        if (ModelState.IsValid){
            Usuario u = repo.Obtener(usuario.IdUsuario);
            var avatarPath = u.Avatar;
            if (usuario.AvatarFile != null && usuario.AvatarFile.Length > 0){
                var fileName = Path.GetFileName(usuario.AvatarFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Avatar", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)){
                    usuario.AvatarFile.CopyTo(fileStream);
                }
                avatarPath = $"/img/Avatar/{fileName}";
            }else if(usuario.BorrarAvatar){
                avatarPath = "/img/Avatar/default.jpg";
            }
            var usuarioEntidad = new Usuario{
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Rol = usuario.Rol,
                Avatar = avatarPath
            };
            usuarioEntidad.Password = u.Password;
            int filasAfectadas = repo.Actualizar(usuarioEntidad);
            int IdUsuario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // var rol = User.FindFirst(ClaimTypes.Role).Value;
            if(filasAfectadas > 0 && usuarioEntidad.IdUsuario == IdUsuario){
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userNameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
                var avatarClaim= User.FindFirst("AvatarUrl");
                claimsIdentity.RemoveClaim(userNameClaim);
                claimsIdentity.RemoveClaim(avatarClaim);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
                claimsIdentity.AddClaim(new Claim("AvatarUrl", avatarPath)); 
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            return RedirectToAction("Index", "Home");
        }
        return View("Editar", usuario);
    }


    [HttpPost]
    public IActionResult CambiarPassword(UsuarioEditar usuario){
        if(ModelState.IsValid){
            Console.WriteLine($@"nueva pass: {usuario.NewPassword}");
            Console.WriteLine($@"confirm pass: {usuario.ConfirmPassword}");
            if(usuario.NewPassword != usuario.ConfirmPassword){
                Console.WriteLine($@"las contras no coinciden");                
                ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden.");
                // return RedirectToAction("Password", "Usuario", new {Id = usuario.IdUsuario});
                return View(usuario);
            }
            Console.WriteLine($@"coincidieron");                
            var usuarioEntity = repo.Obtener(usuario.IdUsuario);
            if(usuarioEntity == null){
                return RedirectToAction("Password", "Usuario");
            }else{
                usuarioEntity.Password = usuario.NewPassword;
                repo.Actualizar(usuarioEntity, usuario.NewPassword);
                return RedirectToAction("Index", "Home");
            }

        }
        var errores = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errores)
        {
            Console.WriteLine(error.ErrorMessage);
        }
        return View(usuario);
    }

    public JsonResult DadosDeBaja(){
        List<Usuario> usuarios = repo.ListarDadosDeBaja();
        return Json(usuarios);
    }
    public JsonResult DadosDeAlta(){
        List<Usuario> usuarios = repo.Listar();
        return Json(usuarios);
    }

    [AllowAnonymous]
    public IActionResult Guardar(Usuario usuario){
        if(ModelState.IsValid){
            usuario.Avatar= "/img/Avatar/default.jpg";
            if(usuario.AvatarFile != null && usuario.AvatarFile.Length > 0){
                var fileName= Path.GetFileName(usuario.AvatarFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Avatar",fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)){
                    usuario.AvatarFile.CopyTo(fileStream);
                }
                usuario.Avatar = "/img/Avatar/"+fileName;
            }
                repo.Guardar(usuario);
            return RedirectToAction( "Index", "Home");
        }
        var errores = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errores)
        {
            Console.WriteLine(error.ErrorMessage);
        }
            return View("Crear", usuario);
    }   

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Borrar (int IdUsuario){
        repo.Eliminar(IdUsuario);
        return RedirectToAction("Index","Usuario");
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Alta (int IdUsuario){
        repo.Alta(IdUsuario);
        return RedirectToAction("Index","Usuario");
    }

}