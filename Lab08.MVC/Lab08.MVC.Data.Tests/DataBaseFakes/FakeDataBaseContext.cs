using System.Data.Entity;
using Lab08.MVC.Domain;
using Lab08.MVC.Data.Tests.DataBaseFakes.FakeSets;

namespace Lab08.MVC.Data.Tests
{
    public class FakeDataBaseContext : DbContext, IDataBaseContext
    {
        public FakeDataBaseContext()
        {
            this.UsersProfiles = new FakeUsersProfilesSet();
            this.BooksProfiles = new FakeBooksProfilesSet();
            this.UsersTypes = new FakeUsersTypesSet();
        }

        public DbSet<UserProfile> UsersProfiles { get; }
        public DbSet<BookProfile> BooksProfiles { get; }
        public DbSet<UserType> UsersTypes { get; }

        public void SetModified(object entity)
        {
            // Used for testing Edit() of Repository
        }
    }
}
