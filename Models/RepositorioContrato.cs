using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProyetoInmobiliaria.Models;

public class RepositorioContrato
{
    private readonly InmobiliariaContext _context;

    public RepositorioContrato(InmobiliariaContext context)
    {
        _context = context;
    }

    public int Crear(Contrato contrato)
    {
        _context.Contratos.Add(contrato);
        _context.SaveChanges();
        return contrato.IdContrato;
    }

    public Contrato Obtener(int id)
    {
        return _context.Contratos.Find(id);
    }

    public void Modificar(Contrato contrato)
    {
        _context.Contratos.Update(contrato);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        Contrato contrato = _context.Contratos.Find(id);
        if (contrato != null)
        {
            contrato.Estado = false;
            _context.Contratos.Update(contrato);
            _context.SaveChanges();
        }
    }

    public List<Contrato> Listar()
    {
        return _context.Contratos.Where(c => c.Estado).ToList();
    }
}