using Microsoft.EntityFrameworkCore;
public class InmobiliariaContext : DbContext {
    public DbSet<Inquilino> Inquilino {get; set;}
    public DbSet<Propietario> Propietario {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBulder){
        optionsBulder.UseMsql("server=localhost; database=inmobiliaria;user=root;password=", new MySqlServerVersion(new Version(8,4,0)));
    }

    

    
}