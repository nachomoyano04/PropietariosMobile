using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.Extensions.Logging; // me sirve para alertar LogWarning

public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;
    private readonly RepositorioContrato _repo;

    public ContratoController(ILogger<ContratoController> logger)
    {
        _logger = logger;
        _repo = new RepositorioContrato();
    }

  // para ver vista de detalles //creo que no se hace un detalleController, manejamos desde aqui
    public IActionResult Detalles(int id)
    {
        var contrato = _repo.Obtener(id);
        if (contrato == null)
        {
            _logger.LogWarning("No se encontro el contrato con el id: {Id}", id);
            return NotFound(); //  me devuelve error si no hay contrato 
        }

        return View(contrato); // muestra a la vista
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Se invoca el index.");
        var contratos = _repo.Listar();
        return View(contratos);
    }

    // public IActionResult Detalles(int id)
    // {
    //     if (id == 0)
    //     {
    //         _logger.LogWarning("No hay detalles para ese id");
    //         return NotFound();
    //     }
    //     var contrato = _repo.Obtener(id);
    //     if (contrato == null)
    //     {
    //         _logger.LogWarning("Contrato no encontrado con id : {Id}", id);
    //         return NotFound();
    //     }
    //     return View(contrato);
    // }

    public IActionResult Crear()
    {
        return View();
    }
    public IActionResult Crear(int id_i){
        
        RepositorioInmueble _repoInmueble= new RepositorioInmueble();

        Inmueble inmueble = _repoInmueble.Obtener(id_i);
        return View(inmueble);
    }

    [HttpPost]
    public IActionResult Guardar(Contrato contrato)
    {
        RepositorioContrato _repoContrato = new RepositorioContrato();

        List<Contrato> contratos = _repoContrato.ObtenerPorInmueble2(contrato.IdInmueble);

        foreach(var c in contratos){
            if((c.FechaInicio>=contrato.FechaInicio && (c.FechaInicio<=contrato.FechaFin||c.FechaInicio<=contrato.FechaAnulacion))||(c.FechaFin>=contrato.FechaInicio && (c.FechaFin<=contrato.FechaFin||c.FechaFin<=contrato.FechaAnulacion))){
                ModelState.AddModelError("", "Las fechas del nuevo contrato se solapan con un contrato existente.");
                return View("Crear");
            }
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (contrato.IdContrato == 0)
                {
                    _repo.Crear(contrato);
                    _logger.LogInformation("Se ha creado un nuevo contrato", contrato.IdContrato);
                }
                else
                {
                    _repo.Modificar(contrato);
                    _logger.LogInformation("Se ha modificado el contrato con id: {Id}", contrato.IdContrato);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al tratar de guardar el contrato", contrato.IdContrato);
                ModelState.AddModelError("",  "Oops ha ocurrido un error al intentar guardar el inmueble"); // muetsro model de aviso
            }
        }
        return View("Crear", contrato);
    }

    [HttpPost]
    public IActionResult Borrar(int id)
    {
        try
        {
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado el contrato con id: {Id}", id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el inmueble", id);
            return RedirectToAction("Index");
        }
    }
}