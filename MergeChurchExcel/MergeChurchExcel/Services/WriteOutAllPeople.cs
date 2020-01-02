using MergeChurchExcel.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MergeChurchExcel.Services
{
    internal class WriteOutAllPeople
    {
        private readonly IEnumerable<Transactions> _models;

        public WriteOutAllPeople(IEnumerable<Transactions> models)
        {
            _models = models;
        }

        /// <summary>
        /// This method insures unique rows and writes to file
        /// </summary>
        /// <param name="file">string value of a file location</param>
        public void Write(string file)
        {
            if (IsValidFile(file))
            {
                var models = getUniqueModelsByPerson();
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine("Last,First");
                    foreach (var row in models)
                    {
                        var people = BreakFirstLast(row);
                        writer.WriteLine($"{people.Item1}, {people.Item2}");
                    }
                }
            }
        }

        private bool IsValidFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            //TODO  Check for valid file
            return true;
        }

        /// <summary>
        /// This method splits last name and first name, trimming each
        /// </summary>
        /// <param name="row">string of unique rows</param>
        /// <returns>tuple last name, then first name </returns>
        private (string, string) BreakFirstLast(string row)
        {
            var personArray = row.Split(',');
            var last = personArray[0].Trim();
            var first = string.Empty;
            if (personArray.Length == 2)
            {
                first = personArray[1];
            }
            return (last, first);
        }

        private IEnumerable<string> getUniqueModelsByPerson()
        {
            return _models.ToList().GroupBy(x => x.Person).Select(y => y.First().Person);
        }
    }
}