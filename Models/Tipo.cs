using System.ComponentModel.DataAnnotations;
public class Tipo{
    [Key]
    public int IdTipo{get; set;}
    [Required(ErrorMessage = "Debe ingresar una observación.")]
    public string Observacion{get; set;}
    public override string ToString(){
        return Observacion;
    }
}