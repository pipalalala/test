namespace Lab08.MVC.Domain
{
    public class BookFullInfoModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CreationYear { get; set; }

        public int PagesCount { get; set; }

        public bool Accessibility { get; set; }

        public string UserName { get; set; }

        public string Authors { get; set; }

        public string Genres { get; set; }
    }
}