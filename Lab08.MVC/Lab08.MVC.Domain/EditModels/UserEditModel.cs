using System.ComponentModel.DataAnnotations;

namespace Lab08.MVC.Domain
{
    public class UserEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Field Login is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Login length must be from 6 to 20")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Field First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field Last name is required")]
        public string LastName { get; set; }
    }
}