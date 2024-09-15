//repositorioUsuario
using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
using BCryptNet = BCrypt.NET.BCrypt;
public class RepositorioUsuario:RepositorioBase{
    public RepositorioUsuario repoUsuario;
    public RepositorioUsuario():base(){
        repoUsuario = new RepositorioUsuario();
    }

    public Usuario Guardar(Usuario usuario){
        int  idCreado=-1;
        using(MySqlConnection connection= new MySqlConnection(ConnectionString)){
            connection.Open();
            string query="INSERT INTO usuarios (correo,contraseñaa,rol,avatar,estado)"+
            "VALUES(@EmailUsuario,@PasswordUsuario,@RolUsuario,@AvatarUsuario,@EstadoUsuario)";

            using (MySqlCommand command=  new MySqlCommand(query,connection)){
                command.Parameters.AddWithValue("@EmailUsuario", usuario.Email);
                command.Parameters.AddWithValue("@PasswordUsuario", encriptar(usuario.Password));
                command.Parameters.AddWithValue("@RolUsuario",usuario.Rol);
                command.Parameters.AddWithValue("@AvatarUsuario",usuario.Avatar);
                command.Parameters.AddWithValue("@EsadoUsuario",usuario.Estado);
                idCreado=  Convert.ToInt32(command.ExecuteScalar());
            };
            

        }
        return idCreado;
        
    }
    public string encriptar(string pass){
        int workFactor= 12;
        string salt=  BCryptNet.BCrypt.GenerateSalt(workFactor);
        string hashedPassword=  BCryptNet.BCrypt.HashPassword(pass,salt);
        return hashedPassword;
    }
}
