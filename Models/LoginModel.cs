public class LoginModel
{
    [Required]
    [Display(Name = "email")]
    public string email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "clave")]
    public string clave { get; set; }
}