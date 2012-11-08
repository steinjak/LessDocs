using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dotless.Core.Parser.Tree;

namespace LessDocumentor
{
    class Program
    {
        static void Main(string[] args)
        {
            var rules = ExtractRules("Style.less");
            FormatDocumentation(rules);
        }

        private static IEnumerable<DocumentedRule> ExtractRules(string fileName)
        {
            var documentedRules = new List<DocumentedRule>();

            var parser = new dotless.Core.Parser.Parser();
            var rules = parser.Parse(File.ReadAllText(fileName), fileName);
            var iterator = rules.Rules.GetEnumerator();
            while (iterator.MoveNext())
            {
                var comment = iterator.Current as Comment;
                if (comment != null)
                {
                    if (!iterator.MoveNext()) break;
                    var ruleset = iterator.Current as Ruleset;
                    if (ruleset == null)
                    {
                        Console.WriteLine("Not a ruleset: " + iterator.Current);
                        continue;
                    }
                    documentedRules.Add(new DocumentedRule(string.Join(", ", ruleset.Selectors.Select(s => s.ToString().Trim())), comment.Value));
                }
            }
            return documentedRules;
        }

        private static void FormatDocumentation(IEnumerable<DocumentedRule> documentedRules)
        {
            foreach (var rule in documentedRules)
            {
                Console.WriteLine("Rule: {0} ({1})\nExample:\n{2}\n\n", rule.Name, rule.Category, rule.Example);
            }
        }

        private class DocumentedRule
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
                        Name = line.Substring("@name".Length);
                    }
                    else if (line.StartsWith("@category"))
                    {
                        Category = line.Substring("@category".Length);
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
}
