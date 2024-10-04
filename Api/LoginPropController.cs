using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;

[Route("api/[controller]")]
[ApiController]
public class LoginPropController : ControllerBase{
    private readonly DataContext context;

    public LoginPropController(DataContext context){
        this.context = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetPropietarios(int id){
        Propietario? propietario = context.Propietario.SingleOrDefault(p => p.IdPropietario == id);
        return Ok(propietario);
    }
}