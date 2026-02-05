using AIWorkshop.MVC.Infrastructure.Utils.Enums;

namespace AIWorkshop.MVC.Infrastructure.Utils.Models
{
    public class AIAgentOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public AIProvider Provider { get; set; } = AIProvider.OpenAI;
    }
}
