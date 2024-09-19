using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private readonly RepositorioInmueble _repo= new RepositorioInmueble();

    public InmuebleController(ILogger<InmuebleController> logger)
    {
        _logger = logger;
        _repo = new RepositorioInmueble();
    }

         // para ver vista detalles
    public IActionResult Detalles(int id)
    {
        var inmueble = _repo.Obtener(id);
        RepositorioContrato _repoContrato= new RepositorioContrato();
        if (inmueble == null)
        {
            _logger.LogWarning("No se encontro el inmueble con el id: {Id} ", id);
            return NotFound(); //  me devuelve error si no hay inmueble 
        }
        Contrato contrato = _repoContrato.ObtenerPorInmueble(inmueble.IdInmueble);
        ContratoViewModel Cvm = new ContratoViewModel{
            Inmueble=inmueble,
            Contrato= contrato
        };
        if(Cvm.Contrato != null){
            _logger.LogInformation("Hay un contrato");
        }
        else{
            _logger.LogWarning("No se encontro contrato");
        }
        return View(Cvm); // muestra a la vista
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Se invoca el index.");
        List<Inmueble> inmuebles = _repo.Listar();
        
        if(inmuebles==null){
            inmuebles= new List<Inmueble>();
        }
        return View(inmuebles);
    }

    public IActionResult Editar(int id){
        RepositorioPropietario _repoProp= new RepositorioPropietario();
        RepositorioDireccion _repoDire= new RepositorioDireccion();
        RepositorioTipo _repoTipo= new RepositorioTipo();
        List<Propietario> propietarios = _repoProp.Listar();
        List<Tipo> tipos = _repoTipo.Listar();
        Inmueble inmueble = _repo.Obtener(id);
        Direccion direccion = _repoDire.Obtener(inmueble.IdDireccion);

        InmuebleDireccion inDi = new InmuebleDireccion{
            Propietarios = propietarios,
            Tipos = tipos,
            Inmueble = inmueble,
            Direccion = direccion
        };
        return View(inDi);
    }
    public IActionResult Crear()
    {
        RepositorioPropietario _repoProp= new RepositorioPropietario();
        RepositorioDireccion _repoDire= new RepositorioDireccion();
        RepositorioTipo _repoTipo= new RepositorioTipo();
        List<Propietario> propietarios = _repoProp.Listar();
        List<Direccion> direcciones= _repoDire.Listar();
        List<Tipo> tipos = _repoTipo.Listar();
       
        InmuebleDireccion inDi = new InmuebleDireccion{
            Propietarios = propietarios,
            Tipos = tipos
        };
        return View(inDi);
    }

    [HttpPost]
    public IActionResult Guardar(InmuebleDireccion modelo)
    {
        _logger.LogInformation("EndPoint Guardar: "+modelo.ToString());
    RepositorioDireccion _repoDireccion = new RepositorioDireccion();
        Inmueble inmueble = new Inmueble {
            IdInmueble = modelo.Inmueble.IdInmueble,
            IdPropietario = modelo.Inmueble.IdPropietario,
            IdTipo= modelo.Inmueble.IdTipo,
            IdDireccion = modelo.Inmueble.IdDireccion,
            Metros2 = modelo.Inmueble.Metros2,
            CantidadAmbientes=modelo.Inmueble.CantidadAmbientes,
            Disponible= modelo.Inmueble.Disponible,
            Precio = modelo.Inmueble.Precio,
            Descripcion= modelo.Inmueble.Descripcion,
            Cochera = modelo.Inmueble.Cochera,
            Piscina = modelo.Inmueble.Piscina,
            Mascotas =  modelo.Inmueble.Mascotas,
            UrlImagen = modelo.Inmueble.UrlImagen
        };
        Direccion direccion = new Direccion{
            Calle = modelo.Direccion.Calle,
            Altura  = modelo.Direccion.Altura,
            Cp = modelo.Direccion.Cp,
            Ciudad = modelo.Direccion.Ciudad,
            Coordenadas = modelo.Direccion.Coordenadas,
        };

        int keyDireccion = _repoDireccion.Crear(direccion);
        inmueble.IdDireccion = keyDireccion;

        try
            {
                _logger.LogInformation("Id inmueble: "+inmueble.IdInmueble);
                if (inmueble.IdInmueble == 0){
                    int idCreado= _repo.Crear(inmueble);
                    _logger.LogInformation($"Se ha creado un nuevo inmueble con id: {idCreado}", inmueble.IdInmueble);
                }else{
                    _repo.Modificar(inmueble);
                    _logger.LogInformation($"Se ha modificado el inmueble con id: {inmueble.IdInmueble}", inmueble.IdInmueble);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al guardar el inmueble", inmueble.IdInmueble);
                ModelState.AddModelError("", "Oops ha ocurrido un error al intentar guardar el inmueble"); // muetsro model de aviso
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