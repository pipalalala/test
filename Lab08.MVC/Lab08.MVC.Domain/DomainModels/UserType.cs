using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab08.MVC.Domain
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<UserProfile> UsersProfiles { get; set; }

        public UserType()
        {
            CreationDate = DateTime.Now;
        }
    }
}