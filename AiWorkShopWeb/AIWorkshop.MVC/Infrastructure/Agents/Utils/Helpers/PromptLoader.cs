using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIWorkshop.MVC.Infrastructure.Agents.Utils.Helpers
{
    public class PromptLoader : IPromptLoader
    {
        private const string RootNamespace = "AIWorkshop.MVC";

        public string Load(string fileName, [CallerFilePath] string callerFilePath = "")
        {
            var callerDir = Path.GetDirectoryName(callerFilePath) ?? string.Empty;
            var promptPath = Path.Combine(callerDir, "Prompts", fileName);

            var projectRoot = GetProjectRoot(callerFilePath);
            var relativePath = Path.GetRelativePath(projectRoot, promptPath);
            var resourceName = $"{RootNamespace}.{relativePath.Replace(Path.DirectorySeparatorChar, '.').Replace(Path.AltDirectorySeparatorChar, '.')}";

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Prompt resource '{resourceName}' not found.");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static string GetProjectRoot(string filePath)
        {
            var dir = new DirectoryInfo(Path.GetDirectoryName(filePath)!);
            while (dir is not null && !dir.GetFiles("*.csproj").Any())
            {
                dir = dir.Parent;
            }
            return dir?.FullName ?? throw new InvalidOperationException("Could not find project root.");
        }
    }
}
