using System;
using Lab08.MVC.Domain;
using NUnit.Framework;
using Lab08.MVC.Mappers;

namespace Lab08.MVC.Tests.Mappers
{
    [TestFixture]
    public class BookModelMapperTests
    {
        private const BookProfile EmptyBook = null;

        private readonly IBookModelMapper bookModelMapper = new BookModelMapper();
        private readonly BookProfile book = new BookProfile
        {
            Id = 123123,
            Title = "In Search of Lost Time",
            CreationYear = 1913,
            PagesCount = 4215,
            Authors = "Marcel Proust",
            Genres = "Modern"
        };

        [Test]
        public void GetBookInfoModel_WhenBookProfileIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookModelMapper.GetBookInfoModel(EmptyBook));
        }

        [Test]
        public void GetBookInfoModel_ShouldMapCorrectly()
        {
            // Arrange
            BookInfoModel resultBook = null;

            // Act
            resultBook = bookModelMapper.GetBookInfoModel(book);

            // Assert
            Assert.AreEqual(book.Id, resultBook.Id);
            Assert.AreEqual(resultBook.Title, book.Title);
            Assert.AreEqual(resultBook.Authors, book.Authors);
            Assert.AreEqual(resultBook.Accessibility, book.Accessibility);
        }

        [Test]
        public void GetBookFullInfoModel_WhenBookProfileIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => bookModelMapper.GetBookFullInfoModel(EmptyBook));
        }

        [Test]
        public void GetBookFullInfoModel_ShouldMapCorrectly()
        {
            // Arrange
            BookFullInfoModel resultBook = null;

            // Act
            resultBook = bookModelMapper.GetBookFullInfoModel(book);

            // Assert
            Assert.AreEqual(resultBook.Id, book.Id);
            Assert.AreEqual(resultBook.Title, book.Title);
            Assert.AreEqual(resultBook.CreationYear, book.CreationYear);
            Assert.AreEqual(resultBook.PagesCount, book.PagesCount);
            Assert.AreEqual(resultBook.Accessibility, book.Accessibility);
            Assert.AreEqual(resultBook.Authors, book.Authors);
            Assert.AreEqual(resultBook.Genres, book.Genres);
            Assert.IsNull(resultBook.UserName);
        }
    }
}