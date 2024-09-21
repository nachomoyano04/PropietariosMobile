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
        
    public IActionResult Index(){
<<<<<<< HEAD
        List <Usuario> usuarios = new List<Usuario>();
        usuarios = repo.Listar();
=======
        List<Usuario> usuarios = repo.Listar();
>>>>>>> 3e11d5dab6159658d22bb641ac042e888e46dc5d
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
<<<<<<< HEAD
        // Usuario u = repo.Obtener(id);
       Usuario u = repo.ObtenerPorId(id);
       _logger.LogInformation(u.Nombre);
        return View(u);
=======
        Usuario u = repo.Obtener(id);
        if(u != null){
            return View(u);
        }
        return RedirectToAction("Index", "Home");
>>>>>>> 3e11d5dab6159658d22bb641ac042e888e46dc5d
    }

    [AllowAnonymous]
    public IActionResult Login(){
        return View();
    }
    [Authorize(Roles = "Administrador")]
    public IActionResult Guardar(Usuario usuario, IFormFile archivo){
        usuario.Avatar= "/img/Avatar/default.jpg";
        if(archivo!=null){
            var fileName= Path.GetFileName(archivo.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Avatar",fileName);
            _logger.LogInformation(filePath);
            using (var fileStream = new FileStream(filePath,FileMode.Create))
            {
                archivo.CopyTo(fileStream);
            }
            usuario.Avatar = "/img/Avatar/"+fileName;
        }
        _logger.LogInformation(usuario.Avatar);


        RepositorioUsuario repositorio = new RepositorioUsuario();
        repositorio.Guardar(usuario);
        return RedirectToAction( "Index", "Login");
    }   
}