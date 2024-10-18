using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyetoInmobiliaria.Models;
public class Contrato{
    [Key]
    public int IdContrato {get; set;}

    [Required(ErrorMessage = "El campo Inquilino es obligatorio.")]
    public int IdInquilino {get; set;}
    
    [Required(ErrorMessage = "El campo Inmueble es obligatorio.")]
    public int IdInmueble {get; set;}
    [ForeignKey("IdInquilino")]

    public Inquilino? inquilino {get; set;}
    [ForeignKey("IdInmueble")]
    
    public Inmueble? inmueble {get; set;}

    [Required(ErrorMessage = "El Monto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor a 0.")]
    public Double Monto {get; set;}

    [Required(ErrorMessage = "La Fecha de Inicio es obligatoria.")]
    public DateTime FechaInicio {get; set;}

    [Required(ErrorMessage = "La Fecha de fin es obligatoria.")]
    [DateRange(ErrorMessage = "La fecha de fin debe ser mayor que la fecha de inicio.")]
    public DateTime FechaFin {get; set;}
    public DateTime FechaAnulacion {get; set;}
    public Boolean Estado {get; set;}
}
public class DateRangeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var contrato = (Contrato)validationContext.ObjectInstance;

        if (contrato.FechaFin <= contrato.FechaInicio)
        {
            return new ValidationResult(ErrorMessage ?? "La fecha de fin debe ser mayor que la fecha de inicio.");
        }

        return ValidationResult.Success;
    }
}