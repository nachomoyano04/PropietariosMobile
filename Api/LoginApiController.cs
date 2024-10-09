using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;

[Route("api/[controller]")]
[ApiController]
public class LoginApiController : ControllerBase{
    private readonly DataContext context;

    public LoginApiController(DataContext context){
        this.context = context;
    }
}