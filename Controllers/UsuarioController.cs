using Microsoft.AspNetCore.Mvc;

public class UsuarioController: Controller{  
        


    public IActionResult Index(){
        return View();
    }
    public IActionResult Crear(){
         
        
        return View();
    }
    public IActionResult Detalles(int id){
        return View();
    }
    public IActionResult Editar(int id){
        return View();
    }

    public IActionResult Login(){
        return View();
    }
    
}