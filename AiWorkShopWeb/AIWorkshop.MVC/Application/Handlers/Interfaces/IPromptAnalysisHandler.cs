using AIWorkshop.MVC.Application.Requests;
using AIWorkshop.MVC.Application.Responses;

namespace AIWorkshop.MVC.Application.Handlers.Interfaces
{
    public interface IPromptAnalysisHandler : IRequestHandler<PromptAnalysisRequest, PromptAnalysisResponse> { }
}
