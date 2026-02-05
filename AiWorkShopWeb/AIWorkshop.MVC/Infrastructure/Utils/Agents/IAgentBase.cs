using AIWorkshop.MVC.Infrastructure.Utils.Enums;
using Microsoft.Agents.AI;

namespace AIWorkshop.MVC.Infrastructure.Utils.Agents
{
    public interface IAgentBase<T>
    {
        Task<T?> AskAsync(string prompt, AIProvider provider = AIProvider.OpenAI);
    }
}
