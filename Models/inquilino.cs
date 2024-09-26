using System.ComponentModel.DataAnnotations;

namespace ProyetoInmobiliaria.Models;
public class Inquilino
{
    [Key]
    public int IdInquilino {get; set;}
    [Required(ErrorMessage = "El DNI es obligatorio")]
    public string? Dni {get; set;}
    [Required(ErrorMessage = "El Apellido es obligatorio")]
    public string? Apellido {get; set;}
    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string? Nombre {get; set;}
    [Required(ErrorMessage = "El Telefono es obligatorio")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
    public string? Telefono {get; set;}
    [Required(ErrorMessage = "El Correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    public string? Correo {get; set;}
    public bool Estado {get; set;}
}