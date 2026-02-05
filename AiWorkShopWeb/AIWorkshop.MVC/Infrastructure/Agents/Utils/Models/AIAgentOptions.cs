using AIWorkshop.MVC.Infrastructure.Agents.Utils.Enums;

namespace AIWorkshop.MVC.Infrastructure.Agents.Utils.Models
{
    public class AIAgentOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public AIProvider Provider { get; set; } = AIProvider.OpenAI;
    }
}
