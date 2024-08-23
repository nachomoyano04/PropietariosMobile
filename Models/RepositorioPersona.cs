// public class RepositorioPersona {
//     public RepositorioPersona(){
//         private readonly InmobiliariaContext _context;

//         public RepositorioPropietario(){
//             _context=new InmobiliariaContext();
//         }

//         public int AgregarPersona(Persona persona){
//             if(persona is Inquilino){
//                 _context.Inquilino.add(persona);
//             }else if(persona is Propietario){
//                 _context.Propietario.add(persona);
//             }else{
//                 return 0;
//             }

//             _context.SabeChanges();

//             return persona.Id;
//         }
//         public Persona ObtenerPersona(int id){
//             return _context.
//         }
//     }
    
// }