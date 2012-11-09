using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace LessDocs.Tests.Formatting
{
    [TestFixture]
    public class WhenFormattingRulesWithTheRazorFormatter
    {
        private string result;
        private IEnumerable<DocumentedRule> rules;

        [SetUp]
        public void Given()
        {
            var extractor = new RuleExtractor("LessFiles\\ValidNonImporting.less");
            rules = extractor.ExtractRules();
            var formatter = new RazorFormatter("Formatting\\TestTemplate.cshtml");
            result = formatter.FormatDocumentation(rules, TODO);
        }

        [Test]
        public void TheRulesAreFormattedUsingTheSuppliedTemplate()
        {
            Assert.That(result, Is.EqualTo("Rules:\r\n" + string.Join("\r\n", rules.Select(r => r.Name)) + "\r\n"));
        }
    }
}