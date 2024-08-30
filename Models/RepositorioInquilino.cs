//repositorioInquilino.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProyetoInmobiliaria.Models;

public class RepositorioInquilino
{
    private readonly InmobiliariaContext _context;

    public RepositorioInquilino()
    {
        _context = new InmobiliariaContext();
    }

    public int Crear(Inquilino inquilino)
    {
        _context.Inquilino.Add(inquilino); // Asegúrate de que la propiedad sea plural: Inquilino
        _context.SaveChanges();
        return inquilino.IdInquilino;
    }

    public Inquilino Obtener(int id)
    {
        return _context.Inquilino.Find(id); // Debería ser Inquilino
    }

    public void Modificar(Inquilino inquilino)
    {
        _context.Inquilino.Update(inquilino); // Debería ser Inquilino
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        Inquilino inquilino = _context.Inquilino.Find(id); // Debería ser Inquilino
        if (inquilino != null)
        {
            inquilino.Estado = false;
            _context.Inquilino.Update(inquilino); // Debería ser Inquilino
            _context.SaveChanges();
        }
    }

    public List<Inquilino> Listar()
    {
        return _context.Inquilino.Where(i => i.Estado==true).ToList(); // Debería ser Inquilino
    }
}
