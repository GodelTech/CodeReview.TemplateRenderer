using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotLiquid;
using GodelTech.CodeReview.TemplateRenderer.Models;
using GodelTech.CodeReview.TemplateRenderer.Options;
using GodelTech.CodeReview.TemplateRenderer.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GodelTech.CodeReview.TemplateRenderer.Commands
{
    public class RenderLiquidCommand : IRenderLiquidCommand
    {
        private readonly IFileService _fileService;
        private readonly ILogger<RenderLiquidCommand> _logger;

        public RenderLiquidCommand(IFileService fileService, ILogger<RenderLiquidCommand> logger)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<int> ExecuteAsync(ScribanOptions options)
        {
            if (options == null) 
                throw new ArgumentNullException(nameof(options));

            _logger.LogInformation("Reading data file. File = {filePath}", options.DataPath);
            
            var dataJson = await _fileService.ReadAllTextAsync(options.DataPath);
            
            _logger.LogInformation("Data file read.");
            _logger.LogInformation("Reading template file. File = {filePath}", options.TemplatePath);

            var templateText = await _fileService.ReadAllTextAsync(options.TemplatePath);

            _logger.LogInformation("Data file read.");

            _logger.LogInformation("Rendering template...");

            var json = JsonConvert.DeserializeObject<IDictionary<string, object>>(dataJson, new DictionaryConverter());
            var jsonHash = Hash.FromDictionary(json);

            var template = Template.Parse(templateText);
            var renderedTemplate = template.Render(jsonHash);            

            await _fileService.WriteAllTextAsync(options.OutputPath, renderedTemplate);

            _logger.LogInformation("Template rendered");

            return Constants.SuccessExitCode;
        }
    }
}
