using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]
public class InmuebleApiController:ControllerBase{
    private DataContext context;
    public InmuebleApiController(DataContext context){
        this.context = context;
    }

    
    [HttpGet("porPropietario/{id}")]
    public IActionResult GetInmueblesPorPropietario(int id){
        List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == id).ToList();
        return Ok(inmuebles);
    }

    [HttpPost]
    public IActionResult GuardarInmueble(Inmueble inmueble){
        context.Add(inmueble);
        context.SaveChanges();
        return Ok();
    }
}