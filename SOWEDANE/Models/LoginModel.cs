using System.ComponentModel.DataAnnotations;
namespace SOWEDANE.Models
{
    public class LoginModel
    {
        [Display(Name = "Login Name")]
        public string? LoginName { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
