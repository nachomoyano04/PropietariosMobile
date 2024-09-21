using System.ComponentModel.DataAnnotations;
public class Auditoria{
    [Key]
    public int IdAuditoria{get; set;}
    public int IdUsuario{get;set}
    public string accion{get; set;}
    public string Observacion{get; set;}
    public DateTime fechaYHora{get;set}

}