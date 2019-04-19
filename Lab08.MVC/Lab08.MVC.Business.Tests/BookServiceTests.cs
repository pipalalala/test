using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using Lab08.MVC.Data;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        private const int BookId = 2;
        private const BookProfile EmptyBook = null;
        private const UserProfile EmptyUser = null;

        private readonly IList<BookProfile> books = new List<BookProfile>
        {
            new BookProfile
            {
                Id = 10,
                Title = "In Se1253 Time",
                CreationYear = 1913,
                PagesCount = 4215,
                Authors = "Marcel Proust",
                Genres = "Modern"
            },
            new BookProfile
            {
                Id = 30,
                Title = "In S1235 Lost Time",
                CreationYear = 1913,
                PagesCount = 4215,
                Authors = "Marcel Proust",
                Genres = "Modern"
            },
            new BookProfile
            {
                Id = 20,
                Title = "In Searc215 Time",
                CreationYear = 1913,
                PagesCount = 4215,
                Authors = "Marcel Proust",
                Genres = "Modern"
            }
        };

        private UserProfile user;
        private BookProfile book;

        private IBookService bookService;

        private Mock<IRepository<BookProfile>> bookRepository;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactory;

        [SetUp]
        public void Init()
        {
            bookRepository = new Mock<IRepository<BookProfile>>();
            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();

            bookService = new BookService(unitOfWorkFactory.Object);

            unitOfWork.Setup(u => u.GetRepository<BookProfile>()).Returns(bookRepository.Object);
            unitOfWorkFactory.Setup(f => f.Create()).Returns(unitOfWork.Object);

            user = new UserProfile
            {
                Login = "123123123",
                Id = "12521561264",
                UserTypeId = 1,
                FirstName = "Alex",
                LastName = "Brown",
                BooksProfiles = new HashSet<BookProfile>()
            };

            book = new BookProfile
            {
                Id = 10,
                Title = "In Search of Lost Time",
                CreationYear = 1913,
                PagesCount = 4215,
                Authors = "Marcel Proust",
                Genres = "Modern"
            };
        }

        [Test]
        public void AddBookToUserBooksCollection_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookService.AddBookToUserBooksCollection(EmptyUser, BookId));
        }

        [Test]
        public void AddBookToUserBooksCollection_WhenRepositoryReturnsNull_ShouldThrowErrorException()
        {
            // Arrange
            bookRepository.Setup(r => r.GetById(BookId)).Returns(EmptyBook);

            // Assert
            Assert.Throws<ErrorException>(() => bookService.AddBookToUserBooksCollection(user, BookId));
        }

        [Test]
        public void AddBookToUserBooksCollection_ShouldAddBookToUserBooksCollectionAndChangeBookAccessibilityAndCallCommitMethod()
        {
            // Arrange
            bookRepository.Setup(r => r.GetById(BookId)).Returns(book);

            // Act
            bookService.AddBookToUserBooksCollection(user, BookId);

            // Assert
            Assert.AreEqual(1, user.BooksCount, "Count of books has no increased");
            Assert.AreSame(book, user.BooksProfiles.First(), "Books are not same");
            Assert.IsFalse(book.Accessibility);
            Assert.AreSame(user.Id, book.UserProfileId, "IDs are not same");
            unitOfWork.Verify(u => u.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        public void RemoveBookFromUserBooksCollection_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookService.RemoveBookFromUserBooksCollection(EmptyUser, BookId));
        }

        [Test]
        public void RemoveBookFromUserBooksCollection_WhenRepositoryReturnsNull_ShouldThrowAErrorException()
        {
            // Arrange
            bookRepository.Setup(r => r.GetById(BookId)).Returns(EmptyBook);

            // Assert
            Assert.Throws<ErrorException>(() => bookService.RemoveBookFromUserBooksCollection(user, BookId));
        }

        [Test]
        public void RemoveBookFromUserBooksCollection_WhenUserBooksCount1_ShouldUserBooksCount0AndChangeBookAccessibilityToTrueAndCallCommitMethod()
        {
            // Arrange
            user.BooksProfiles.Add(book);
            user.BooksCount = 1;

            bookRepository.Setup(r => r.GetById(BookId)).Returns(book);

            // Act
            bookService.RemoveBookFromUserBooksCollection(user, BookId);

            // Assert
            Assert.AreEqual(0, user.BooksCount, "Count of books has no decreased");
            Assert.IsTrue(book.Accessibility);
            Assert.IsNull(book.UserProfileId);
            unitOfWork.Verify(u => u.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        public void AddBookToLibrary_WhenBookIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookService.AddBookToLibrary(EmptyBook));
        }

        [Test]
        public void AddBookToLibrary_ShouldCallAddAndCommitMethods()
        {
            // Act
            bookService.AddBookToLibrary(book);

            // Assert
            bookRepository.Verify(r => r.Add(book), Times.Once, "Method was not called once");
            unitOfWork.Verify(u => u.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        public void UpdateBook_WhenBookIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookService.UpdateBook(EmptyBook));
        }

        [Test]
        public void UpdateBook_ShouldCallEditAndCommitMethods()
        {
            // Act
            bookService.UpdateBook(book);

            // Assert
            bookRepository.Verify(r => r.Edit(book), Times.Once, "Method was not called once");
            unitOfWork.Verify(u => u.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        public void RemoveBookFromLibrary_WhenBookIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            bookRepository.Setup(r => r.GetById(BookId)).Returns(EmptyBook);

            // Assert
            Assert.Throws<ErrorException>(() => bookService.RemoveBookFromLibrary(BookId));
        }

        [Test]
        public void RemoveBookFromLibrary_ShouldCallRemoveAndCommitMethods()
        {
            // Arrange
            bookRepository.Setup(r => r.GetById(BookId)).Returns(book);

            // Act
            bookService.RemoveBookFromLibrary(BookId);

            // Assert
            bookRepository.Verify(r => r.Remove(book), Times.Once, "Method was not called once");
            unitOfWork.Verify(u => u.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        public void GetBooksCount_ShouldEqualExpectedCountsAndReturnsCountsAndCallGetAllMethod(int expectedBooksCount, int booksCount)
        {
            // Arrange
            var resultBooks = new List<BookProfile>();
            for (var i = 0; i < booksCount; i++)
            {
                resultBooks.Add(It.IsAny<BookProfile>());
            }

            bookRepository.Setup(r => r.GetAll()).Returns(resultBooks);

            // Act
            var resultCount = bookService.GetBooksCount();

            // Assert
            Assert.AreEqual(resultCount, expectedBooksCount, $"Count of books is not {expectedBooksCount}");
            bookRepository.Verify(r => r.GetAll(), Times.Once, "Method was not called once");
        }

        [Test]
        [TestCase(10, 3)]
        [TestCase(5, 4)]
        [TestCase(5, 2)]
        public void GetBooksCount_ShouldNotEqualExpectedCountsAndReturnsCountsAndCallGetAllMethod(int expectedBooksCount, int booksCount)
        {
            // Arrange
            var resultBooks = new List<BookProfile>();
            for (var i = 0; i < booksCount; i++)
            {
                resultBooks.Add(It.IsAny<BookProfile>());
            }

            bookRepository.Setup(r => r.GetAll()).Returns(resultBooks);

            // Act
            var resultCount = bookService.GetBooksCount();

            // Assert
            Assert.AreNotEqual(resultCount, expectedBooksCount, "Counts are equal");
            bookRepository.Verify(r => r.GetAll(), Times.Once, "Method was not called once");
        }

        [Test]
        public void GetBookById_ShouldMatchResultBookIdAndBookIdAndCallGetByIdMethod()
        {
            // Arrange
            bookRepository.Setup(r => r.GetById(BookId)).Returns(book);

            // Act
            var resultBook = bookService.GetBookById(BookId);

            // Assert
            Assert.AreEqual(resultBook.Id, book.Id, "IDs are not equal");
            bookRepository.Verify(r => r.GetById(BookId), Times.Once, "Method was not called once");
        }

        [Test]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(2, 2)]
        public void GetAllBooks_ShouldMatchExpectedBooksCountAndResultBooksCountAndCallGetAllMethod(int expectedBooksCount, int booksCount)
        {
            // Arrange
            var bookProfiles = new List<BookProfile>();
            for (var i = 0; i < booksCount; i++)
            {
                bookProfiles.Add(book);
            }

            bookRepository.Setup(r => r.GetAll()).Returns(bookProfiles);

            // Act
            var resultBooks = bookService.GetAllBooks();

            // Assert
            Assert.AreEqual(resultBooks.Count(), expectedBooksCount, "Counts are not equal");
            bookRepository.Verify(r => r.GetAll(), Times.Once, "Method was not called once");
        }

        [Test]
        public void GetFreeBooks_When1FreeBook_ShouldMatchResultBooksCountAnd1AndCallGetAllMethod()
        {
            // Arrange
            books[0].Accessibility = false;
            books[1].Accessibility = false;

            bookRepository.Setup(r => r.GetAll()).Returns(books);

            // Act
            var resultBooks = bookService.GetFreeBooks();

            // Assert
            Assert.AreEqual(1, resultBooks.Count(), "Counts are not equal");
            bookRepository.Verify(r => r.GetAll(), Times.Once, "Method was not called once");
        }

        [Test]
        public void ReturnBooksOfUser_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookService.ReturnBooksOfUser(EmptyUser));
        }

        [Test]
        public void ReturnBooksOfUser_WhenUserBooksCount4_ShouldUserBooksCount0AndCallCommitMethod()
        {
            // Arrange
            user.BooksCount = 4;

            // Act
            bookService.ReturnBooksOfUser(user);

            // Assert
            Assert.AreEqual(0, user.BooksCount, "Count of books is not 0");
            unitOfWork.Verify(u => u.Commit(), Times.Once, "Method was not called once");
        }
    }
}