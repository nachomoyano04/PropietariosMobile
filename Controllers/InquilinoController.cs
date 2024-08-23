// using Microsoft.AspNetCore.Mvc;
// using ProyetoInmobiliaria.Models;

// namespace ProyetoInmobiliaria.Controllers{
//     private RepositorioInquilino repo;
//     public class InquilinoController : Controller{
//         //Metodo get de todos los inquilinos para el Index
//         public IActionResult Index(){
//             var inquilinos = repo.getInquilinos();
//             return View(inquilinos);
//         }

//         //Metodo get inquilino segun el id para vista Editar
//         public IActionResult Editar(int Id){
//             if(Id == 0){
//                 return View();
//             }else{
//                 return View(repo.getInquilino(Id));
//             }
//         }

//         //Metodo que guarda segun si es para editar un inquilino o para crear uno nuevo y redirige al Index
//         [HttpPost]
//         public IActionResult Guardar(int Id, Inquilino inquilino){
//             Id = inquilino.IdInquilino;
//             if(Id == 0){
//                 repo.DarDeAlta(inquilino);
//             }else{
//                 repo.Modificar(inquilino);
//             }
//             return RedirectToAction("Index");
//         }

//         //Metodo que borra un inquilino segun el id y redirige al Index
//         [HttpPost]
//         public IActionResult Borrar(int Id){
//             repo.DarDeBaja(Id);
//             return RedirectToAction("Index");
//         }    
//     }
// }