namespace Buildit.Crawler.Infrastructure
{
    public class SystemFile : IFile
    {
        public void Write(string filePath, string text)
        {
            System.IO.File.WriteAllText(filePath, text);
        }
    }
}
