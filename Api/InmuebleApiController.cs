using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
[Route("api/[controller]")]
[ApiController]
public class InmuebleApiController:ControllerBase{
    private DataContext context;
    public InmuebleApiController(DataContext context){
        this.context = context;
    }

    //http://localhost:5203/api/inmuebleapi
    [Authorize]
    [HttpGet]
    public IActionResult GetInmueblesPorPropietario(){
        var IdPropietario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var propietario = context.Propietario.Find(IdPropietario);
        if(propietario != null){
            List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == IdPropietario).Include(e => e.direccion).ToList();
            return Ok(inmuebles);
        }
        return BadRequest();
    }


    //http://localhost:5203/api/inmuebleapi     /*CHEQUEADO*/
    [Authorize]
    [HttpPost]
    public IActionResult CrearInmueble([FromForm]Inmueble inmueble, [FromForm]Direccion direccion){
        context.Direccion.Add(direccion);
        context.SaveChanges();
        if(direccion.IdDireccion > 0){
            int IdPropietario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            inmueble.IdPropietario = IdPropietario;
            inmueble.IdDireccion = direccion.IdDireccion;
            inmueble.Estado = true;
            inmueble.Disponible = false;
            context.Add(inmueble);
            context.SaveChanges();
            return Ok("Inmueble creado correctamente");
        }
        return BadRequest();
    }

    //http://localhost:5203/api/inmuebleapi/id
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult EditarInmueble(int id, Inmueble inmueble){
        if(context.Inmueble.Find(id) != null){
            context.Update(inmueble);
            context.SaveChanges();
            return Ok();
        }
        return NotFound();
    }

    //http://localhost:5203/api/inmuebleapi/id
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetInmueblePorId(int id){
        int idPropietario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var inmueble = context.Inmueble.Where(e => e.IdInmueble == id && e.IdPropietario == idPropietario).Include(e => e.direccion).ToList();
        if(inmueble != null && !inmueble.IsNullOrEmpty()){
            return Ok(inmueble);
        }else{
            return Unauthorized();
        }
    }

    //http://localhost:5203/api/inmuebleapi/disponibilidad/id
    [Authorize]
    [HttpPut("disponibilidad/{id}")]
    public IActionResult CambiarDisponibilidad(int id){
        int IdPropietario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var inmueble = context.Inmueble.First(e => e.IdInmueble == id && e.IdPropietario == IdPropietario);
        if(inmueble != null){
            string mensaje;
            if(inmueble.Disponible){
                inmueble.Disponible = false;
                mensaje = "Inmueble fuera de disponibilidad";
            }else{
                inmueble.Disponible = true;
                mensaje = "Inmueble disponible";
            }
            context.SaveChanges();
            return Ok(mensaje);
        }else{
            return Unauthorized();
        }
    }
}