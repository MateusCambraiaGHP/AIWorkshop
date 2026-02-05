using AIWorkshop.MVC.Data.Entities;

namespace AIWorkshop.MVC.Data.Repositories
{
    public interface IQuizRepository
    {
        List<QuizQuestion> GetQuestions();
        Task<QuizResult> SubmitQuizAsync(int userId, List<QuizAnswer> answers);
        Task<QuizScore?> GetUserQuizScoreAsync(int userId);
    }
}