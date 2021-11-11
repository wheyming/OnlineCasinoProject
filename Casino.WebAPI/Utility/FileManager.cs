using Casino.WebAPI.Interfaces;
using System.IO;

namespace Casino.WebAPI.Utility
{
    /// <summary>
    /// 
    /// </summary>
    internal class FileManager : IFileManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        public void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] DirectoryGetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
