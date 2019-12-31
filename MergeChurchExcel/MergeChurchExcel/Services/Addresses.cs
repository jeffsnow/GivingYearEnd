using System;
using System.Text;

namespace MergeChurchExcel.Services
{
    public static class Addresses
    {
        public static string FolderAddress { get { return @"C:\Users\snows_qsmftp5\Desktop\2019FIFRecord\2019"; } }
        public static string WriteFolder { get { return @"C:\Users\snows_qsmftp5\Desktop\2019FIFRecord\2019\Write"; } }

        public static string Header
        {
            get {
                var sb = new StringBuilder();
                sb.Append($"{new string(' ', 40)}Filipino American Community Church");
                sb.Append(Environment.NewLine);
                sb.Append($"{new string(' ', 35)}13617 Midlothian Turnpike, Midlothian, VA 23113");
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
        }

        public static string ReportTitle
        {
            get {
                var sb = new StringBuilder();
                sb.Append($"{new string(' ', 50)}2019 Giving Report");
                sb.Append(Environment.NewLine);
                sb.Append($"{new string(' ', 30)}No goods or services were provided in exchange for donations");
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
        }

        public static string Treasurer
        {
            get {
                var sb = new StringBuilder();
                sb.Append(Environment.NewLine);
                sb.Append(string.Empty);
                sb.Append($"{new string(' ', 44)}Report certified by the Church Treasurer, Yolly Park");
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
        }
    }
}