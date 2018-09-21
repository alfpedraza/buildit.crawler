namespace Buildit.Crawler.Infrastructure
{
    public interface IFile
    {
        void Write(string filePath, string text);
    }
}
