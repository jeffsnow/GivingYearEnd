using System.Collections.Generic;
using System.IO;

namespace PrintDonations.Services
{
    internal class GetAllFilesFromDirectory
    {
        private string _filePath;

        public GetAllFilesFromDirectory(string filePath)
        {
            _filePath = filePath;
        }

        internal IEnumerable<string> Run()
        {
            return Directory.GetFiles(_filePath);
        }
    }
}