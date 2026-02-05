using AIWorkshop.MVC.Application.Responses;
using AIWorkshop.MVC.Infrastructure.Agents;
using AIWorkshop.MVC.Infrastructure.Agents.Utils.Factory;
using AIWorkshop.MVC.Infrastructure.Agents.Utils.Helpers;

namespace AIWorkshop.MVC.Infrastructure.Agents.PromptAnalysis
{
    public class PromptAnalysisAgent : AgentBase<PromptAnalysisResponse>, IPromptAnalysisAgent
    {
        public PromptAnalysisAgent(
            IAIAgentFactory agentFactory,
            IPromptLoader promptLoader)
            : base(agentFactory, promptLoader.Load("PromptAnalysis.md")) { }
    }
}
