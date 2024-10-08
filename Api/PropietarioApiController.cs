using System.Security.Claims;
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
    [HttpPut] //para cuando no necesite pasar el id porque tengo en la claim el id logueado
    // public IActionResult EditarPropietario([FromForm] Propietario propietario){
        // int IdPropietario = Int32.Parse(User.FindFirst("idPropietario").Value);
    [HttpPut("{id}")]
    public IActionResult EditarPropietario(int id, [FromForm] Propietario propietario){
        if(context.Propietario.Find(id) != null){
            context.Update(propietario);
            context.SaveChanges();
            return Ok();
        }
        return NotFound();
    }

}