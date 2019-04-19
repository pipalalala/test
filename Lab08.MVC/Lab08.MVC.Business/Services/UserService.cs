using Lab08.MVC.Domain;
using Microsoft.AspNet.Identity;

namespace Lab08.MVC.Business
{
    public class UserService : UserManager<IdentityUserModel>
    {
        public UserService(IUserStore<IdentityUserModel> store)
                : base(store)
        { }
    }
}