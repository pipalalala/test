using System.ComponentModel.DataAnnotations;

namespace Lab08.MVC.Domain
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Field Login is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Login length must be from 6 to 20")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Field Email is required")]
        [RegularExpression(@"^[.-_a-z0-9]+@([a-z0-9][-a-z0-9]+.)+[a-z]{2,6}$", ErrorMessage = "Field Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password length must be from 8 to 20")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field Password Confirm is required")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}