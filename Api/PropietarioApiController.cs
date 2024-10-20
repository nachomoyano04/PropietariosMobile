using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyetoInmobiliaria.Models;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PropietarioApiController:ControllerBase{
    private DataContext context;
    private IConfiguration configuration;
    private int IdPropietario;
    
    public PropietarioApiController(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor){
        this.context = context;
        this.configuration = configuration;
        string claim =  httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IdPropietario = ParsearId(claim);
    }

    //metodo que obtiene de la claim el id del propietario si no es nulo
    private int ParsearId(string? claim){
        if(!claim.IsNullOrEmpty()){
            return Int32.Parse(claim);
        }
        return 0;
    }

    //http://localhost:5203/api/propietarioapi/login    CHEQUEADO
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromForm] LoginPropietario login){
        Propietario propietario = context.Propietario.SingleOrDefault(e => e.Correo.ToLower() == login.Correo.ToLower());
        if(propietario != null){
            bool passwordCorrecta = VerificarPassword(login.Password, propietario.Password);
            if(passwordCorrecta){
                string token = GenerarToken(propietario);
                return Ok(token);
            }
        }
        return NotFound("Usuario y/o password incorrectos.");
    }
    
    //http://localhost:5203/api/propietarioapi
    [HttpPut]
    public IActionResult EditarPropietario([FromForm] Propietario propietario){
        var propietarioBD = context.Propietario.SingleOrDefault(p => p.IdPropietario == IdPropietario);
        if(propietarioBD != null){
            if(propietario.Password.IsNullOrEmpty()){
                propietario.Password = propietarioBD.Password;
            }else{
                propietario.Password = HashearPassword(propietario.Password);
            }
            context.Entry(propietarioBD).State = EntityState.Detached;
            propietario.IdPropietario = IdPropietario;
            propietario.Estado = true;
            context.Propietario.Update(propietario);
            context.SaveChanges();
            return Ok("Datos del perfil actualizados correctamente...");
        }
        return NotFound();
    }

    //http://localhost:5203/api/propietarioapi/avatar
    [HttpPut("avatar")]
    public IActionResult EditarAvatar([FromForm] IFormFile avatar){
        var propietario = context.Propietario.Find(IdPropietario); 
        if(propietario != null){
            if(avatar != null && avatar.Length > 0){
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Avatar", avatar.FileName);
                using(var stream = new FileStream(ruta, FileMode.Create)){
                    avatar.CopyTo(stream);
                }
                propietario.Avatar = avatar.FileName;
                context.SaveChanges();      
                return Ok("Avatar actualizado");
            }
        }
        return Unauthorized();
    }

    //http://localhost:5203/api/propietarioapi    CHEQUEADO
    [HttpGet]
    public IActionResult GetPropietario(){
        var propietario = context.Propietario.Find(IdPropietario);
        return Ok(propietario);
    }

    //http://localhost:5203/api/propietarioapi/password     CHEQUEADO
    [HttpPut("password")]
    public IActionResult ChangePassword([FromForm] string Password){
        var propietario = context.Propietario.Find(IdPropietario);
        if(propietario != null){
            if(!Password.IsNullOrEmpty()){
                propietario.Password = HashearPassword(Password);
                context.SaveChanges();
                return Ok("Campos guardados correctamente!");
            }else{
                return BadRequest("Falta parametro.");
            }
        }
        return NotFound();
    }


    //SOLO PARA DESARROLLO //http://localhost:5203/api/propietarioapi/crear   //SOLO PARA DESARROLLO
    [AllowAnonymous]
    [HttpPost("crear")]
    public IActionResult Crear([FromForm] Propietario p){
        p.Password = HashearPassword(p.Password);
        context.Propietario.Add(p);
        context.SaveChanges();
        return Ok($"idPropietario: {p.IdPropietario}");
    }

    //metodo para verificar que las password coinciden
    public bool VerificarPassword(string Password, string DBPassword){
        string hashed = HashearPassword(Password);
        return DBPassword == hashed;
    }

    //metodo para generar token
    public string GenerarToken(Propietario propietario){
        var claveSegura = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"]));
        var credenciales = new SigningCredentials(claveSegura, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, propietario.IdPropietario.ToString()),
            new Claim(ClaimTypes.Email, propietario.Correo),
            new Claim(ClaimTypes.Name, propietario.Nombre),
            new Claim(ClaimTypes.Surname, propietario.Apellido)
        };
        var token = new JwtSecurityToken(configuration["TokenAuthentication:Issuer"],
        configuration["TokenAuthentication:Audience"],
        claims,
        expires: DateTime.Now.AddMinutes(15),
        signingCredentials: credenciales);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //metodo para hashear password
    public string HashearPassword(string password){
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					    password: password,
                        salt: Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
        return hash;
    }

}