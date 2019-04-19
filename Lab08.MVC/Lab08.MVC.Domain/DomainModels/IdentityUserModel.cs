using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab08.MVC.Domain
{
    public class IdentityUserModel : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}