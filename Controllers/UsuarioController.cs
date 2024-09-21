using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProyetoInmobiliaria.Models;

[Authorize]
public class UsuarioController: Controller{  
       private readonly ILogger _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        
    public IActionResult Index(){
        return View();
    }
    [AllowAnonymous]
    public IActionResult Crear(){        
        
        return View();
    }
    [Authorize(Roles = "Administrador")]
        public IActionResult Borrar(int id){
            RepositorioUsuario repositorio = new RepositorioUsuario();
            repositorio.Borrar(id);
            return RedirectToAction("Index", "Login");
    }
    
    [Authorize(Roles = "Administrador")]
    public IActionResult Detalles(int id){
        return View();
    }

    public IActionResult Editar(int id){
        return View();
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