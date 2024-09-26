using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProyetoInmobiliaria.Models;

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

    public IActionResult Editar(int id){
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
        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult GuardarEditar(UsuarioEditar usuario){
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
            if (!string.IsNullOrWhiteSpace(usuario.NewPassword)){
                usuarioEntidad.Password = usuario.NewPassword;
                repo.Actualizar(usuarioEntidad, usuario.NewPassword);
            }else{
                usuarioEntidad.Password = u.Password;
                repo.Actualizar(usuarioEntidad);
            }
            return RedirectToAction("Index", "Home");
        }
        return View("Editar", usuario);
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
            // if(usuario.IdUsuario == 0){
                repo.Guardar(usuario);
            // }else{
                // repo.Actualizar(usuario);
            // }
            return RedirectToAction( "Index", "Home");
        }
        var errores = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errores)
        {
            Console.WriteLine(error.ErrorMessage);
        }
        // if(usuario.IdUsuario == 0){
            return View("Crear", usuario);
        // }else{
            // return View("Editar", usuario);
        // }
    }   

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Borrar (int IdUsuario){
        repo.Eliminar(IdUsuario);
        return RedirectToAction("Index","Usuario");
    }

}