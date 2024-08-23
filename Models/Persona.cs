public class Persona{
    private int Id {get; set;}
    private int Dni {get; set;}
    private string Apellido {get; set;}
    private string Nombre {get; set;}
    private string Telefono {get; set;}
    private string Correo {get; set;}
    private bool Estado {get; set;}

    public Persona(){

    }
    public Persona(int dni,string apellido,string nombre,string telefono,string correo, bool estado){
        this.Dni=dni;
        this.Apellido=apellido;
        this.Nombre=nombre;
        this.Telefono=telefono;
        this.Correo= correo;
        this.Estado=estado;
    }
    
}