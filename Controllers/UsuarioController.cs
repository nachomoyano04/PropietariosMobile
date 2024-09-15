using Microsoft.AspNetCore.Mvc;
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
    [AllowAnonymous]
    public IActionResult Guardar(Usuario usuario){
        
        _logger.LogInformation($"Entro al Guardar con los datos de usuadio : Nombre {usuario.Nombre}, Apellido: {usuario.Apellido}, Rol: {usuario.Rol}");
        RepositorioUsuario _repoUsuario= new RepositorioUsuario();
        int id =  _repoUsuario.Guardar(usuario);

        return RedirectToAction("Index","Login");

    }
    
}