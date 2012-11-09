using System.Linq;
using NUnit.Framework;

namespace LessDocs.Tests.Parsing
{
    [TestFixture]
    public class WhenParsingJavadocStyleComments
    {
        private DocumentedRule rule;

        private const string Comment =
            "/**\n" +
            " * @name Test rule     \n" +
            " * @category Tests\n" +
            " * @description Test description\n" +
            " * @example\n" +
            " * <div>\n" +
            " *   <span>Content here, indented</span>\n" +
            " * </div>\n" +
            " */";

        private const string ExpectedExample =
            "<div>\n" +
            "  <span>Content here, indented</span>\n" +
            "</div>";

        [SetUp]
        public void Given()
        {
            rule = new DocumentedRule(".test-rule", Comment);
        }

        [Test]
        public void TheNameIsParsed()
        {
            Assert.That(rule.Name, Is.EqualTo("Test rule"));
        }

        [Test]
        public void TheCategoryIsParsed()
        {
            Assert.That(rule.Category, Is.EqualTo("Tests"));
        }

        [Test]
        public void TheDescriptionIsParsed()
        {
            Assert.That(rule.Description, Is.EqualTo("Test description"));
        }

        [Test]
        public void TheExampleIsParsed()
        {
            var example = rule.Examples.FirstOrDefault();
            Assert.That(example, Is.Not.Null);
            Assert.That(example.Contains("Content here, indented"));
        }

        [Test]
        public void TheExampleRetainsIndentation()
        {
            Assert.That(rule.Examples.FirstOrDefault(), Is.EqualTo(ExpectedExample));
        }
    }
}