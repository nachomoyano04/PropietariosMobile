using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class UsuarioController: Controller{  
        


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
        //Guardar usuario en la base de datos
        
        RepositorioUsuario repositorio = new RepositorioUsuario();
        repositorio.Guardar(usuario);
        return View();
    }
    
}