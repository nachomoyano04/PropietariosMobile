using System.ComponentModel.DataAnnotations;
public class UsuarioEditar{
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El campo nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El campo apellido es obligatorio")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "El campo email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    public string Email { get; set; }

    public string Rol { get; set; }
    public IFormFile? AvatarFile { get; set;}
    public bool BorrarAvatar {get; set;}
    public string? NewPassword {get; set;} 
    public string? ConfirmPassword {get;set;}
    public string? Avatar { get; set; }
}