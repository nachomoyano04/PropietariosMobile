//repositorioUsuario
using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
public class RepositorioUsuario:RepositorioBase{
    public RepositorioUsuario repoUsuario;
    public RepositorioUsuario():base(){
        repoUsuario = new RepositorioUsuario();
    }
}
