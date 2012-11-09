using System.Collections.Generic;
using System.IO;

namespace LessDocs
{
    public class RazorFormatter
    {
        private readonly string templatePath;

        public RazorFormatter(string templatePath)
        {
            this.templatePath = templatePath;
        }

        public string FormatDocumentation(IEnumerable<DocumentedRule> documentedRules, string css = "")
        {
            var template = File.ReadAllText(templatePath);
            return RazorEngine.Razor.Parse(template, new {Rules = documentedRules, Css = css});
        }
    }
}