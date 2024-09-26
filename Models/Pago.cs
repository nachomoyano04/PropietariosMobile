using System.ComponentModel.DataAnnotations;
public class Pago{
    [Key]
    public int IdPago{get; set;}
    public int IdContrato{get;set;}
    public Contrato? contrato{get;set;}
    
    [Required(ErrorMessage = "La fecha de pago es obligatoria")]
    public DateTime FechaPago{get;set;}
    
    [Required(ErrorMessage = "El importe es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El importe debe ser mayor que 0.")]
    public Decimal Importe{get;set;}
    
    [Required(ErrorMessage = "El numero de pago es obligatorio")]
    public string NumeroPago{get;set;}

    [Required(ErrorMessage = "El detalle es obligatorio")]
    public string Detalle{get;set;}
    public bool Estado{get;set;}
}