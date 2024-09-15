using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
public class PagoController:Controller{
    private RepositorioPago repo = new RepositorioPago();
    private RepositorioContrato repoContrato = new RepositorioContrato();
    public IActionResult Index(){
        var pago = repo.Listar();
        if(pago == null){
            pago = new List<Pago>();
        }
        return View(pago);
    }
    public IActionResult Detalle(int id){
        var pago = repo.Obtener(id);
        if(pago == null){
            pago = new Pago();
        }
        return View(pago);
    }
    public IActionResult Crear(){
        PagoViewModel pvm = new PagoViewModel{
            Contratos = repoContrato.Listar(),
            Pago = new Pago()
        };
        return View(pvm);
    }
    public IActionResult Editar(int id){
        var pago = repo.Obtener(id);
        if(pago == null){
            pago = new Pago();
        }
        return View(repo.Obtener(id));
    }

    [HttpPost]
    public IActionResult Guardar(int id, Pago pago){
        if(id == 0){
            repo.Crear(pago);
        }else{
            repo.Modificar(pago);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Borrar(int id){
        if(repo.Eliminar(id) == 1){
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index", "Home");
    }

}