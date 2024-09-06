using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Contrato{
    [Key]
    public int IdContrato {get; set;}
    public int IdInquilino {get; set;}
    public int IdInmueble {get; set;}
    public Inquilino inquilino {get; set;}
    public Inmueble inmueble {get; set;}
    public Double Monto {get; set;}
    public DateTime FechaInicio {get; set;}
    public DateTime FechaFin {get; set;}
    public DateTime FechaAnulacion {get; set;}
    public Boolean Estado {get; set;}
}