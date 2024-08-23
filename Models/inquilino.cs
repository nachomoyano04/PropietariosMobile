public class Inquilino: Persona
{
    private int idInquilino {get; set;}
    
    public Inquilino(){}
    public Inquilino(int id,int dni,string apellido, string nombre,string telefono, string correo, bool estado){
        this.idInquilino=id;
        base(dni,apellido,nombre,telefono,correo,estado);
    }
}