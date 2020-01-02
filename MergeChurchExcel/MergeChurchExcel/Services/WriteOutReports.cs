using MergeChurchExcel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            var giver = _data[0].Person.Replace("  ", " ");
            using (var writer = new StreamWriter(_file, false)) //false overwrites files
            {
                writer.WriteLine(Addresses.Header);
                writer.WriteLine(Addresses.ReportTitle);
                writer.WriteLine($"{new string(' ', 40)}Prepared for:  {giver}");
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine(FormatHeaders());
                foreach (var row in _data)
                {
                    writer.WriteLine(FormateRows(row));
                }
                writer.WriteLine();
                writer.WriteLine(GetTotals());
                writer.WriteLine(Addresses.Treasurer);
            }
        }

        private string GetTotals()
        {
            var tithes = "Tithes and Offerings";
            var tithesRows = _data.Where(x => x.Category == tithes).ToList();
            var sb = new StringBuilder();
            if (tithesRows.Count > 0) //Only print rows if there are rows to print
            {
                sb.Append($"{new string(' ', 10)}Total Tithe    {new string('.', 65)}{tithesRows.Sum(x => x.TotalAmount):c}");
                sb.Append(Environment.NewLine);
            }
            var otherRows = _data.Where(x => x.Category != tithes).ToList();
            if (otherRows.Count > 0)
            {
                sb.Append($"{new string(' ', 10)}Total Other    {new string('.', 65)}{otherRows.Sum(x => x.TotalAmount):c}");
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        private string FormateRows(Transactions row)
        {
            var date = row.Date.ToShortDateString().PadRight(15);
            var category = row.Category.PadRight(30);
            var checkNum = FormatCheckNumber(row.CheckNumber);
            var check = FormatMoney(row.CheckAmount).PadRight(10);
            var cash = FormatMoney(row.CashAmount).PadRight(10);
            var total = $"{row.CheckAmount + row.CashAmount:c}";
            return $"{new string(' ', 10)}{date}{category}{checkNum}{check}{cash}{total}";
        }

        private string FormatCheckNumber(int checkNumber)
        {
            if (checkNumber == 0)
            {
                return new string(' ', 15);
            }
            return $"{checkNumber}".PadRight(15);
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
            return $"{new string(' ', 10)}{date}{category}{checkNum}{check}{cash}{total}";
        }
    }
}