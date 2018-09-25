using Buildit.Crawler.Entities;
using Buildit.Crawler.Infrastructure;

namespace Buildit.Crawler.Console
{
    public class ConsoleOutputHandler : IConsoleOutputHandler
    {
        private const string WaitText = "Press any key to exit...";

        private readonly IOutputGenerator _output;
        private readonly IFile _file;
        private readonly IConsole _console;

        public ConsoleOutputHandler(IOutputGenerator output, IFile file, IConsole console)
        {
            _output = output;
            _file = file;
            _console = console;
        }

        public string Generate(Node node)
        {
            string result = _output.Generate(node);
            return result;
        }

        public void Save(string filePath, string text)
        {
            _file.Write(filePath, text);
        }

        public void Write(string text, bool wait)
        {
            _console.Write(text);
            if (wait)
            {
                _console.Write(WaitText);
                _console.Wait();
            }
        }
    }
}
