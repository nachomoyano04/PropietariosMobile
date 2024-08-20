using System.Collections;
using Microsoft.AspNetCore.Mvc;

public class  PropietarioController : Controller{

    private readonly ILogger<PropietarioController> _logger;
    public PropietarioController(ILogger<PropietarioController> logger){
        _logger = logger;
        repo = new RepositorioInquilino();
    }
    public IActionResult Index(){
        return View();
    }

    public IActionResult Mostrar(){
        var propietarios = repo.obtenerPropietarios();
        return View(propietarios);
    }

    public IActionResult Baja(int Id){
        Boolean dadoDeBaja = repo.darDeBaja(Id);
        return dadoDeBaja;
    }

    public IActionResult Alta(string Apellido, string Nombre, string Telefono, string Correo){
        int Id = repo.crearPropietario(Apellido, Nombre, Telefono, Correo);
        if(Id != -1){
            return View(Id);
        }
    }

    public IActionResult Modificacion(int Id){
        // codigo para editar   
    }
} 
