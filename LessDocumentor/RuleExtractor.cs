using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Stylizers;

namespace LessDocumentor
{
    public class RuleExtractor
    {
        public IEnumerable<DocumentedRule> ExtractRules(string fileName)
        {
            var parser = new dotless.Core.Parser.Parser(new PlainStylizer(), new Importer(new FileReader(new RelativeToFileLocationResolver(fileName))));
            var rules = parser.Parse(File.ReadAllText(fileName), fileName);
            return ExtractRulesRecursively(rules);
        }

        private static IEnumerable<DocumentedRule> ExtractRulesRecursively(Ruleset rules)
        {
            var documentedRules = new List<DocumentedRule>();
            var iterator = rules.Rules.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (iterator.Current is Import)
                {
                    var import = (Import)iterator.Current;
                    var importedRules = ExtractRulesRecursively(import.InnerRoot);
                    documentedRules.AddRange(importedRules);
                }
                else if (iterator.Current is Comment)
                {
                    var result = ExtractConsecutiveComments(iterator);
                    var ruleset = iterator.Current as Ruleset;
                    if (ruleset != null)
                    {
                        documentedRules.Add(new DocumentedRule(string.Join(", ", ruleset.Selectors.Select(s => s.ToString().Trim())), result));
                    }
                }
            }
            return documentedRules;
        }

        private static string ExtractConsecutiveComments(IEnumerator<Node> iterator)
        {
            var comment = new StringBuilder();
            comment.AppendLine(((Comment) iterator.Current).Value);

            while (iterator.MoveNext())
            {
                if (!(iterator.Current is Comment))
                {
                    break;
                }
                comment.AppendLine(((Comment)iterator.Current).Value);
            }
            return comment.ToString().TrimEnd('\n', '\r');
        }

        private class RelativeToFileLocationResolver : IPathResolver
        {
            private readonly FileInfo fileInfo;

            public RelativeToFileLocationResolver(string fileName)
            {
                fileInfo = new FileInfo(fileName);
            }

            public string GetFullPath(string path)
            {
                return fileInfo.Directory == null ? path : Path.Combine(fileInfo.Directory.FullName, path);
            }
        }
    }
}