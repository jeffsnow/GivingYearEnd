using MergeChurchExcel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MergeChurchExcel.Services
{
    internal class WriteOutReports
    {
        private string _file;
        private List<Transactions> _data;

        public WriteOutReports(IEnumerable<Transactions> data)
        {
            _file = data.ToList()[0].Person;
            _file = $"{_file.Replace(',', '_')}.txt";
            _file = _file.Replace(' ', '_');
            _file = Path.Combine(Addresses.WriteFolder, _file);
            _data = data.OrderBy(x => x.Date).ToList();
        }

        public void Write()
        {
            using (var writer = new StreamWriter(_file))
            {
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine($"Filipino American Community Church");
                writer.WriteLine($"Year end individual giving report");
                writer.WriteLine($"Prepared for:  {_data[0].Person}");
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine(FormatHeaders());
                foreach (var row in _data)
                {
                    writer.WriteLine(FormateRows(row));
                }
                writer.WriteLine();
                var total = $"Total {new string('.', 72)}{_data.Sum(x => x.TotalAmount):c}";
            }
        }

        private string FormateRows(Transactions row)
        {
            var date = row.Date.ToShortDateString().PadRight(15);
            var category = row.Category.PadRight(30);
            var checkNum = row.CheckNumber.ToString().PadRight(15);
            var check = FormatMoney(row.CheckAmount).PadRight(10);
            var cash = FormatMoney(row.CashAmount).PadRight(10);
            var total = $"{check + cash:c}";
            return $"{date}{category}{checkNum}{check}{cash}{total}";
        }

        private string FormatMoney(decimal? money)
        {
            if (money == null || money.Value == 0m) { return string.Empty; }
            return $"{money.Value:c}".ToString();
        }

        private string FormatHeaders()
        {
            var date = $"Date".PadRight(15);
            var category = "Category".PadRight(30);
            var checkNum = "Check Number".PadRight(15);
            var check = "Check".PadRight(10);
            var cash = "Cash".PadRight(10);
            var total = "Total";
            return $"{date}{category}{checkNum}{check}{cash}{total}";
        }
    }
}