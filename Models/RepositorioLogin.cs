//repositorioLogin

using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;

public class RepositorioLogin : RepositorioBase
{
    public RepositorioLogin() : base() { //llamo repo base

    }

    public Usuario Verificar(LoginViewModel model) // acepto objetp
    {
        Usuario usuarioEncontrado = null;

        try
        {
            _connection.Open(); // heredo conexion del repo base

            string query = "SELECT * FROM usuarios WHERE email = @email";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@email", model.Email);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // busca datos, si encuentra devuelve true
            {
                string passwordHash = reader["password"].ToString(); // recupero passs
                if (VerifyPasswordHash(model.Password, passwordHash))
                {
                    usuarioEncontrado = new Usuario //  pass correcto creo objeto y lo paso
                    {
                        Email = reader["email"].ToString(),
                        Rol = reader["rol"].ToString()
                    };
                }
            }

            reader.Close(); // cierro conex
        }
        catch (Exception ex)
        {
            throw new Exception("Error al verificar usuario", ex); // MANEJO ERROR
        }
        finally
        {
            _connection.Close();
        }

        return usuarioEncontrado;
    }

    private bool VerificarHashPassword(string passwordProporcionada, string hashAlmacenado)
    {
        // Usar BCrypt para verificar la contraseña
        return BCrypt.Net.BCrypt.Verify(passwordProporcionada, hashAlmacenado);
    }

}
