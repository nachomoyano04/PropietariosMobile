using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProyetoInmobiliaria.Models;

[Route("api/[controller]")]
[ApiController]
public class LoginApiController : ControllerBase{
    private readonly DataContext context;
    private IConfiguration configuration;

    public LoginApiController(DataContext context, IConfiguration configuration){
        this.context = context;
        this.configuration = configuration;
    }

    [HttpPost]
    public IActionResult Login([FromForm] LoginPropietario login){
        Console.WriteLine($"Correo form: {login.Correo}");
        Propietario propietario = context.Propietario.Single(e => e.Correo.ToLower() == login.Correo.ToLower());
        Console.WriteLine($"Correo propietario: {propietario.Correo}");
        if(propietario != null){
            bool passwordCorrecta = VerificarPassword(propietario.Password, login.Password);
            if(passwordCorrecta){
                string token = generarToken(propietario);
                return Ok(token);
            }
        }
        return NotFound("No existe el usuario");
    }
    
    //metodo para verificar que las password coinciden
    public bool VerificarPassword(string Password, string DBPassword){
                //PARA CUANDO TENGA LAS PASSWORDS HASHEADAS
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: Password,
					salt: Encoding.ASCII.GetBytes(configuration["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
        DBPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: DBPassword,
					salt: Encoding.ASCII.GetBytes(configuration["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
        // bool verificada = BCrypt.Net.BCrypt.Verify(Password, DBPassword);
        bool verificada = DBPassword == hashed;
        return verificada;
    }

    //metodo para generar token
    public string generarToken(Propietario propietario){
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
}