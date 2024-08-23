using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class RepositorioInquilino
{
    private readonly InmobiliariaContext _context;

    public RepositorioInquilino()
    {
        _context = new InmobiliariaContext();
    }

    public int AgregarInquilino(Inquilino inquilino)
    {
        _context.Inquilinos.Add(inquilino); // Asegúrate de que la propiedad sea plural: Inquilinos
        _context.SaveChanges();
        return inquilino.Id;
    }

    public Inquilino ObtenerInquilino(int id)
    {
        return _context.Inquilinos.Find(id); // Debería ser Inquilinos
    }

    public void ActualizarInquilino(Inquilino inquilino)
    {
        _context.Inquilinos.Update(inquilino); // Debería ser Inquilinos
        _context.SaveChanges();
    }

    public void EliminarInquilino(int id)
    {
        Inquilino inquilino = _context.Inquilinos.Find(id); // Debería ser Inquilinos
        if (inquilino != null)
        {
            _context.Inquilinos.Remove(inquilino); // Debería ser Inquilinos
            _context.SaveChanges();
        }
    }

    public List<Inquilino> ObtenerInquilinos()
    {
        return _context.Inquilinos.ToList(); // Debería ser Inquilinos
    }
}
