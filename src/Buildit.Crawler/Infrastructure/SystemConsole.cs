namespace Buildit.Crawler.Infrastructure
{
    public class SystemConsole : IConsole
    {
        public void Write(string content)
        {
            System.Console.Write(content);
        }

        public void Wait()
        {
            System.Console.ReadKey(true);
        }
    }
}
