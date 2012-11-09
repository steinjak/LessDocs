using System.Linq;
using NUnit.Framework;

namespace LessDocs.Tests.Parsing
{
    [TestFixture]
    public class WhenParsingSlashSyntaxComments
    {
        private DocumentedRule rule;

        // note the windows-style line endings (we'll support both unix and windows style endings)
        private const string Comment =
            "// @name Test rule     \r\n" +
            "// @category Tests\r\n" +
            "// @example\r\n" +
            "// <div>\r\n" +
            "//   <span>Content here, indented</span>\r\n" +
            "// </div>\r\n";

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
        public void TheExampleIsParsed()
        {
            Assert.That(rule.Examples.FirstOrDefault().Contains("Content here, indented"));
        }

        [Test]
        public void TheExampleRetainsIndentation()
        {
            Assert.That(rule.Examples.FirstOrDefault(), Is.EqualTo(ExpectedExample));
        }
    }
}