using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using ProyetoInmobiliaria.Models;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PropietarioApiController:ControllerBase{
    private DataContext context;
    private IConfiguration configuration;
    private int IdPropietario;

    private IWebHostEnvironment environment;
    
    public PropietarioApiController(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment){
        this.context = context;
        this.configuration = configuration;
        this.environment = environment;
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
            propietario.Avatar = propietarioBD.Avatar;
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
    public IActionResult ChangePassword([FromForm] string PasswordVieja, [FromForm] string Password){
        var propietario = context.Propietario.Find(IdPropietario);
        if(propietario != null){
            if(!Password.IsNullOrEmpty() && !PasswordVieja.IsNullOrEmpty()){
                if(propietario.Password == HashearPassword(PasswordVieja)){
                    propietario.Password = HashearPassword(Password);
                    context.SaveChanges();
                    return Ok("Campos guardados correctamente!");
                }else{
                    return Ok("La password vieja no coincide");
                }
            }else{
                return BadRequest("Debe completar todos los campos");
            }
        }
        return NotFound();
    }

    [HttpGet("generarPassword")]
    public IActionResult GenerarPassword(){
        //generamos la nueva clave "random"
        Random random = new Random(Environment.TickCount);
        string password = "";
        string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
        for(int i = 0; i < 8; i++){
            password += caracteres[random.Next(0, caracteres.Length)];
        }
        //actualizamos el propietario con esa clave hasheada
        var propietario = context.Propietario.FirstOrDefault(e => e.IdPropietario == IdPropietario);
        propietario.Password = HashearPassword(password);
        int filasAfectadas = context.SaveChanges();
        Console.WriteLine($"Filas afectadas: {filasAfectadas}");
        if(filasAfectadas > 0){
            //mandamos el mail
            string mensajeEnHtml = $"<h1>Bienvenido nuevamente {propietario.Nombre}!</h1>"
            +$"<p>Su nueva password es: {password}</p>";
            EnviarMail("Inmobiliaria", "nachomoyag@gmail.com", propietario.Nombre, propietario.Correo, mensajeEnHtml);
			return Ok("Password generada correctamente");
        }else{
            return BadRequest("No se pudo pisar la password en generar password");
        }
    }

    //http://localhost:5203/api/propietarioapi/recuperarpassword
    [AllowAnonymous]
    [HttpPost("recuperarPassword")]
    public IActionResult RecuperarPassword([FromForm] string correo){
        var propietario = context.Propietario.FirstOrDefault(e => e.Correo == correo);
        if(propietario != null){
            string dominio = "";
            if(environment.IsDevelopment()){
                // dominio = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                dominio = "http://localhost:5203/api/propietarioapi/generarPassword";
            }else{
                dominio = "www.myinmobiliaria.com";
            }
            string token = GenerarToken(propietario);
            dominio += $"?access_token={token}";
            string mensajeEnHtml = $"<h1>Hola {propietario.Nombre}!</h1>"
            +$"<p>Para generar una nueva password acceda al siguiente link {dominio}</p>";
            EnviarMail("Inmobiliaria", "nachomoyag@gmail.com", propietario.Nombre, propietario.Correo, mensajeEnHtml);
            return Ok("Email de recuperacion enviado");
        }
        return BadRequest("No existe un usuario con ese email.");
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

    //metodo para enviar mails
    public void EnviarMail(string emisor, string emisorCorreo, string destinatario, string destinatarioCorreo, string mensajeEnHtml){
        var mensaje = new MimeKit.MimeMessage();
        mensaje.To.Add(new MailboxAddress(destinatario, destinatarioCorreo));
        mensaje.From.Add(new MailboxAddress(emisor, emisorCorreo));
        mensaje.Subject = "Mensaje de recuperacion de contraseña";
        mensaje.Body = new TextPart("html"){
            Text = mensajeEnHtml
        };
        MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
        client.ServerCertificateValidationCallback = (object sender,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors) =>
            { return true; };
            client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
            client.Authenticate(configuration["SMPT:User"], configuration["SMPT:Password"]);//estas credenciales deben estar en el user secrets
            client.Send(mensaje);
    }

}