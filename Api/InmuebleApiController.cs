using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InmuebleApiController:ControllerBase{
    private DataContext context;
    private int IdPropietario;
    public InmuebleApiController(DataContext context, IHttpContextAccessor httpContextAccessor){
        this.context = context;
        IdPropietario = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    //http://localhost:5203/api/inmuebleapi         /*CHEQUEADO*/
    [HttpGet]
    public IActionResult GetInmueblesPorPropietario(){
        var propietario = context.Propietario.Find(IdPropietario);
        if(propietario != null){
            List<Inmueble>inmuebles = context.Inmueble.Where(i => i.IdPropietario == IdPropietario).Include(e => e.direccion).ToList();
            return Ok(inmuebles);
        }
        return BadRequest();
    }


    //http://localhost:5203/api/inmuebleapi     /*CHEQUEADO*/
    [HttpPost]
    public IActionResult CrearInmueble([FromForm]Inmueble inmueble, [FromForm]Direccion direccion){
        context.Direccion.Add(direccion);
        context.SaveChanges();
        if(direccion.IdDireccion > 0){
            inmueble.UrlImagen = "default.png";
            inmueble.IdPropietario = IdPropietario;
            inmueble.IdDireccion = direccion.IdDireccion;
            inmueble.Estado = true;
            inmueble.Disponible = false;
            context.Inmueble.Add(inmueble);
            context.SaveChanges();
            return Ok(inmueble.IdInmueble+"");
        }
        return BadRequest("La direccion no se insertó");
    }


    //http://localhost:5203/api/inmuebleapi/id          /*CHEQUEADO*/
    [HttpPut("{id}")]
    public IActionResult EditarInmueble(int id, [FromForm]Inmueble inmueble, [FromForm]Direccion direccion){
        var inm = context.Inmueble.SingleOrDefault(i => i.IdInmueble == id);
        if(inm != null){
            var dir = context.Direccion.FirstOrDefault(i => i.IdDireccion == inm.IdDireccion);
            if(dir != null){
                if(inm.IdPropietario == IdPropietario){
                    //editamos los campos de direccion
                    dir.Calle = direccion.Calle;
                    dir.Altura = direccion.Altura;
                    dir.Ciudad = direccion.Ciudad;
                    //copiamos los campos que llegan desde el form en el inmueble que trackeamos
                    inm.CantidadAmbientes = inmueble.CantidadAmbientes;
                    inm.Cochera = inmueble.Cochera;
                    inm.Descripcion = inmueble.Descripcion;
                    inm.direccion = dir;
                    inm.Disponible = inmueble.Disponible;
                    inm.IdDireccion = dir.IdDireccion;
                    inm.Mascotas = inmueble.Mascotas;
                    inm.Metros2 = inmueble.Metros2;
                    inm.Piscina = inmueble.Piscina;
                    inm.Precio = inmueble.Precio;
                    inm.tipo = inmueble.tipo;
                    inm.Uso = inmueble.tipo;
                    context.SaveChanges();
                    return Ok("Datos del inmueble actualizados correctamente...");
                }else{
                    return Unauthorized();
                }
            }
        }
        return BadRequest();
    }

    [HttpPut("imagen/{id}")]
    public IActionResult EditarImagenInmueble(int id, [FromForm] IFormFile imagen){
        var inmueble = context.Inmueble.FirstOrDefault(i => i.IdInmueble == id);
        if(inmueble != null){
            if(imagen != null && imagen.Length > 0){
                var fileName = $"{IdPropietario}_{Guid.NewGuid()}{Path.GetExtension(imagen.FileName)}";
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Inmueble", fileName);
                using(var stream = new FileStream(ruta, FileMode.Create)){
                    imagen.CopyTo(stream);
                }
                inmueble.UrlImagen = fileName;
                int filasAfectadas = context.SaveChanges();
                if(filasAfectadas > 0){
                    return Ok("Imagen de inmueble modificada");
                }
                return Ok("No se modificó ninguna fila");
            }
        }
        return BadRequest();
    }

    //http://localhost:5203/api/inmuebleapi/id          /*CHEQUEADO*/
    [HttpGet("{id}")]
    public IActionResult GetInmueblePorId(int id){
        var inmueble = context.Inmueble.Where(e => e.IdInmueble == id && e.IdPropietario == IdPropietario).Include(e => e.direccion).ToList();
        if(inmueble != null && !inmueble.IsNullOrEmpty()){
            return Ok(inmueble);
        }else{
            return Unauthorized();
        }
    }

    //http://localhost:5203/api/inmuebleapi/disponibilidad/id          /*CHEQUEADO*/
    [HttpPut("disponibilidad/{id}")]
    public IActionResult CambiarDisponibilidad(int id){
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