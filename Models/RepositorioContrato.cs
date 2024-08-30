using ProyetoInmobiliaria.Models;
public class RepositorioContrato2{
    private readonly InmobiliariaContext context;

    public RepositorioContrato2(){
        context = new InmobiliariaContext();
    }

    //Crear
    public int Crear(Contrato contrato){
        var entidadCreada = context.Add(contrato);
        context.SaveChanges();
        return entidadCreada.Entity.IdContrato;
    }

    //Modificar
    public int Modificar(Contrato contrato){
        context.Update(contrato);
        int filasAfectadas = context.SaveChanges();
        return filasAfectadas;
    }

    //Listar
    public List<Contrato> Listar(){
        var contratos = context.Contrato.Where(e => e.Estado).ToList(); 
        return contratos;
    }

    //Obtener
    public Contrato Obtener(int IdContrato){
        return context.Contrato.Find(IdContrato);
    }

    //Eliminar
    public int Eliminar(int IdContrato){
        Contrato contrato = Obtener(IdContrato);
        if(contrato != null){
            contrato.Estado = false;
            return Modificar(contrato);
        }
        return -1;
    }
}