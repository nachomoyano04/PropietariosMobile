using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
[Route("api/[controller]")]
[ApiController]
public class PropietarioApiController:ControllerBase{
    private DataContext context;
    
    public PropietarioApiController(DataContext context){
        this.context = context;
    }

    //http://localhost:5203/api/PropietarioApi/id
    [HttpPut("{id}")]
    public IActionResult EditarPropietario(int id, [FromForm] Propietario propietario){
        if(context.Propietario.Find(id) != null){
            propietario.IdPropietario = id;
            context.Update(propietario);
            context.SaveChanges();
            return Ok();
        }
        return NotFound();
    }


}