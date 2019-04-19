using System;
using System.Linq;
using NUnit.Framework;
using Moq;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private const UserProfile EmptyUser = null;
        private const string EmptyUserId = null;
        private const string UserId = "112233";

        private static readonly Mock<FakeDataBaseContext> ContextMock = new Mock<FakeDataBaseContext>();

        private readonly IRepository<UserProfile> userProfileRepository = new Repository<UserProfile>(ContextMock.Object);

        private FakeDataBaseContext context;

        [SetUp]
        public void Init()
        {
            context = new FakeDataBaseContext
            {
                UsersProfiles =
                {
                    new UserProfile
                    {
                        Login = "112233",
                        Id = "332211",
                        UserTypeId = 1,
                        FirstName = "Alex",
                        LastName = "Brown"
                    },
                    new UserProfile
                    {
                        Login = "445566",
                        Id = "665544",
                        FirstName = "Raman",
                        LastName = "Zinevich"
                    }
                },
                BooksProfiles =
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
                    new BookProfile
                    {
                        Title = "Moby Dick",
                        CreationYear = 1851,
                        PagesCount = 585,
                        Authors = "Herman Melville",
                        Genres = "Roman"
                    },
                    new BookProfile
                    {
                        Title = "Hamlet",
                        CreationYear = 1599,
                        PagesCount = 104,
                        Authors = "William Shakespeare",
                        Genres = "Tragedy"
                    },
                    new BookProfile
                    {
                        Title = "The Divine Comedy",
                        CreationYear = 1308,
                        PagesCount = 136,
                        Authors = "Dante Alighieri",
                        Genres = "Epos"
                    }
                },
                UsersTypes =
                {
                    new UserType
                    {
                        Name = "Admin",
                        Description = "Admin"
                    },
                    new UserType
                    {
                        Name = "User",
                        Description = "User"
                    }
                }
            };
        }

        [Test]
        public void Add_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userProfileRepository.Add(EmptyUser));
        }

        [Test]
        [Repeat(100)]
        public void Add_ShouldUsersProfilesContainsUser()
        {
            // Arrange
            var user = new UserProfile
            {
                Login = "778899",
                Id = "998877",
                UserTypeId = 1,
                FirstName = "Alex",
                LastName = "Brown"
            };

            ContextMock.Setup(c => c.Set<UserProfile>().Add(user)).Returns(context.UsersProfiles.Add(user));

            // Act
            userProfileRepository.Add(user);

            // Assert
            Assert.Contains(user, context.UsersProfiles.ToList());
        }

        [Test]
        [Repeat(100)]
        public void GetById_WhenUserExists_ShouldResultUserEqualUser()
        {
            // Arrange
            ContextMock.Setup(c => c.Set<UserProfile>().Find(UserId)).Returns(context.UsersProfiles.Find(UserId));

            // Act
            var resultUser = userProfileRepository.GetById(UserId);

            // Assert
            Assert.AreEqual(resultUser, context.UsersProfiles.Find(UserId));
        }

        [Test]
        [Repeat(100)]
        public void GetById_WhenUserNotExists_ShouldGetByIdReturnNull()
        {
            // Arrange
            const string userId = "124215";

            ContextMock.Setup(c => c.Set<UserProfile>().Find(userId)).Returns(context.UsersProfiles.Find(userId));

            // Act
            var user = userProfileRepository.GetById(userId);

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public void GetById_WhenUserIdIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userProfileRepository.GetById(EmptyUserId));
        }

        [Test]
        public void Remove_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userProfileRepository.Remove(EmptyUser));
        }

        [Test]
        [TestCase("665544")]
        [TestCase("332211")]
        public void Remove_ShouldDeleteUserFromCollection(string userId)
        {
            // Arrange
            var user = context.UsersProfiles.FirstOrDefault(u => u.Id == userId);

            ContextMock.Setup(c => c.Set<UserProfile>().Remove(user)).Returns(context.UsersProfiles.Remove(user));

            // Act
            userProfileRepository.Remove(user);

            // Assert
            CollectionAssert.DoesNotContain(context.UsersProfiles, user);
        }

        [Test]
        public void Edit_WhenUserIsNull_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => userProfileRepository.Edit(EmptyUser));
        }

        [Test]
        [TestCase("665544", "111111")]
        [TestCase("332211", "222222")]
        public void Edit_ShouldUserIdMatchExpectedUserId(string userId, string expectedUserId)
        {
            // Arrange
            var user = context.UsersProfiles.FirstOrDefault(u => u.Id == userId);
            ContextMock.Setup(c => c.Set<UserProfile>().Attach(user)).Returns(context.UsersProfiles.Attach(user));

            // Act
            user.Id = expectedUserId;
            userProfileRepository.Edit(user);

            // Assert
            Assert.AreEqual(user.Id, expectedUserId);
        }

        [Test]
        public void GetAll_WhenUsersCount2_ShouldResultUsersCountEqual2()
        {
            // Arrange
            ContextMock.Setup(c => c.Set<UserProfile>()).Returns(context.UsersProfiles);

            // Act
            var resultUsers = userProfileRepository.GetAll();

            // Assert
            Assert.AreEqual(resultUsers.Count(), context.UsersProfiles.Count());
        }
    }
}
