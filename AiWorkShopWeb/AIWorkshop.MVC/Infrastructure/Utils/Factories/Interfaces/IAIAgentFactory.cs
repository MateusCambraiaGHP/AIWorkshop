using AIWorkshop.MVC.Infrastructure.Utils.Enums;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIWorkshop.MVC.Infrastructure.Utils.Factories.Interfaces
{
    public interface IAIAgentFactory
    {
        AIAgent Create(
            AIProvider provider = AIProvider.OpenAI,
            ChatOptions? chatOptions = null);
    }
}
