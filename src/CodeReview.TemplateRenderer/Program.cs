﻿using System;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using GodelTech.CodeReview.TemplateRenderer.Commands;
using GodelTech.CodeReview.TemplateRenderer.Models;
using GodelTech.CodeReview.TemplateRenderer.Options;
using GodelTech.CodeReview.TemplateRenderer.Services;
using GodelTech.CodeReview.TemplateRenderer.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GodelTech.CodeReview.TemplateRenderer
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
            return container.GetRequiredService<IRenderLiquidCommand>().ExecuteAsync(options);
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

            serviceProvider.AddTransient<IRenderLiquidCommand, RenderLiquidCommand>();

            return serviceProvider.BuildServiceProvider();
        }
    }

}
