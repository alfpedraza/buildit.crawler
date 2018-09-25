using Buildit.Crawler.Entities;
using System;
using System.Text;

namespace Buildit.Crawler.Console
{
    public class TextOutputGenerator : IOutputGenerator
    {
        private const int SpacesByLevel = 4;
        private const char SpaceCharacter = ' ';
        private const int InitialLevel = 0;

        public string Generate(Node node)
        {
            var builder = new StringBuilder();
            GenerateInternal(builder, node, InitialLevel);
            var result = builder.ToString();
            return result;
        }

        // Navigates recursively throught the node tree to print it.
        private void GenerateInternal(StringBuilder builder, Node node, int level)
        {
            // Prints the left indented margin.
            var spaceCount = SpacesByLevel * level;
            var levelMargin = new String(SpaceCharacter, spaceCount);
            builder.Append(levelMargin);

            // Prints the node Uri and then its children.
            builder.AppendLine(node.Uri.AbsoluteUri);
            foreach (var child in node.Nodes)
            {
                GenerateInternal(builder, child, level + 1);
            }
        }
    }
}
