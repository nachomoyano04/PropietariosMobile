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
        List <Usuario> usuarios = new List<Usuario>();
        usuarios = repo.Listar();
        return View(usuarios);
    }
    [AllowAnonymous]
    public IActionResult Crear(){        
        
        return View();
    }
    [Authorize(Roles = "Administrador")]
    public IActionResult Detalle(int id){
        // Usuario u = repo.Obtener(id);
        Usuario u = new Usuario{
            Apellido = "Moyano",
            Nombre = "Ignacio", 
            Email = "nachomoyag@as",
            Avatar = "/img/Avatar/Marvel image.jpg",
            Estado = true,
            Password = "asdasd",
            IdUsuario = 3,
            Rol = "Administrador"
        };
        return View(u);
    }

    public IActionResult Editar(int id){
        // Usuario u = repo.Obtener(id);
       Usuario u = repo.ObtenerPorId(id);
       _logger.LogInformation(u.Nombre);
        return View(u);
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