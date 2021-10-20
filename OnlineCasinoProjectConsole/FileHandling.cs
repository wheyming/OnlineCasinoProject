using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public class FileHandling : IFileHandling
    {
        public string readAllText(string path)
        {
            return File.ReadAllText(path);
        }
        public void writeAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
        public bool directoryExists(string path)
        {
            return Directory.Exists(path);
        }
        public bool fileExists(string path)
        {
            return File.Exists(path);
        }
        public string[] directoryGetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
        public void createDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
