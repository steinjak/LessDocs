using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace LessDocs.Tests.Extraction
{
    [TestFixture]
    public class WhenExtractingRulesFromANonImportingLessDocument
    {
        private IEnumerable<DocumentedRule> rules;

        [SetUp]
        public void Given()
        {
            var extractor = new RuleExtractor("LessFiles\\ValidNonImporting.less");
            rules = extractor.ExtractRules();
        }

        [Test]
        public void RulesWithJavadocLikeSyntaxAreRecognized()
        {
            Assert.That(rules.Any(r => r.Name == "Defined with javadoc syntax"));
        }

        [Test]
        public void RulesWithSlashSyntaxAreRecognized()
        {
            Assert.That(rules.Any(r => r.Name == "Defined with slash-syntax"));
        }
    }
}