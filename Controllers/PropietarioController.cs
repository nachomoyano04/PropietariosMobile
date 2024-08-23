using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
public class PropietarioController : Controller{
    private readonly ILogger<PropietarioController> _logger;
    private RepositorioPropietario repo;
    public PropietarioController(ILogger<PropietarioController> logger){
        _logger = logger;
        repo = new RepositorioPropietario();  //CREAR DESDE EL MODELO
    }
    public IActionResult Index(){
        var propietarios = repo.GetPropietarios();
        return View(propietarios);
    }
    public IActionResult Editar(int Id){
        if(Id == 0){
            return View();
        }else{
            return View(repo.GetPropietario(Id));
        }
    }

    [HttpPost]
    public IActionResult Guardar(int Id, Propietario propietario){
        Id = propietario.IdPropietario;
        if(Id == 0){
            repo.DarDeAlta(propietario);
        }else{
            repo.Modificar(propietario);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Borrar(int Id){
        repo.DarDeBaja(Id);
        return RedirectToAction("Index");
    }    


    // public IActionResult Mostrar(){
    //     var propietarios = repo.obtenerPropietarios(); //CREAR DESDE EL MODELO
    //     return View(propietarios);
    // }

    // public IActionResult Baja(int Id){
    //     Boolean dadoDeBaja = repo.darDeBaja(Id); //CREAR DESDE EL MODELO
    //     return dadoDeBaja;
    // }

    // public IActionResult Alta(string Apellido, string Nombre, string Telefono, string Correo){
    //     int Id = repo.crearPropietario(Apellido, Nombre, Telefono, Correo); //Devolver el id autoincremental q crea la bd
    //     if(Id != -1){
    //         return View(Id);
    //     }
    // }

    // public IActionResult Modificacion(int Id, string Apellido, string Nombre, string Telefono, string Correo){
    //     int Id = repo.modificar(Id, Apellido, Nombre, Telefono, Correo); //DEsde el modelo, para
    //     // codigo para editar
    // }
} 
