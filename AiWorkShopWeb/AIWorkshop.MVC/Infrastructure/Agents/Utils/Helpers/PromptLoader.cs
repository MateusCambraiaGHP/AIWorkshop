using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIWorkshop.MVC.Infrastructure.Agents.Utils.Helpers
{
    public class PromptLoader : IPromptLoader
    {
        private const string RootNamespace = "AIWorkshop.MVC";
        private readonly Assembly _assembly;

        public PromptLoader()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public string Load(string agentFolder, string fileName)
        {
            // Build resource name: AIWorkshop.MVC.Infrastructure.Agents.{AgentFolder}.Prompts.{FileName}
            var resourceName = $"{RootNamespace}.Infrastructure.Agents.{agentFolder}.Prompts.{fileName}";

            using var stream = _assembly.GetManifestResourceStream(resourceName);

            if (stream is null)
            {
                // List available resources for debugging
                var available = string.Join(", ", _assembly.GetManifestResourceNames());
                throw new InvalidOperationException(
                    $"Prompt resource '{resourceName}' not found. Available resources: {available}");
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
