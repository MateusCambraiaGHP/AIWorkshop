using System.Runtime.CompilerServices;

namespace AIWorkshop.MVC.Infrastructure.Utils.Helpers.Interfaces
{
    public interface IPromptLoader
    {
        string Load(string fileName);
        string LoadRelative(string fileName, [CallerFilePath] string callerFilePath = "");
    }
}
