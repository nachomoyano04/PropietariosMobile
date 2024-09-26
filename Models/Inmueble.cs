using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Inmueble{
    [Key]
    public int IdInmueble {get; set;}

    [Required(ErrorMessage = "Debe elegir un propietario")]
    public int IdPropietario {get; set;}
    
    public int IdDireccion {get; set;}
    
    [Required(ErrorMessage = "Debe elegir un tipo de inmueble.")]
    public int IdTipo {get; set;}

    public Propietario? propietario {get; set;}
    public Direccion? direccion {get; set;} 
    public Tipo? tipo {get; set;}
    
    [Required(ErrorMessage = "Los metros cuadrados son obligatorios.")]
    [Range(1, double.MaxValue, ErrorMessage = "Los metros cuadrados deben ser mayores que 0.")]
    public string Metros2 {get; set;}
    
    [Required(ErrorMessage = "Debe especificar la cantidad de ambientes")]
    public int CantidadAmbientes {get; set;}
    
    public Boolean Disponible {get; set;}
    
    [Required(ErrorMessage = "Debe ingresar el precio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
    public Decimal Precio {get; set;}
    
    [Required(ErrorMessage = "Debe ingresar una descripción.")]
    public string Descripcion {get; set;}
    public Boolean Cochera {get; set;}
    public Boolean Piscina {get; set;}
    public Boolean Mascotas {get; set;}
    public Boolean Estado {get; set;}
    
    [Required(ErrorMessage = "Debe ingresar una url para la imagen...")]
    public string UrlImagen {get; set;}
}