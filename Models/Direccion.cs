using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Direccion{
    [Key]
    public int IdDireccion {get; set;}

    [Required(ErrorMessage = "Debe ingresar una calle.")]
    public string Calle {get; set;}

    [Required(ErrorMessage = "Debe ingresar una altura.")]
    public int Altura {get; set;}

    [Required(ErrorMessage = "Debe ingresar una ciudad.")]
    public string Ciudad {get; set;}

    // public string Cp {get; set;}
    // public string Coordenadas {get; set;}
}