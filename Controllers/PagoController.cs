using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
public class PagoController:Controller{
    private RepositorioPago repo = new RepositorioPago();
    private RepositorioContrato repoContrato = new RepositorioContrato();
    public IActionResult Index(int Id){
        var pagos = repo.ListarPorContrato(Id);
        var contratos = repoContrato.Listar();
        PagoContrato pc = new PagoContrato{
            Pagos = pagos,
            Contratos = contratos,
            IdContrato = Id,
        };
        return View(pc);
    }
    public IActionResult Detalle(int id){
        var pago = repo.Obtener(id);
        if(pago != null){
            return View(pago);
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Crear(int Id){
        Contrato c = repoContrato.Obtener(Id);
        if(c != null){
            Pago p = new Pago{
                IdContrato = Id,
                contrato = c
            };
            return View(p);
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Editar(int id){
        var pago = repo.Obtener(id);
        if(pago != null){
            return View(pago);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Guardar(Pago pago){
        if(pago.IdPago == 0){
            repo.Crear(pago);
        }else{
            repo.Modificar(pago);
        }
        return RedirectToAction("Index", "Pago", new { id = pago.IdContrato });
    }
    [HttpPost]
    public IActionResult Borrar(int Id){
        Console.WriteLine(Id);
        int filasAfectadas = repo.Eliminar(Id);
        if(filasAfectadas == 1){
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index", "Home");
    }

    public JsonResult GetPorContrato(int IdContrato){
        List<Pago> pagos = repo.ListarPorContrato(IdContrato);
        return Json(pagos);
    }
}