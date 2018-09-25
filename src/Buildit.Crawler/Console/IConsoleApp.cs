namespace Buildit.Crawler.Console
{
    /// <summary>
    /// Represents a console application that can be executed.
    /// </summary>
    public interface IConsoleApp
    {
        /// <summary>
        /// Executes the console application using the input command line arguments.
        /// </summary>
        /// <param name="args">An array of <see cref="System.String"/> that contain the command line arguments.</param>
        void Run(string[] args);
    }
}
