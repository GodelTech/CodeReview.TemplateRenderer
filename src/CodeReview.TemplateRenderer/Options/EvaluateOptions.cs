using CommandLine;

namespace GodelTech.CodeReview.TemplateRenderer.Options
{
    [Verb("liquid", HelpText = "Create issue summary using provided manifest.")]
    public class ScribanOptions
    {
        [Option('t', "template", Required = true, HelpText = "Path to scriban template file")]
        public string TemplatePath { get; set; }
        
        [Option('d', "data", Required = true, HelpText = "Path to data file")]
        public string DataPath { get; set; }
        
        [Option('o', "output", Required = true, HelpText = "Output file path")]
        public string OutputPath { get; set; }
    }
}