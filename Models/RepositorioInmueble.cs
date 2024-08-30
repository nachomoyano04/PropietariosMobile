using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProyetoInmobiliaria.Models;

public class RepositorioInmueble
{
    private readonly InmobiliariaContext _context;

    public RepositorioInmueble(InmobiliariaContext context)
    {
        _context = context;
    }

    public int Crear(Inmueble inmueble)
    {
        _context.Inmuebles.Add(inmueble);
        _context.SaveChanges();
        return inmueble.IdInmueble;
    }

    public Inmueble Obtener(int id)
    {
        return _context.Inmuebles.Find(id);
    }

    public void Modificar(Inmueble inmueble)
    {
        _context.Inmuebles.Update(inmueble);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        Inmueble inmueble = _context.Inmuebles.Find(id);
        if (inmueble != null)
        {
            inmueble.Estado = false;
            _context.Inmuebles.Update(inmueble);
            _context.SaveChanges();
        }
    }

    public List<Inmueble> Listar()
    {
        return _context.Inmuebles.Where(i => i.Estado).ToList();
    }
}