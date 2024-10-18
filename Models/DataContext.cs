using Microsoft.EntityFrameworkCore;
using ProyetoInmobiliaria.Models;

public class DataContext:DbContext{
    public DataContext(DbContextOptions<DataContext> options):base(options){

    }
    public DbSet<Propietario> Propietario {get;set;}
    public DbSet<Inquilino> Inquilino {get;set;}
    public DbSet<Inmueble> Inmueble {get;set;}
    public DbSet<Contrato> Contrato {get;set;}
    public DbSet<Pago> Pago {get; set;}
    public DbSet<Direccion> Direccion {get; set;}
}