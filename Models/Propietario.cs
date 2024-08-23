public class Propietario: Persona{
    private int id {get; set;}

    public Propietario(int id, int dni, string apellido,string nombre, string telefono,string correo,bool estado){
        this.id=id;
        base(dni,apellido,nombre, telefono,correo,estado);
    }
    public Propietario( int dni, string apellido,string nombre, string telefono,string correo,bool estado){
        
        base(dni,apellido,nombre, telefono,correo,estado);
    }

}