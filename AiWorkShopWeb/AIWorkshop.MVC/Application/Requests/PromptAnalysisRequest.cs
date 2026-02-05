using AIWorkshop.MVC.Infrastructure.Agents.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace AIWorkshop.MVC.Application.Requests
{
    public class PromptAnalysisRequest
    {
        public string Prompt { get; init; } = string.Empty;
        public AIProvider Provider { get; init; } = AIProvider.OpenAI;
    }
}
