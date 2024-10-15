using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Route("api/[controller]")]
[ApiController]
public class InmuebleApiController:ControllerBase{
    private DataContext context;
    public InmuebleApiController(DataContext context){
        this.context = context;
    }

    // //http://localhost:5203/api/InmuebleApi/id
    // [Authorize]
    // [HttpGet("{id}")]
    // public IActionResult GetInmueblesPorPropietario(int id){
    //     // List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == id).Include(i => i.propietario).ToList();
    //     List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == id).ToList();
    //     return Ok(inmuebles);
    // }
    
    //http://localhost:5203/api/InmuebleApi
    [Authorize]
    [HttpGet]
    public IActionResult GetInmueblesPorPropietario(){
        var IdPropietario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var propietario = context.Propietario.Find(IdPropietario);
        if(propietario != null){
            List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == IdPropietario).ToList();
            return Ok(inmuebles);
        }
        return BadRequest();
    }


    //http://localhost:5203/api/InmuebleApi
    [HttpPost]
    public IActionResult CrearInmueble([FromForm]Inmueble inmueble){
        context.Add(inmueble);
        context.SaveChanges();
        return Ok();
    }

    //http://localhost:5203/api/InmuebleApi/id
    [HttpPut("{id}")]
    public IActionResult EditarInmueble(int id, Inmueble inmueble){
        if(context.Inmueble.Find(id) != null){
            context.Update(inmueble);
            context.SaveChanges();
            return Ok();
        }
        return NotFound();
    }
}