using ProyetoInmobiliaria.Models;

public class InmuebleDireccion{
    public string Inmueble {get; set;}
    public string Direccion {get; set;}
    public override string ToString(){
        return Direccion;
    }


}

