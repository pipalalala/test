using System;
using System.Collections.Generic;
using Moq;
using Lab08.MVC.Data;
using Lab08.MVC.Domain;
using NUnit.Framework;

namespace Lab08.MVC.Business.Tests
{
    [TestFixture]
    public class UserProfileServiceTests
    {
        private const string EmptyUserId = null;
        private const string UserId = "123123";
        private const UserProfile EmptyUser = null;

        private UserProfile user;
        private Mock<IRepository<UserProfile>> userRepository;
        private Mock<IRepository<UserType>> userTypeRepository;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactory;
        private IUserProfileService userService;

        [SetUp]
        public void Init()
        {
            userRepository = new Mock<IRepository<UserProfile>>();
            userTypeRepository = new Mock<IRepository<UserType>>();
            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();

            userService = new UserProfileService(unitOfWorkFactory.Object);

            unitOfWork.Setup(u => u.GetRepository<UserProfile>()).Returns(userRepository.Object);
            unitOfWork.Setup(u => u.GetRepository<UserType>()).Returns(userTypeRepository.Object);
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
        }

        [Test]
        public void GetUserLoginById_WhenUserIdIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.GetUserLoginById(EmptyUserId));
        }

        [Test]
        public void GetUserLoginById_ShouldUserLoginMatchReturningLoginAndCallGetByIdMethod()
        {
            // Arrange
            userRepository.Setup(r => r.GetById(UserId)).Returns(user);

            // Act
            var userLogin = userService.GetUserLoginById(UserId);

            // Assert
            Assert.AreEqual(userLogin, user.Login, "Logins are not same");
            userRepository.Verify(r => r.GetById(UserId), Times.Once, "Method was not called once");
        }

        [Test]
        public void GetUserTypeName_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.GetUserTypeName(EmptyUser));
        }

        [Test]
        public void GetUserTypeName_ShouldUserTypeNameMatchReturningNameAndCallGetByIdMethod()
        {
            // Arrange
            var userType = new UserType
            {
                Id = 1,
                Name = "User"
            };

            userTypeRepository.Setup(r => r.GetById(user.UserTypeId)).Returns(userType);

            // Act
            var userTypeName = userService.GetUserTypeName(user);

            // Assert
            Assert.AreEqual(userTypeName, userType.Name);
            userTypeRepository.Verify(r => r.GetById(user.UserTypeId), Times.Once, "Method was not called once");
        }

        [Test]
        public void GetUserById_WhenUserIdIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.GetUserById(EmptyUserId));
        }

        [Test]
        public void GetUserById_ShouldReturningUseMatchUserAndCallGetByIdMethod()
        {
            // Arrange
            userRepository.Setup(r => r.GetById(UserId)).Returns(user);

            // Act
            var resultUser = userService.GetUserById(UserId);

            // Assert
            Assert.AreEqual(resultUser, user, "Users are not same");
            userRepository.Verify(r => r.GetById(UserId), Times.Once, "Method was not called once");
        }

        [Test]
        public void AddUser_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.AddUser(EmptyUser));
        }

        [Test]
        public void AddUser_ShouldCallAddAndCommitMethods()
        {
            // Arrange
            userRepository.Setup(r => r.Add(user));

            // Act
            userService.AddUser(user);

            // Assert
            userRepository.Verify(r => r.Add(user), Times.Once, "Method was not called once");
            unitOfWork.Verify(r => r.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        public void UpdateUser_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.UpdateUser(EmptyUser));
        }

        [Test]
        public void UpdateUser_ShouldCallUpdateAndCommitMethods()
        {
            // Arrange
            userRepository.Setup(r => r.Edit(user));

            // Act
            userService.UpdateUser(user);

            // Assert
            userRepository.Verify(r => r.Edit(user), Times.Once, "Method was not called once");
            unitOfWork.Verify(r => r.Commit(), Times.Once, "Method was not called once");
        }

        [Test]
        public void DeleteUser_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.DeleteUser(EmptyUser));
        }

        [Test]
        public void DeleteUser_ShouldCallDeleteAndCommitMethods()
        {
            // Arrange
            userRepository.Setup(r => r.Remove(user));

            // Act
            userService.DeleteUser(user);

            // Assert
            userRepository.Verify(r => r.Remove(user), Times.Once, "Method was not called once");
            unitOfWork.Verify(r => r.Commit(), Times.Once, "Method was not called once");
        }
    }
}