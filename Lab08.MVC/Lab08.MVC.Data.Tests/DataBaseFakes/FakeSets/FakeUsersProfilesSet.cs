using System.Collections.Generic;
using System.Linq;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.Tests.DataBaseFakes.FakeSets
{
    public class FakeUsersProfilesSet : FakeDbSet<UserProfile>
    {
        public override UserProfile Find(params object[] keyValues)
        {
            var key = (string)keyValues.Single();
            IList<UserProfile> users = this.ToList();
            return users.FirstOrDefault(u => u.Id == key);
        }
    }
}