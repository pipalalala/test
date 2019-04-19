using System.ComponentModel.DataAnnotations;

namespace Lab08.MVC.Domain
{
    public class BookProfile
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field Creation year is required")]
        public int CreationYear { get; set; }

        [Required(ErrorMessage = "Field Pages count is required")]
        public int PagesCount { get; set; }

        public bool Accessibility { get; set; }

        public string UserProfileId { get; set; }

        public string Authors { get; set; }

        [Required(ErrorMessage = "Field Genres is required")]
        public string Genres { get; set; }

        public BookProfile()
        {
            Accessibility = true;
            UserProfileId = null;
        }
    }
}