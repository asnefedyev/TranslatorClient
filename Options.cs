using CommandLine;

namespace TranslatorClient
{
    public class Options
    {
        [Option('d', "definition", Required = true, HelpText = "Input a text for translate")]
        public string InputText { get; set; }

        [Option('s', "sourceLanguage", Required = true, HelpText = "The source language.", Default = "ru")]
        public string SourceLanguage { get; set; }

        [Option('t', "targetLanguage", Required = true, HelpText = "The target language.", Default = "en")]
        public string TargetLanguage { get; set; }
    }
}