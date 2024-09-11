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
    public IActionResult Autenticar(Usuario usuario)
    {
        // Autenticación de usuario
        if (ModelState.IsValid)
        {
            
            if (usuarioEncontrado == null)
            {
                TempData["Error"] = "Email o contraseña incorrectos";
                return RedirectToAction("Login");
            }
            else
            {
                RepositorioUsuario repositorio = new RepositorioUsuario();
                Usuario usuarioEncontrado = repositorio.Verificar(usuario.Email, usuario.Clave);
                return RedirectToAction("Index");
            }
            
        }
        return View(usuario);
    }
}