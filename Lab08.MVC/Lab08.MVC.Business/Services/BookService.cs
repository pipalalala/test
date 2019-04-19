using System;
using System.Linq;
using System.Collections.Generic;
using Lab08.MVC.Domain;
using Lab08.MVC.Data;

namespace Lab08.MVC.Business
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public BookService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public void ReturnBooksOfUser(UserProfile user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                foreach (var book in user.BooksProfiles)
                {
                    book.UserProfileId = null;
                    book.Accessibility = true;
                }

                user.BooksCount = 0;

                unitOfWork.Commit();
            }
        }

        public IEnumerable<BookProfile> GetFreeBooks()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork
                    .GetRepository<BookProfile>()
                    .GetAll()
                    .Where(book => book.Accessibility);
            }
        }

        public IEnumerable<BookProfile> GetAllBooks()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<BookProfile>().GetAll();
            }
        }

        public BookProfile GetBookById(int bookId)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<BookProfile>().GetById(bookId);
            }
        }

        public void AddBookToUserBooksCollection(UserProfile user, int bookId)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                BookProfile book = unitOfWork.GetRepository<BookProfile>().GetById(bookId);

                if (book == null)
                {
                    throw new ErrorException("Book is empty");
                }

                user.BooksProfiles.Add(book);
                user.BooksCount++;

                book.Accessibility = false;
                book.UserProfileId = user.Id;

                unitOfWork.Commit();
            }
        }

        public void RemoveBookFromUserBooksCollection(UserProfile user, int bookId)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                BookProfile book = unitOfWork.GetRepository<BookProfile>().GetById(bookId);

                if (book == null)
                {
                    throw new ErrorException("Book is empty");
                }

                user.BooksProfiles.Remove(book);
                user.BooksCount--;

                book.Accessibility = true;
                book.UserProfileId = null;

                unitOfWork.Commit();
            }
        }

        public void AddBookToLibrary(BookProfile book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<BookProfile>().Add(book);

                unitOfWork.Commit();
            }
        }

        public void UpdateBook(BookProfile book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<BookProfile>().Edit(book);
                unitOfWork.Commit();
            }
        }

        public void RemoveBookFromLibrary(int bookId)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var book = unitOfWork.GetRepository<BookProfile>().GetById(bookId);

                if (book == null)
                {
                    throw new ErrorException("Book is empty");
                }

                unitOfWork.GetRepository<BookProfile>().Remove(book);

                unitOfWork.Commit();
            }
        }

        public int GetBooksCount()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<BookProfile>().GetAll().Count();
            }
        }
    }
}