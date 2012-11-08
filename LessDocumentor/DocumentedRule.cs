using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LessDocumentor
{
    public class DocumentedRule
    {
        private static readonly Regex KeywordLine = new Regex(@"^@(?<keyword>\w+)(?:\s+(?<rest>.*))?", RegexOptions.Compiled);
        private static readonly Regex LeadingCommentMarker = new Regex(@"^\s*(\*/?|//)\s?", RegexOptions.Compiled);

        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Example { get; private set; }

        public string Selectors { get; private set; }

        public DocumentedRule(string selectors, string comment)
        {
            Selectors = selectors;
            InitFieldsFrom(comment);
        }

        private void InitFieldsFrom(string comment)
        {
            string keyword = null;
            var content = new List<string>();
            foreach (var line in comment.Split('\n'))
            {
                var trimmed = Trim(line);
                var match = KeywordLine.Match(trimmed);
                if (match.Success)
                {
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        HandleContent(keyword, content);
                    }

                    keyword = match.Groups["keyword"].Value;
                    content = new List<string>();
                    var rest = match.Groups["rest"].Value;
                    if (!string.IsNullOrWhiteSpace(rest))
                    {
                        content.Add(rest);
                    }
                }
                else
                {
                    content.Add(line);
                }
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                HandleContent(keyword, content);
            }
        }

        private void HandleContent(string keyword, IEnumerable<string> lines)
        {
            switch (keyword.ToLowerInvariant())
            {
                case "name":
                    Name = AsOneLine(lines);
                    break;
                case "category":
                    Category = AsOneLine(lines);
                    break;
                case "example":
                    Example = RetainIndentation(lines).TrimEnd('\n');
                    break;
            }
        }

        private string RetainIndentation(IEnumerable<string> lines)
        {
            return string.Join("\n", lines.Select(TrimToCommentMarkerIfAny));
        }

        private string TrimToCommentMarkerIfAny(string line)
        {
            var match = LeadingCommentMarker.Match(line);
            return match.Success ? line.Substring(match.Length) : line;
        }

        private static string AsOneLine(IEnumerable<string> lines)
        {
            return string.Join(" ", lines.Select(Trim));
        }

        private static string Trim(string line)
        {
            return line.Trim(' ', '\t', '*', '/');
        }
    }
}