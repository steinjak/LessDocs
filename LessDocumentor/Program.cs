namespace LessDocumentor
{
    class Program
    {
        static void Main(string[] args)
        {
            var rules = RuleExtractor.ExtractRules("Style.less");
            RuleFormatter.FormatDocumentation(rules);
        }
    }
}
