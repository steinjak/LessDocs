using System;

namespace LessDocs
{
    class Program
    {
        static void Main(string[] args)
        {
            var extractor = new RuleExtractor("Style.less");
            var formatter = new RazorFormatter("DefaultTemplate.cshtml");

            var rules = extractor.ExtractRules();
            Console.WriteLine(formatter.FormatDocumentation(rules, extractor.RenderCss()));
        }
    }
}
