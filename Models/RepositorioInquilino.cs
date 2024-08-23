public class RepositorioInquilino {
    public readonly InmobiliariaContext _context;

    public RepositorioInquilino(){
        _context= new InmobiliariaContext();
    }

    public int AgregarInquilino(Inquilino inquilino){
        _context.Inquilino.add(inquilino)

        _context.SaveChanges();
        return inquilino.Id;
    }

    public Inquilino ObtenerInquilino(int id){
        return _context.Inquilino.Find(id);
    }

    public void ActualizarInquilino(Inquilino inquilino){
        _context.Inquilino.Update(inquilino);
        _context.SaveChanges();
    }

    public void EliminarInquilino(int id){
        Inquilino i = _context.Inquilino.Find(id);
        _context.Inquilino.Remove(i);
        _context.SaveChanges();
    }
    public List<Inquilino> ObtenerInquilinos(){
        return _context.Inquilino.ToList();
    }

}