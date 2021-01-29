using System.Threading.Tasks;
using ReviewItEasy.TemplateRenderer.Options;

namespace ReviewItEasy.TemplateRenderer.Commands
{
    public interface IRenderScribanCommand
    {
        Task<int> ExecuteAsync(ScribanOptions options);
    }
}