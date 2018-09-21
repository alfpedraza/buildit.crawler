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

        public void Write(string filePath, Node node, bool wait)
        {
            string content = _output.Generate(node);
            _file.Write(filePath, content);

            _console.Write(content);
            _console.Write(WaitText);
            if (wait) _console.Wait();
        }
    }
}
