using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LessDocs.Tests.Extraction
{
    [TestFixture]
    public class WhenExtractingRulesFromAnImportingLessDocument
    {
        private IEnumerable<DocumentedRule> rules;

        [SetUp]
        public void Given()
        {
            var extractor = new RuleExtractor("LessFiles\\ValidImporting.less");
            rules = extractor.ExtractRules();
        }

        [Test]
        public void RulesInTheRootDocumentAreRecognized()
        {
            Assert.That(rules.Any(r => r.Name == "In the importing file"));
        }

        [Test]
        public void RulesInTheImportedDocumentAreRecognized()
        {
            Assert.That(rules.Any(r => r.Name == "Defined with javadoc syntax"));
        }
    }
}