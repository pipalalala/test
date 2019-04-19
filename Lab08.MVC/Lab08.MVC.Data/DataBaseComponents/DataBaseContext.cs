using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data
{
    public class DataBaseContext : IdentityDbContext<IdentityUserModel>, IDataBaseContext
    {
        public DbSet<UserProfile> UsersProfiles { get; private set; }
        public DbSet<BookProfile> BooksProfiles { get; private set; }
        public DbSet<UserType> UsersTypes { get; private set; }

        public DataBaseContext()
            : base("Lab08MvcDbConnection")
        {
            Database.SetInitializer(new DataBaseInitializer());
            ////Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Migrations.Configuration>());
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            else
            {
                base.OnModelCreating(modelBuilder);

                ConfigureUserProfileTable(modelBuilder);
                ConfigureBookProfileTable(modelBuilder);
                ConfigureUserTypeTable(modelBuilder);

                ConfigureUserProfileBookProfileRelation(modelBuilder);
                ConfigureUserProfileUserTypeRelation(modelBuilder);
            }
        }

        private static void ConfigureUserProfileTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.Login)
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.FirstName)
                .HasMaxLength(30)
                .IsOptional()
                .HasColumnName("First Name");
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.LastName)
                .HasMaxLength(30)
                .IsOptional()
                .HasColumnName("Last Name");
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.BooksCount)
                .IsOptional()
                .HasColumnName("Books Count");
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.RegistrationDate)
                .IsRequired()
                .HasColumnName("Registration Date");
        }

        private static void ConfigureBookProfileTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.Title)
                .HasMaxLength(150)
                .IsRequired();
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.CreationYear)
                .IsRequired()
                .HasColumnName("Creation Year");
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.PagesCount)
                .IsRequired()
                .HasColumnName("Pages Count");
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.Accessibility)
                .IsRequired();
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.UserProfileId)
                .IsOptional();
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.Authors)
                .HasMaxLength(150)
                .IsOptional();
            modelBuilder.Entity<BookProfile>()
                .Property(b => b.Genres)
                .HasMaxLength(150)
                .IsOptional();
        }

        private static void ConfigureUserTypeTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>()
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<UserType>()
                .Property(r => r.Description)
                .HasMaxLength(150)
                .IsOptional();
            modelBuilder.Entity<UserType>()
                .Property(r => r.CreationDate)
                .IsRequired()
                .HasColumnName("Creation Date");
        }

        private static void ConfigureUserProfileBookProfileRelation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
               .HasMany<BookProfile>(u => u.BooksProfiles)
               .WithOptional()
               .HasForeignKey<string>(b => b.UserProfileId)
               .WillCascadeOnDelete(false);
        }

        private static void ConfigureUserProfileUserTypeRelation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>()
               .HasMany<UserProfile>(u => u.UsersProfiles)
               .WithOptional()
               .HasForeignKey<int>(b => b.UserTypeId)
               .WillCascadeOnDelete(true);
        }
    }
}