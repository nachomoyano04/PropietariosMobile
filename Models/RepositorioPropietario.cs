// public class RepositorioPropietario{
//     public readonly InmobiliariaContext _context;

//     public RepositorioPropietario(){
//         _context= new InmobiliariaContext();
//     }

//      public int crearPropietario(Propietario propietario){
//         _context.Propietario.add(propietario)

//         _context.SaveChanges();
//         return propietario.Id;
//     }

//     public Propietario ObtenerInquilino(int id){
//         return _context.Propietario.Find(id);
//     }

//     public void ActualizarInquilino(Propietario propietario){
//         _context.Propietario.Update(propietario);
//         _context.SaveChanges();
//     }

//     public void darDeBaja(int id){
//         Propietario i = _context.Propietario.Find(id);
//         _context.Propietario.Remove(i);
//         _context.SaveChanges();
//     }
//     public List<Propietario> obtenerPropietarios(){
//         return _context.Propietario.ToList();
//     }
// }
using MySql.Data.MySqlClient;
namespace ProyetoInmobiliaria.Models;
public class RepositorioPropietario{
    string ConnectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    
    public List<Propietario> GetPropietarios(){
        List<Propietario> propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            var query = "SELECT * FROM propietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                connection.Open();
                var reader = command.ExecuteReader();
                while(reader.Read()){
                    propietarios.Add(new Propietario{
                        IdPropietario = reader.GetInt32("idPropietario"),
                        Apellido= reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        Dni = reader.GetString("dni"),
                        Telefono = reader.GetString("Telefono"),
                        Correo = reader.GetString("correo")
                    });
                }
                connection.Close();
            }
            return propietarios;
        }
    }
    public Propietario? GetPropietario(int Id){
        Propietario? propietario = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            var query = $@"SELECT * FROM propietario WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", Id);
                connection.Open();
                var reader = command.ExecuteReader();
                if(reader.Read()){
                    propietario = new Propietario{
                        IdPropietario = reader.GetInt32("IdPropietario"),
                        Apellido= reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        Dni = reader.GetString("Dni"),
                        Telefono = reader.GetString("Telefono"),
                        Correo = reader.GetString("Correo")
                    };
                }
                connection.Close();
            }
            return propietario;
        }
    }
    public int DarDeAlta(Propietario propietario){
        int respuesta = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            var query = $@"INSERT INTO propietario (dni, apellido, nombre, telefono, correo, estado) VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo, 1);SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Correo", propietario.Correo);
                connection.Open();
                respuesta = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return respuesta;
    }


    public int Modificar(Propietario propietario){
        int respuesta = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            var query = $@"UPDATE propietario SET dni = @Dni, apellido = @Apellido, nombre = @Nombre, telefono = @Telefono, correo = @Correo WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Correo", propietario.Correo);
                command.Parameters.AddWithValue("@IdPropietario", propietario.IdPropietario);
                connection.Open();
                respuesta = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return respuesta;
    }

    public int DarDeBaja(int Id){
        int respuesta = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            var query = $@"UPDATE propietario SET(estado = 0) WHERE (idPropietario = @IdPropietario)";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", Id);
                connection.Open();
                respuesta = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return respuesta;
    }
}