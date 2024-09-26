//repositorioUsuario
using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
using BCrypt.Net;
using System.Security.Claims;

public class RepositorioUsuario:RepositorioBase{
public RepositorioUsuario(): base(){  

    }

    public int Guardar(Usuario usuario){

        int  idCreado=-1;
        using(MySqlConnection connection= new MySqlConnection(ConnectionString)){
            connection.Open();
            string query="INSERT INTO usuario (email,password,rol,avatar,nombre,apellido,estado)"+
            "VALUES(@EmailUsuario,@PasswordUsuario,@RolUsuario,@AvatarUsuario,@NombreUsuario,@ApellidoUsuario,@EstadoUsuario)";

            using (MySqlCommand command=  new MySqlCommand(query,connection)){
                command.Parameters.AddWithValue("@EmailUsuario", usuario.Email);
                command.Parameters.AddWithValue("@PasswordUsuario", encriptar(usuario.Password));
                command.Parameters.AddWithValue("@RolUsuario",usuario.Rol);
                command.Parameters.AddWithValue("@AvatarUsuario",usuario.Avatar);
                command.Parameters.AddWithValue("@NombreUsuario",usuario.Nombre);
                command.Parameters.AddWithValue("@ApellidoUsuario",usuario.Apellido);
                command.Parameters.AddWithValue("@EstadoUsuario","1");
                idCreado=  Convert.ToInt32(command.ExecuteScalar());
            };
            

        }
        return idCreado;
        
    }
    public string encriptar(string pass){
        int workFactor= 12;
        string salt=  BCrypt.Net.BCrypt.GenerateSalt(workFactor);
        string hashedPassword=  BCrypt.Net.BCrypt.HashPassword(pass,salt);
        return hashedPassword;
    }



    // listar todos los usuarios activos
    public List<Usuario> Listar(){
        List<Usuario> usuarios = new List<Usuario>();
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM usuario WHERE estado = true"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                using (MySqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        Usuario usuario = new Usuario{
                            IdUsuario = reader.GetInt32("idUsuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Rol = reader.GetString("rol"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Estado = reader.GetBoolean("estado")
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
        }
        return usuarios;
    }

    public List<Usuario> ListarDadosDeBaja(){
        List<Usuario> usuarios = new List<Usuario>();
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM usuario WHERE estado = false"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                using (MySqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        Usuario usuario = new Usuario{
                            IdUsuario = reader.GetInt32("idUsuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Rol = reader.GetString("rol"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Estado = reader.GetBoolean("estado")
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
        }
        return usuarios;
    }



    public Usuario Obtener(int id){
        Usuario usuario = null;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT * FROM usuario WHERE idUsuario = @IdUsuario AND estado = true"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdUsuario", id);
                using (MySqlDataReader reader = command.ExecuteReader()){
                    if (reader.Read()){
                        usuario = new Usuario{
                            IdUsuario = reader.GetInt32("idUsuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Rol = reader.GetString("rol"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Estado = reader.GetBoolean("estado")
                        };
                    }
                }
            }
        }
        return usuario;
    }

    //  actualizar un usuario 
    public int Actualizar(Usuario usuario, string? nuevaPassword = null){
        int filasAfectadas = 0;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = @"UPDATE usuario 
                      SET email = @EmailUsuario, rol = @RolUsuario, avatar = @AvatarUsuario, 
                          nombre = @NombreUsuario, apellido = @ApellidoUsuario ";
            if (!string.IsNullOrWhiteSpace(nuevaPassword)){
                query += ", password = @PasswordUsuario ";
            }
            query += "WHERE idUsuario = @IdUsuario";
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@EmailUsuario", usuario.Email);
                if (!string.IsNullOrWhiteSpace(nuevaPassword)){
                    command.Parameters.AddWithValue("@PasswordUsuario", encriptar(nuevaPassword));
                }
                command.Parameters.AddWithValue("@RolUsuario", usuario.Rol);
                command.Parameters.AddWithValue("@AvatarUsuario", usuario.Avatar);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.Nombre);
                command.Parameters.AddWithValue("@ApellidoUsuario", usuario.Apellido);
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    // eliminar un usuario
    public int Eliminar(int id){
        int filasAfectadas = 0;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE usuario SET estado = false WHERE idUsuario = @IdUsuario"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdUsuario", id);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    // dar de alta un usuario
    public int Alta(int id){
        int filasAfectadas = 0;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE usuario SET estado = true WHERE idUsuario = @IdUsuario"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdUsuario", id);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }
}
