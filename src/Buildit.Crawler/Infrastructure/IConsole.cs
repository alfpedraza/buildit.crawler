namespace Buildit.Crawler.Infrastructure
{
    public interface IConsole
    {
        void Write(string content);
        void Wait();
    }
}
