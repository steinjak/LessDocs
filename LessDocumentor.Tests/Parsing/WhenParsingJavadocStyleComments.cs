using NUnit.Framework;

namespace LessDocumentor.Tests.Parsing
{
    [TestFixture]
    public class WhenParsingJavadocStyleComments
    {
        private DocumentedRule rule;

        private const string Comment =
            "/**\n" +
            " * @name Test rule     \n" +
            " * @category Tests\n" +
            " * @example\n" +
            " * <div>\n" +
            " *   <span>Content here, indented</span>\n" +
            " * </div>\n" +
            " */";

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
        public void TheExampleIsParsed()
        {
            Assert.That(rule.Example.Contains("Content here, indented"));
        }
    }
}