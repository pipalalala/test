using System.Collections.Generic;

namespace Lab08.MVC.Domain
{
    public class UserInfoModel
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BooksCount { get; set; }

        public IEnumerable<BookInfoModel> Books { get; set; }
    }
}