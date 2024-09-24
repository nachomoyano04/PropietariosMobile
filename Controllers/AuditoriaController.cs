using Microsoft.AspNetCore.Mvc;

public class AuditoriaController : Controller{
    private readonly RepositorioAuditoria repo;
    private readonly RepositorioUsuario repoUsuario;

    public AuditoriaController(){
        repo = new RepositorioAuditoria();
        repoUsuario = new RepositorioUsuario();
    }

    public JsonResult GetPorUsuario(int id){
        List<Auditoria>auditorias = repo.ListarPorIdUsuario(id);
        return Json(auditorias);
    }

    public IActionResult Index(){
        List<Auditoria> auditorias = repo.Listar();
        List<Usuario> usuarios = repoUsuario.Listar();
        AuditoriaUsuarioViewModel auvm = new AuditoriaUsuarioViewModel{
            Auditorias = auditorias,
            Usuarios = usuarios
        };
        return View(auvm);
    }

}