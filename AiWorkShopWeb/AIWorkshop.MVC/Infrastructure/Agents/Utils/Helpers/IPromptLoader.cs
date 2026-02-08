using System.Runtime.CompilerServices;

namespace AIWorkshop.MVC.Infrastructure.Agents.Utils.Helpers
{
    public interface IPromptLoader
    {
        string Load(string agentFolder, string fileName);
    }
}
