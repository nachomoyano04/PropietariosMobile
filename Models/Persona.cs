public class Persona{
    
    public int Dni {get; set;}
    public string Apellido {get; set;}
    public string Nombre {get; set;}
    public string Telefono {get; set;}
    public string Correo {get; set;}
    public bool Estado {get; set;}

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