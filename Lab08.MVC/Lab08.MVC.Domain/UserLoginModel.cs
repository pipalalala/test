using System.ComponentModel.DataAnnotations;

namespace Lab08.MVC.Domain
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Field Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Field Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}