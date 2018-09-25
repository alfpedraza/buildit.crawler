namespace Buildit.Crawler.Infrastructure
{
    /// <summary>
    /// Defines methods to interact with the file system.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Writes the text into the specified file.
        /// </summary>
        /// <param name="filePath">The file to write to</param>
        /// <param name="text">A <see cref="System.String"/> representing the text to write.</param>
        void Write(string filePath, string text);
    }
}
