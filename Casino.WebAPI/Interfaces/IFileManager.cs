namespace Casino.WebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IFileManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadAllText(string path);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        void WriteAllText(string path, string text);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DirectoryExists(string path);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool FileExists(string path);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string[] DirectoryGetFiles(string path);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        void CreateDirectory(string path);
    }
}
