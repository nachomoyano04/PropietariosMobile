using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;

public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private readonly RepositorioInmueble _repo;

    public InmuebleController(ILogger<InmuebleController> logger)
    {
        _logger = logger;
        _repo = new RepositorioInmueble();
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Se invoca el index.");
        var inmuebles = _repo.Listar();
        return View(inmuebles);
    }

    public IActionResult Detalles(int id)
    {
        if (id == 0)
        {
            _logger.LogWarning("No hay detalles para ese id");
            return NotFound();
        }
        var inmueble = _repo.Obtener(id);
        if (inmueble == null)
        {
            _logger.LogWarning("Inmueble no encontrado con id : {Id}", id);
            return NotFound();
        }
        return View(inmueble);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Guardar(Inmueble inmueble)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (inmueble.IdInmueble == 0)
                {
                    _repo.Crear(inmueble);
                    _logger.LogInformation("Se ha creado un nuevo inmueble", inmueble.IdInmueble);
                }
                else
                {
                    _repo.Modificar(inmueble);
                    _logger.LogInformation("Se ha modificado el inmueble con id: {Id}", inmueble.IdInmueble);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al guardar el inmueble", inmueble.IdInmueble);
                ModelState.AddModelError("", "Oops ha ocurrido un error al intentar guardar el inmueble");
            }
        }
        return View("Crear", inmueble);
    }

    [HttpPost]
    public IActionResult Borrar(int id)
    {
        try
        {
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado el inmueble con id: {Id}", id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el inmueble", id);
            return RedirectToAction("Index");
        }
    }
}