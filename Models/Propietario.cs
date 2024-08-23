// kpublic class Propietario: Persona{
//     private int id {get; set;}

//     public Propietario(int id, int dni, string apellido,string nombre, string telefono,string correo,bool estado){
//         this.id=id;
//         base(dni,apellido,nombre, telefono,correo,estado);
//     }
//     public Propietario( int dni, string apellido,string nombre, string telefono,string correo,bool estado){

//         base(dni,apellido,nombre, telefono,correo,estado);
//     }

// }
namespace ProyetoInmobiliaria.Models;
public class Propietario{
    public int IdPropietario { get; set; }
    public string Apellido { get; set; } = "";
    public string Nombre { get; set;} = "";
    public string Dni { get; set;} = "";
    public string Telefono { get; set; } = "";
    public string Correo { get; set; } = "";
}