public class RepositorioDireccion{
    private readonly InmobiliariaContext context;

    public RepositorioDireccion(){
        context = new InmobiliariaContext();
    }

    //Crear
    public int Crear(Direccion direccion){
        var entidadCreada = context.Add(direccion);
        context.SaveChanges();
        return entidadCreada.Entity.IdDireccion;
    }

    //Modificar
    public int Modificar(Direccion direccion){
        context.Update(direccion);
        int filasAfectadas = context.SaveChanges();
        return filasAfectadas;
    }

    //Listar
    public List<Direccion> Listar(){
        return context.Direccion.ToList();
    }

    //Obtener
    public Direccion Obtener(int IdDireccion){
        return context.Direccion.Find(IdDireccion);
    }

    //Eliminar
    public int Eliminar(int IdDireccion){
        Direccion direccion = Obtener(IdDireccion);
        int filasAfectadas = 0;
        if(direccion != null){
            context.Remove(direccion);
            filasAfectadas = context.SaveChanges();
        }
        return filasAfectadas;
    }

}