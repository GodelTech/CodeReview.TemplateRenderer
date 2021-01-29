using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReviewItEasy.TemplateRenderer.Models;
using ReviewItEasy.TemplateRenderer.Options;
using ReviewItEasy.TemplateRenderer.Services;
using Scriban.Parsing;
using Scriban.Runtime;

namespace ReviewItEasy.TemplateRenderer.Commands
{
    public class RenderScribanCommand : IRenderScribanCommand
    {
        private readonly IFileService _fileService;
        private readonly ILogger<RenderScribanCommand> _logger;
        private static string _renamedKey;

        public RenderScribanCommand(IFileService fileService, ILogger<RenderScribanCommand> logger)
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

            var template = await _fileService.ReadAllTextAsync(options.TemplatePath);

            _logger.LogInformation("Data file read.");

            _logger.LogInformation("Rendering template...");

            var renderedTemplate = RenderJson(dataJson, template);

            await _fileService.WriteAllTextAsync(options.OutputPath, renderedTemplate);

            _logger.LogInformation("Template rendered");

            return Constants.SuccessExitCode;
        }

        private static string RenderJson(string json, string content)
        {
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(json);
            var scriptObject = BuildScriptObject(expando);
            
            var templateContext = new Scriban.TemplateContext
            {
                EnableRelaxedMemberAccess = true
            };

            templateContext.PushGlobal(scriptObject);
            
            var template = Scriban.Template.Parse(content);
            var result = template.Render(templateContext);

            return result;
        }

        private static ScriptObject BuildScriptObject(ExpandoObject expando)
        {
            var scriptObject = new ScriptObject();

            foreach (var (key, value) in expando)
            {
                if (value is ExpandoObject expandoValue)
                {
                    scriptObject.Add(key, BuildScriptObject(expandoValue));
                }
                else
                {
                    scriptObject.Add(key, value);
                }
            }

            return scriptObject;
        }
    }
}
