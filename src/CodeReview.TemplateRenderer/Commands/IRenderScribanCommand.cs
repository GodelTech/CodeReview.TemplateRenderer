using System.Threading.Tasks;
using GodelTech.CodeReview.TemplateRenderer.Options;

namespace GodelTech.CodeReview.TemplateRenderer.Commands
{
    public interface IRenderScribanCommand
    {
        Task<int> ExecuteAsync(ScribanOptions options);
    }
}