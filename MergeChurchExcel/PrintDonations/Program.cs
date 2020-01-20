using PrintDonations.Services;
using System;

namespace PrintDonations
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Get all Files
            var file = @"C:\Users\snows_qsmftp5\Desktop\2019FIFRecord\2019\Write\";
            var allFiles = new GetAllFilesFromDirectory(file).Run();
            //Print each file
            foreach (var record in allFiles)
            {
                Console.WriteLine($"Current record{record}...Printing");
                new Print(file);
                Console.WriteLine("Press any key for the next record");
                Console.ReadKey();
            }
        }
    }
}