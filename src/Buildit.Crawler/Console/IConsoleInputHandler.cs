using System;

namespace Buildit.Crawler.Console
{
    public interface IConsoleInputHandler
    {
        void ReadArguments(string[] args);
        Uri DomainUri { get; }
        string OutputFilePath { get; }
        bool WaitBeforeEnd { get; }
    }
}
