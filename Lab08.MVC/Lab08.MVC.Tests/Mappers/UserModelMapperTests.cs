using System;
using System.Collections.Generic;
using Lab08.MVC.Domain;
using NUnit.Framework;
using Lab08.MVC.Mappers;
using Moq;

namespace Lab08.MVC.Tests.Mappers
{
    [TestFixture]
    public class UserModelMapperTests
    {
        private const UserProfile EmptyUser = null;

        private static readonly ICollection<BookProfile> Books = new List<BookProfile>
        {
            new BookProfile
            {
                Title = "In Search of Lost Time",
                CreationYear = 1913,
                PagesCount = 4215,
                Authors = "Marcel Proust",
                Genres = "Modern"
            },
            new BookProfile
            {
                Title = "Don Quixote",
                CreationYear = 1605,
                PagesCount = 863,
                Authors = "Miguel de Cervantes",
                Genres = "Roman"
            },
        };
        private readonly UserProfile user = new UserProfile
        {
            Id = "123123",
            Login = "lalalala",
            FirstName = "lala",
            LastName = "lalala",
            BooksCount = Books.Count,
            BooksProfiles = Books,
            IdentityUserModel = null
        };

        private Mock<IBookModelMapper> bookModelMapper;
        private IUserModelMapper userModelMapper;

        [SetUp]
        public void Init()
        {
            bookModelMapper = new Mock<IBookModelMapper>();
            userModelMapper = new UserModelMapper(bookModelMapper.Object);
        }

        [Test]
        public void GetUserInfoModel_WhenUserProfileIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userModelMapper.GetUserInfoModel(EmptyUser));
        }

        [Test]
        public void GetUserInfoModel_ShouldMapCorrectly()
        {
            // Arrange
            UserInfoModel resultUser = null;

            // Act
            resultUser = userModelMapper.GetUserInfoModel(user);

            // Assert
            Assert.AreEqual(resultUser.Id, user.Id);
            Assert.AreEqual(resultUser.Login, user.Login);
            Assert.AreEqual(resultUser.FirstName, user.FirstName);
            Assert.AreEqual(resultUser.LastName, user.LastName);
            Assert.AreEqual(resultUser.BooksCount, user.BooksCount);
        }
    }
}