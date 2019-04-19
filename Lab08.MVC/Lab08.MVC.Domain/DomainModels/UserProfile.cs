using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab08.MVC.Domain
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("IdentityUserModel")]
        public string Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BooksCount { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int UserTypeId { get; set; }

        public virtual ICollection<BookProfile> BooksProfiles { get; set; }

        public virtual IdentityUserModel IdentityUserModel { get; set; }

        public UserProfile()
        {
            UserTypeId = 2;
            BooksCount = 0;
            RegistrationDate = DateTime.Now;
        }
    }
}