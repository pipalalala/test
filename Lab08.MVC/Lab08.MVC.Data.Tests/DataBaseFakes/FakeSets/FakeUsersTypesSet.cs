using System.Collections.Generic;
using System.Linq;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.Tests.DataBaseFakes.FakeSets
{
    public class FakeUsersTypesSet : FakeDbSet<UserType>
    {
        public override UserType Find(params object[] keyValues)
        {
            var key = (int)keyValues.Single();
            IList<UserType> userTypes = this.ToList();
            return userTypes.FirstOrDefault(u => u.Id == key);
        }
    }
}