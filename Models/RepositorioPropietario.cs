using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class RepositorioPropietario{
    public readonly InmobiliariaContext _context;

    public RepositorioPropietario(){
        _context= new InmobiliariaContext();
    }

     public int crearPropietario(Propietario propietario){
        _context.Propietarios.add(propietario);

        _context.SaveChanges();
        return propietario.getId();
    }

    public Propietario ObtenerInquilino(int id){
        return _context.Propietarios.Find(id);
    }

    public void ActualizarInquilino(Propietario propietario){
        _context.Propietarios.Update(propietario);
        _context.SaveChanges();
    }

    public void darDeBaja(int id){
        Propietario i = _context.Propietarios.Find(id);
        _context.Propietario.Remove(i);
        _context.SaveChanges();
    }
    public List<Propietario> obtenerPropietarios(){
        return _context.Propietarios.ToList();
    }
}