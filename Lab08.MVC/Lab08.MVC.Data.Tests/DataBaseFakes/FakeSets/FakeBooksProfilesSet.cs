using System.Collections.Generic;
using System.Linq;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.Tests.DataBaseFakes.FakeSets
{
    public class FakeBooksProfilesSet : FakeDbSet<BookProfile>
    {
        public override BookProfile Find(params object[] keyValues)
        {
            var key = (int)keyValues.Single();
            IList<BookProfile> books = this.ToList();
            return books.FirstOrDefault(u => u.Id == key);
        }
    }
}