using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Buildit.Crawler.Service
{
    public class LinkExtractor : ILinkExtractor
    {
        private const string HrefRegularExpression = @"<a[^>]+\s*href\s*=\s*(['""])?([^\1>]+?)\1";
        private const string SrcRegularExpression = @"<img[^>]+\s*src\s*=\s*(['""])?([^\1>]+?)\1";
        private const int HrefMatchGroupNumber = 2;
        private const int SrcMatchGroupNumber = 2;

        public virtual List<Uri> GetHyperlinks(string html)
        {
            // Gets the hyperlinks found in the HTML code.
            var hrefList = GetHyperlinks(html, HrefRegularExpression, HrefMatchGroupNumber);
            var srcList = GetHyperlinks(html, SrcRegularExpression, SrcMatchGroupNumber);

            // Removes any duplicated hyperlink and returns the result.
            var list = new List<Uri>();
            list.AddRange(hrefList);
            list.AddRange(srcList);
            var result = list.Distinct().ToList();

            return result;
        }

        private List<Uri> GetHyperlinks(string html, string regexPattern, int groupNumber)
        {
            // Gets all the matches for the specified regexPattern.
            var linkList = new List<Uri>();
            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(html);

            // Iterates through each match and if it's successful, adds the result to the list.
            foreach (Match match in matches)
            {
                if (match.Success && match.Groups.Count > groupNumber && match.Groups[groupNumber].Success)
                {
                    var uriString = match.Groups[groupNumber].Value;
                    var hyperlinkUri = new Uri(uriString, UriKind.RelativeOrAbsolute);
                    linkList.Add(hyperlinkUri);
                }
            }

            return linkList;
        }
    }
}
