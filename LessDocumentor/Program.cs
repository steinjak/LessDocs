using System;
using System.IO;

namespace LessDocs
{
    class Program
    {
        static void Main(string[] args)
        {
            var extractor = new RuleExtractor("Style.less");
            var formatter = new RazorFormatter("DefaultTemplate.cshtml");

            var rules = extractor.ExtractRules();
            var formatted = formatter.FormatDocumentation(rules, extractor.RenderCss());
            File.WriteAllText("output.html", formatted);

            Console.WriteLine("Wrote the documentation to output.html");
        }
    }
}
