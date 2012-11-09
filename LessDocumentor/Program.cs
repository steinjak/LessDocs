using System;

namespace LessDocs
{
    class Program
    {
        static void Main(string[] args)
        {
            var extractor = new RuleExtractor();
            var formatter = new RazorFormatter("DefaultTemplate.cshtml");

            var rules = extractor.ExtractRules("Style.less");
            Console.WriteLine(formatter.FormatDocumentation(rules));
        }
    }
}
