public class RepositorioTipo{
    private readonly InmobiliariaContext context;

    public RepositorioTipo(){
        context = new InmobiliariaContext();
    }

    //Crear
    public int Crear(Tipo tipo){
        var entidadCreada = context.Add(tipo);
        context.SaveChanges();
        return entidadCreada.Entity.IdTipo;
    } 
    
    //Modificar
    public int Modificar(Tipo tipo){
        context.Update(tipo);
        int filasAfectadas = context.SaveChanges();
        return filasAfectadas;
    }

    //Listar
    public List<Tipo> Listar(){
        List<Tipo> tipos = context.Tipo.ToList();
        return tipos;
    }
    
    //Obtener
    public Tipo Obtener(int IdTipo){
        return context.Tipo.Find(IdTipo);
    }

    //Eliminar
    public int Eliminar(int IdTipo){
        Tipo tipo = Obtener(IdTipo);
        int filasAfectadas = 0;
        if(tipo != null){
            context.Remove(tipo);
            filasAfectadas = context.SaveChanges();
        }
        return filasAfectadas;
    }
}