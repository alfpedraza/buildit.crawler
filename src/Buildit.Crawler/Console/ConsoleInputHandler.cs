using Buildit.Crawler.Infrastructure;
using System;

namespace Buildit.Crawler.Console
{
    public class ConsoleInputHandler : IConsoleInputHandler
    {
        private const int FirstArgumentIndex = 0;
        private const int SecondArgumentIndex = 1;
        private const int ThirdArgumentIndex = 2;
        private const string DefaultDomain = "https://buildit.wiprodigital.com";
        private const string DefaultOutputFilePathFormat = "crawl_{0:yyyyMMdd_HHmmss}.txt";
        private const string DefaultWaitBeforeEnd = "true";

        private readonly IClock _clock;

        public Uri DomainUri { get; private set; }
        public string OutputFilePath { get; private set; }
        public bool WaitBeforeEnd { get; private set; }

        public ConsoleInputHandler(IClock clock)
        {
            _clock = clock;
        }

        public void ReadArguments(string[] args)
        {
            DomainUri = GetDomainUri(args);
            OutputFilePath = GetOutputFilePath(args);
            WaitBeforeEnd = GetWaitBeforeEnd(args);
        }

        // Gets a domain from the command line arguments ignoring any path.
        private Uri GetDomainUri(string[] args)
        {
            var argumentUriString = ReadArgument(args, FirstArgumentIndex, DefaultDomain);
            var argumentUri = new Uri(argumentUriString);
            var domainUriString = argumentUri.GetLeftPart(UriPartial.Authority);
            var result = new Uri(domainUriString);
            return result;
        }

        // Gets a output file path from the command line arguments.
        private string GetOutputFilePath(string[] args)
        {
            var filePathFormat = ReadArgument(args, SecondArgumentIndex, DefaultOutputFilePathFormat);
            var result = string.Format(filePathFormat, _clock.Now);
            return result;
        }

        // Gets a boolean flag to wait from the command line arguments.
        private bool GetWaitBeforeEnd(string[] args)
        {
            var waitString = ReadArgument(args, ThirdArgumentIndex, DefaultWaitBeforeEnd);
            var result = Convert.ToBoolean(waitString);
            return result;
        }

        // Reads a value from the specified array. If the index doesn't exists, take the default value.
        private string ReadArgument(string[] args, int index, string defaultValue)
        {
            return (args.Length > index) ? args[index] : defaultValue;
        }
    }
}
