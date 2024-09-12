public class LoginViewModel
{
    [Required]
    [Display(Name = "email")]
    public string email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "password")]
    public string password { get; set;}
}