using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotless.Core.Importers;
using dotless.Core.Input;
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
                    var comment = (Comment)iterator.Current;

                    if (!iterator.MoveNext()) break;
                    if (iterator.Current is Ruleset)
                    {
                        var ruleset = (Ruleset) iterator.Current;
                        documentedRules.Add(new DocumentedRule(string.Join(", ", ruleset.Selectors.Select(s => s.ToString().Trim())), comment.Value));
                    }
                    else
                    {
                        Console.WriteLine("Ignoring: " + iterator.Current);
                    }
                }
            }
            return documentedRules;
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