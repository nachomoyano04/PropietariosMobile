using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Pago{
    [Key]
    public int IdPago{get; set;}
    public int IdContrato{get;set;}
    [ForeignKey("IdContrato")]
    public Contrato? contrato{get;set;}
    
    [Required(ErrorMessage = "La fecha de pago es obligatoria")]
    public DateTime FechaPago{get;set;}
    
    [Required(ErrorMessage = "El importe es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El importe debe ser mayor que 0.")]
    public decimal Importe{get;set;}
    
    [Required(ErrorMessage = "El numero de pago es obligatorio")]
    public string NumeroPago{get;set;}

    [Required(ErrorMessage = "El detalle es obligatorio")]
    public string Detalle{get;set;}
    public bool Estado{get;set;}
}