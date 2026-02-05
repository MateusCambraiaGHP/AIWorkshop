using AIWorkshop.MVC.Infrastructure.Agents.Utils.Enums;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIWorkshop.MVC.Infrastructure.Agents.Utils.Factory
{
    public interface IAIAgentFactory
    {
        AIAgent Create(
            AIProvider provider = AIProvider.OpenAI,
            ChatOptions? chatOptions = null);
    }
}
