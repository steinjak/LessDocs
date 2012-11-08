using System.Linq;
using System.Text;

namespace LessDocumentor
{
    public class DocumentedRule
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Example { get; private set; }

        public string Selectors { get; private set; }
        public string Comment { get; private set; }

        public DocumentedRule(string selectors, string comment)
        {
            Selectors = selectors;
            Comment = comment;

            var exampleMode = false;
            var example = new StringBuilder();
            var lines = comment.Split('\n').Select(line => line.Trim(' ', '\t', '*', '/'));
            foreach (var line in lines)
            {
                if (exampleMode)
                {
                    example.Append(line);
                }
                else if (line.StartsWith("@name"))
                {
                    Name = line.Substring("@name".Length).Trim();
                }
                else if (line.StartsWith("@category"))
                {
                    Category = line.Substring("@category".Length).Trim();
                }
                else if (line.StartsWith("@example"))
                {
                    exampleMode = true;
                }
            }
            Example = example.ToString();
        }
    }
}