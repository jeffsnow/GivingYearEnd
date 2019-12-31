using MergeChurchExcel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeChurchExcel.Services
{
    internal class GroupLikePeople
    {
        public GroupLikePeople(IEnumerable<Transactions> data)
        {
            var people = data.GroupBy(x => x.Person).Select(y => y.First().Person).ToList();
            foreach (var person in people)
            {
                var personData = data.Where(x => x.Person == person);
                new WriteOutReports(personData).Write();
            }
        }
    }
}