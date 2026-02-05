using AIWorkshop.MVC.Infrastructure.Agents.Utils.Enums;
using Microsoft.Agents.AI;

namespace AIWorkshop.MVC.Infrastructure.Agents
{
    public interface IAgentBase<T>
    {
        Task<T?> AskAsync(string prompt, AIProvider provider = AIProvider.OpenAI);
    }
}
