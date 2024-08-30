public class RepositorioInmueble2{
    private readonly InmobiliariaContext context;

    public RepositorioInmueble2(){
        context = new InmobiliariaContext();
    }

    //Crear
    public int Crear(Inmueble inmueble){
        var entidadCreada = context.Add(inmueble);
        context.SaveChanges();
        return entidadCreada.Entity.IdInmueble;
    }    

    //Modificar
    public int Modificar(Inmueble inmueble){
        context.Update(inmueble);
        int filasAfectadas = context.SaveChanges();
        return filasAfectadas;
    }

    //Listar
    public List<Inmueble> Listar(){
        return context.Inmueble.Where(i => i.Estado).ToList();
    }

    //Obtener
    public Inmueble Obtener(int IdInmueble){
        return context.Inmueble.Find(IdInmueble);
    }

    //Eliminar
    public int Eliminar(int IdInmueble){
        Inmueble inmueble = Obtener(IdInmueble);
        if(inmueble != null){
            inmueble.Estado = false;
            return Modificar(inmueble);
        }
        return -1;
    }   
}