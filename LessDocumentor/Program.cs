namespace LessDocumentor
{
    class Program
    {
        static void Main(string[] args)
        {
            var extractor = new RuleExtractor();
            var formatter = new RuleFormatter();

            var rules = extractor.ExtractRules("Style.less");
            formatter.FormatDocumentation(rules);
        }
    }
}
