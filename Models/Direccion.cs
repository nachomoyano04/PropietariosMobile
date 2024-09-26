using System.ComponentModel.DataAnnotations;

public class Direccion{
    [Key]
    public int IdDireccion {get; set;}

    [Required(ErrorMessage = "Debe ingresar una calle.")]
    public string Calle {get; set;}

    [Required(ErrorMessage = "Debe ingresar una altura.")]
    public int Altura {get; set;}

    [Required(ErrorMessage = "Debe ingresar un codigo postal.")]
    public string Cp {get; set;}

    [Required(ErrorMessage = "Debe ingresar una ciudad.")]
    public string Ciudad {get; set;}

    [Required(ErrorMessage = "Debe ingresar coordenadas.")]
    public string Coordenadas {get; set;}
}