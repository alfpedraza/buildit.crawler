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

        private void GenerateInternal(StringBuilder builder, Node node, int level)
        {
            var spaceCount = SpacesByLevel * level;
            var levelMargin = new String(SpaceCharacter, spaceCount);
            builder.Append(levelMargin);
            builder.AppendLine(node.Uri.AbsoluteUri);
            foreach (var child in node.Nodes)
            {
                GenerateInternal(builder, child, level + 1);
            }
        }
    }
}
