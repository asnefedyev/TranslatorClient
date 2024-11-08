using Grpc.Net.Client;
using ExampleSpace;
using Microsoft.Extensions.Configuration;
using TranslatorClient;
using CommandLine;

class Program
{
    static async Task Main(string[] args)
    {
        await Parser.Default.ParseArguments<Options>(args)
          .WithParsedAsync(RunOptionsAndReturnExitCode);
    }

    private async static Task RunOptionsAndReturnExitCode(Options opts)
    {
        var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())       
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        var channel = GrpcChannel.ForAddress(configuration["freeUrl"] ?? "https://localhost:5000");
        var client = new Translater.TranslaterClient(channel);
        var request = new TranslateRequest
        {
            SourceLang = opts.SourceLanguage,
            TargetLang = opts.TargetLanguage,
            TranslateText = opts.InputText
        };

        TranslateResponse response = await client.DoTranslateAsync(request);

        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Translated text: {response.TranslateResult}");
        Console.ResetColor();

        await channel.ShutdownAsync();
    }
}