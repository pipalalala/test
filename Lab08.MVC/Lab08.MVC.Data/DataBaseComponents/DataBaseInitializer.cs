using System;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data
{
    internal class DataBaseInitializer : CreateDatabaseIfNotExists<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            UserManager<IdentityUserModel> userManager = new UserManager<IdentityUserModel>(new UserStore<IdentityUserModel>(context));

            // AddRange() is not available in IDbSet
            foreach (var book in books)
            {
                context.BooksProfiles.Add(book);
            }
            foreach (var userType in userTypes)
            {
                context.UsersTypes.Add(userType);
            }

            AddNewUsers(context, userManager);

            context.SaveChanges();

            base.Seed(context);
        }

        private readonly IList<UserType> userTypes = new List<UserType>
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
            };

        private readonly IList<BookProfile> books = new List<BookProfile>
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
            };

        private readonly IList<IdentityUserModel> identityUsers = new List<IdentityUserModel>
        {
            new IdentityUserModel
            {
                UserName = "lalalala",
                Email = "lalalala@mail.ru"
            },

            new IdentityUserModel
            {
                UserName = "pipa",
                Email = "pipa@mail.ru"
            },

            new IdentityUserModel
            {
                UserName = "123kov",
                Email = "123kov@mail.ru"
            },
        };

        private void AddNewUsers(IDataBaseContext context, UserManager<IdentityUserModel> userManager)
        {
            userManager.Create(identityUsers[0], "11111111");

            context.UsersProfiles.Add(new UserProfile
            {
                Login = identityUsers[0].UserName,
                Id = identityUsers[0].Id,
                UserTypeId = 1,
                FirstName = "Alex",
                LastName = "Brown"
            });

            userManager.Create(identityUsers[1], "123123123");

            context.UsersProfiles.Add(new UserProfile
            {
                Login = identityUsers[1].UserName,
                Id = identityUsers[1].Id,
                FirstName = "Raman",
                LastName = "Zinevich"
            });

            userManager.Create(identityUsers[2], "123321");

            context.UsersProfiles.Add(new UserProfile
            {
                Login = identityUsers[2].UserName,
                Id = identityUsers[2].Id,
                FirstName = "Bob",
                LastName = "Anderson"
            });
        }
    }
}