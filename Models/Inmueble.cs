using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyetoInmobiliaria.Models;
public class Inmueble{
    [Key]
    public int IdInmueble {get; set;}

    [ForeignKey("IdPropietario")]
    public Propietario? propietario {get; set;}
    [Required(ErrorMessage = "Debe elegir un propietario")]
    public int IdPropietario {get; set;}
    
    [ForeignKey("IdDireccion")]
    public Direccion? direccion {get; set;} 
    public int IdDireccion {get; set;}
    
    public string tipo {get; set;}
    public string Uso {get; set;} = "";
    
    [Required(ErrorMessage = "Los metros cuadrados son obligatorios.")]
    [Range(1, double.MaxValue, ErrorMessage = "Los metros cuadrados deben ser mayores que 0.")]
    public string Metros2 {get; set;}
    
    [Required(ErrorMessage = "Debe especificar la cantidad de ambientes")]
    public int CantidadAmbientes {get; set;}
    
    public bool Disponible {get; set;}
    
    [Required(ErrorMessage = "Debe ingresar el precio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
    public decimal Precio {get; set;}
    
    [Required(ErrorMessage = "Debe ingresar una descripción.")]
    public string Descripcion {get; set;}
    public bool Cochera {get; set;}
    public bool Piscina {get; set;}
    public bool Mascotas {get; set;}
    public bool Estado {get; set;}    
    public string UrlImagen {get; set;}
}