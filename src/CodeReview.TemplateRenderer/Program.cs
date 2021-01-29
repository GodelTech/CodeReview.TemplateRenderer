using System;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReviewItEasy.TemplateRenderer.Commands;
using ReviewItEasy.TemplateRenderer.Models;
using ReviewItEasy.TemplateRenderer.Options;
using ReviewItEasy.TemplateRenderer.Services;
using ReviewItEasy.TemplateRenderer.Utils;

namespace ReviewItEasy.TemplateRenderer
{
    class Program
    {
        private static int Main(string[] args)
        {
            using var container = CreateServiceProvider();

            var parser = new Parser(x =>
            {
                x.HelpWriter = TextWriter.Null;
            });

            var result = parser.ParseArguments<ScribanOptions>(args);

            var exitCode = result
                .MapResult(
                    (ScribanOptions x) => ProcessRenderScribanAsync(x, container).GetAwaiter().GetResult(),
                    _ => ProcessErrors(result));

            return exitCode;
        }

        private static Task<int> ProcessRenderScribanAsync(ScribanOptions options, IServiceProvider container)
        {
            return container.GetRequiredService<IRenderScribanCommand>().ExecuteAsync(options);
        }

        private static int ProcessErrors(ParserResult<ScribanOptions> result)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);

            Console.WriteLine(helpText);

            return Constants.ErrorExitCode;
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceProvider = new ServiceCollection();

            serviceProvider.AddLogging(x =>
            {
                x.ClearProviders();
                x.AddProvider(new SimplifiedConsoleLoggerProvider());
            });

            serviceProvider.AddSingleton<IFileService, FileService>();

            serviceProvider.AddTransient<IRenderScribanCommand, RenderScribanCommand>();

            return serviceProvider.BuildServiceProvider();
        }
    }

}
