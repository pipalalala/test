using System.Collections.Generic;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business
{
    public interface IBookService
    {
        void ReturnBooksOfUser(UserProfile user);

        IEnumerable<BookProfile> GetFreeBooks();

        IEnumerable<BookProfile> GetAllBooks();

        BookProfile GetBookById(int bookId);

        void AddBookToUserBooksCollection(UserProfile user, int bookId);

        void RemoveBookFromUserBooksCollection(UserProfile user, int bookId);

        void AddBookToLibrary(BookProfile book);

        void UpdateBook(BookProfile book);

        void RemoveBookFromLibrary(int bookId);

        int GetBooksCount();
    }
}