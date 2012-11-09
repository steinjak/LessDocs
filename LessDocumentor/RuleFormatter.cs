using System;
using System.Collections.Generic;

namespace LessDocs
{
    public class RuleFormatter
    {
        public void FormatDocumentation(IEnumerable<DocumentedRule> documentedRules)
        {
            foreach (var rule in documentedRules)
            {
                Console.WriteLine("Rule: {0} ({1})\nExample:\n{2}\n\n", rule.Name, rule.Category, rule.Example);
            }
        }
    }
}