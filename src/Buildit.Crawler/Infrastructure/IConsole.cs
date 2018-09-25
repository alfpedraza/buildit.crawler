namespace Buildit.Crawler.Infrastructure
{
    /// <summary>
    /// Defines methods to work with the command line window.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Writes the text to the command line window.
        /// </summary>
        /// <param name="content"></param>
        void Write(string content);

        /// <summary>
        /// Waits until the user press any key.
        /// </summary>
        void Wait();
    }
}
