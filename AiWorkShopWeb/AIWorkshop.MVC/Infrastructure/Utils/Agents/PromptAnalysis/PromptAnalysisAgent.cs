using AIWorkshop.MVC.Application.Responses;
using AIWorkshop.MVC.Infrastructure.Utils.Agents.PromptAnalysis.Interfaces;
using AIWorkshop.MVC.Infrastructure.Utils.Factories.Interfaces;
using AIWorkshop.MVC.Infrastructure.Utils.Helpers.Interfaces;

namespace AIWorkshop.MVC.Infrastructure.Utils.Agents.PromptAnalysis
{
    public class PromptAnalysisAgent : AgentBase<PromptAnalysisResponse>, IPromptAnalysisAgent
    {
        public PromptAnalysisAgent(
            IAIAgentFactory agentFactory,
            IPromptLoader promptLoader)
            : base(agentFactory, promptLoader.LoadRelative("PromptAnalysis.md")) { }
    }
}
