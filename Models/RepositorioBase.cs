public abstract class RepositorioBase{
    protected readonly IConfiguration configuration;
    protected readonly string ConnectionString;

    protected RepositorioBase(IConfiguration configuration){
        this.configuration = configuration;
        ConnectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }
}