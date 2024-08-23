using Microsoft.EntityFrameworkCore;
public class InmobiliariaContext : DbContext {
    public DbSet<Inquilino> Inquilinos {get; set;}
    public DbSet<Propietario> Propietarios {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBulder){
        optionsBulder.UseMsql("server=localhost; database=inmobiliaria;user=root;password=", new MySqlServerVersion(new Version(8,4,0)));
    }

    

    
}