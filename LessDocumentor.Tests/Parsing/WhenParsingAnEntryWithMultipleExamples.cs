using System.Linq;
using NUnit.Framework;

namespace LessDocs.Tests.Parsing
{
    [TestFixture]
    public class WhenParsingAnEntryWithMultipleExamples
    {
        private DocumentedRule rule;
        private const string Comment =
            "// @name Test with multiple examples\n" +
            "// @example\n" +
            "// This is the first example\n" +
            "// @example This is the second\n" +
            "// example\n" +
            "// @example\n" +
            "// This is the third example\n";

        [SetUp]
        public void Given()
        {
            rule = new DocumentedRule(".test-rule", Comment);
        }

        [Test]
        public void AllExamplesAreRecognized()
        {
            Assert.That(rule.Examples.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ExampleTextsAreCorrect()
        {
            Assert.That(rule.Examples.First(), Is.EqualTo("This is the first example"));
            Assert.That(rule.Examples.Skip(1).First(), Is.EqualTo("This is the second\nexample"));
            Assert.That(rule.Examples.Skip(2).First(), Is.EqualTo("This is the third example"));
        }
    }
}