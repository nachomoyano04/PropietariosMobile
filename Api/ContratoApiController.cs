using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContratoApiController: ControllerBase{
    private DataContext context;
    private int IdPropietario;
    public ContratoApiController(DataContext context, IHttpContextAccessor httpContextAccessor){
        this.context = context;
        IdPropietario = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    
    //http://localhost:5203/api/contratoapi             //*CHEQUEADO*//
    [HttpGet]
    public IActionResult GetContratos(){ //todos los contratos asociados a los inmuebles del propietario
        var contratos = context.Contrato.Include(c => c.inmueble).ThenInclude(c => c.direccion).Include(c => c.inquilino).Where(c => c.inmueble.IdPropietario == IdPropietario);
        if(!contratos.IsNullOrEmpty()){
            return Ok(contratos);
        }
        return BadRequest();
    }
    


    //http://localhost:5203/api/contratoapi/id             //*CHEQUEADO*//
    [HttpGet("{id}")]
    public IActionResult GetContratosPorInmueble(int id){
        var contratos = context.Contrato.Where(c => c.IdInmueble == id && c.inmueble.IdPropietario == IdPropietario).ToList();
        return Ok(contratos);
    }

    //http://localhost:5203/api/contratoapi/inquilino/id             //*CHEQUEADO*//
    [HttpGet("inquilino/{id}")]
    public IActionResult GetInquilinoPorContrato(int id){
        var contrato = context.Contrato.Include(c => c.inquilino).Include(c => c.inmueble).FirstOrDefault(c => c.IdContrato == id);
        if(contrato != null){
            if(contrato.inmueble.IdPropietario == IdPropietario){
                return Ok(contrato.inquilino);
            }
        }
        return BadRequest();
    }


    //http://localhost:5203/api/contratoapi/pagos/id             //*CHEQUEADO*//
    [HttpGet("pagos/{id}")]
    public IActionResult GetPagosPorContrato(int id){
        var pagos = context.Pago.Include(e => e.contrato).Include(e => e.contrato.inmueble).Where(e => e.IdContrato == id && e.contrato.inmueble.IdPropietario == IdPropietario).ToList();
        if(pagos != null){
            return Ok(pagos);
        }
        return Unauthorized();
    }
}