public class RepositorioPropietario{
    public readonly InmobiliariaContext _context;

    public RepositorioPropietario(){
        _context= new InmobiliariaContext();
    }

     public int crearPropietario(Propietario propietario){
        _context.Propietario.add(propietario)

        _context.SaveChanges();
        return propietario.Id;
    }

    public Propietario ObtenerInquilino(int id){
        return _context.Propietario.Find(id);
    }

    public void ActualizarInquilino(Propietario propietario){
        _context.Propietario.Update(propietario);
        _context.SaveChanges();
    }

    public void darDeBaja(int id){
        Propietario i = _context.Propietario.Find(id);
        _context.Propietario.Remove(i);
        _context.SaveChanges();
    }
    public List<Propietario> obtenerPropietarios(){
        return _context.Propietario.ToList();
    }
}