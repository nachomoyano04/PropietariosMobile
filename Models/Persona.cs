public class Persona{
    private int dni {get; set;}
    private string apellido {get; set;}
    private string nombre {get; set;}
    private string telefono {get; set;}
    private string correo {get; set;}
    private bool estado {get; set;}

    public Persona(){

    }
    public Persona(int dni,string apellido,string nombre,string telefono,string correo, bool estado){
        this.dni=dni;
        this.apellido=apellido;
        this.nombre=nombre;
        this.telefono=telefono;
        this.correo= correo;
        this.estado=estado;
    }
    
}