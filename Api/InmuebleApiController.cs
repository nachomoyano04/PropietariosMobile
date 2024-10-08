using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]
public class InmuebleApiController:ControllerBase{
    private DataContext context;
    public InmuebleApiController(DataContext context){
        this.context = context;
    }

    //http://localhost:5203/api/InmuebleApi/id
    [HttpGet("{id}")]
    public IActionResult GetInmueblesPorPropietario(int id){
        List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == id).ToList();
        return Ok(inmuebles);
    }


    //http://localhost:5203/api/InmuebleApi
    [HttpPost]
    public IActionResult GuardarInmueble([FromForm]Inmueble inmueble){
        context.Add(inmueble);
        context.SaveChanges();
        return Ok();
    }

    //http://localhost:5203/api/InmuebleApi/id
    [HttpPut("{id}")]
    public IActionResult EditarInmueble(int id, Inmueble inmueble){
        if(context.Inmueble.Find(id) != null){
            inmueble.IdInmueble = id;
            context.Update(inmueble);
            context.SaveChanges();
            return Ok();
        }
        return NotFound();
    }
}