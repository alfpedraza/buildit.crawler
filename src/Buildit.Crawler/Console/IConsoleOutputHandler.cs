using Buildit.Crawler.Entities;

namespace Buildit.Crawler.Console
{
    public interface IConsoleOutputHandler
    {
        void Write(string filePath, Node node, bool wait);
    }
}
