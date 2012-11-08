using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotless.Core.Parser.Tree;

namespace LessDocumentor
{
    public class RuleExtractor
    {
        public static IEnumerable<DocumentedRule> ExtractRules(string fileName)
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
                    documentedRules.Add(new DocumentedRule(String.Join(", ", ruleset.Selectors.Select(s => s.ToString().Trim())), comment.Value));
                }
            }
            return documentedRules;
        }
    }
}