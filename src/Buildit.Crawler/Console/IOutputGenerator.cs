using Buildit.Crawler.Entities;

namespace Buildit.Crawler.Console
{
    public interface IOutputGenerator
    {
        string Generate(Node node);
    }
}
