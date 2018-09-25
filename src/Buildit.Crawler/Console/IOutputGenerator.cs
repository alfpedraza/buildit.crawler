using Buildit.Crawler.Entities;

namespace Buildit.Crawler.Console
{
    /// <summary>
    /// Represents an object that converts a <see cref="Buildit.Crawler.Entities.Node" /> into a string representation.
    /// </summary>
    public interface IOutputGenerator
    {
        /// <summary>
        /// Converts a root <see cref="Buildit.Crawler.Entities.Node" /> into a string representation.
        /// </summary>
        /// <param name="node">A root node to print from.</param>
        /// <returns>A <see cref="System.String"/> representation of the root node.</returns>
        string Generate(Node node);
    }
}
