using Buildit.Crawler.Entities;

namespace Buildit.Crawler.Console
{
    /// <summary>
    /// Defines methods to write the output to the specified file.
    /// </summary>
    public interface IConsoleOutputHandler
    {
        /// <summary>
        /// Generates a <see cref="System.String"/> representation of the <see cref="Buildit.Crawler.Entities.Node"/>.
        /// </summary>
        /// <param name="node">The root node to print from.</param>
        /// <returns>A <see cref="System.String"/> representation of the <see cref="Buildit.Crawler.Entities.Node"/>.</returns>
        string Generate(Node node);

        /// <summary>
        /// Saves the text to the specified file path.
        /// </summary>
        /// <param name="filePath">The file to write to.</param>
        /// <param name="text">The text to write.</param>
        void Save(string filePath, string text);

        /// <summary>
        /// Writes the text to the command line window.
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="wait">Determines whether to wait for the user.</param>
        void Write(string text, bool wait);
    }
}
