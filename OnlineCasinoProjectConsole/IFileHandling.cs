namespace OnlineCasinoProjectConsole
{
    public interface IFileHandling
    {
        string readAllText(string path);
        void writeAllText(string path, string text);
        bool directoryExists(string path);
        bool fileExists(string path);
        string[] directoryGetFiles(string path);
        void createDirectory(string path);
    }
}
