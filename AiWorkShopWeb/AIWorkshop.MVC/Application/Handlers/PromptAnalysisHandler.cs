using AIWorkshop.MVC.Application.Handlers.Interfaces;
using AIWorkshop.MVC.Application.Requests;
using AIWorkshop.MVC.Application.Responses;
using AIWorkshop.MVC.Infrastructure.Agents.PromptAnalysis;
using System.Text.Json;

namespace AIWorkshop.MVC.Application.Handlers
{
    public class PromptAnalysisHandler : IPromptAnalysisHandler
    {
        private readonly IPromptAnalysisAgent _agent;

        public PromptAnalysisHandler(IPromptAnalysisAgent agent)
        {
            _agent = agent;
        }

        public async Task<PromptAnalysisResponse> HandleAsync(
              PromptAnalysisRequest request,
              CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return new PromptAnalysisResponse();

            var response = await _agent.AskAsync(request.Prompt);

            return response ?? new PromptAnalysisResponse();
        }
    }
}
