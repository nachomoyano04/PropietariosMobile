using System.ComponentModel.DataAnnotations;
public class Usuario {
    [Key]
    public int IdUsuario  { get; set; }

    [Required(ErrorMessage = "El campo nombre es obligatorio")]
    public string Nombre  { get; set; }

    [Required(ErrorMessage = "El campo apellido es obligatorio")]
    public  string Apellido  { get; set; }

    [Required(ErrorMessage = "El campo email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    public string Email   {get; set;}

    [Required(ErrorMessage = "El campo password es obligatorio")]
    public string Password  {get; set;}
    public string Rol {get; set;}
    
    [Required(ErrorMessage = "El campo password es obligatorio")]
    public string Avatar {get; set;}
    public bool  Estado {get; set;}
}