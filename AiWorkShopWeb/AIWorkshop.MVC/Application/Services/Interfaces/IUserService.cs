using AIWorkshop.MVC.Data.Entities;

namespace AIWorkshop.MVC.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetCurrentUserAsync();
        Task<User> LoginOrCreateAsync(string username, string? email = null);
        Task SetCurrentUserAsync(User user);
        Task LogoutAsync();
        Task SaveScoreAsync(
            int userId, 
            string prompt, 
            int score,
            string rating,
            string componentScores, 
            List<string> recommendations, 
            string improvedPrompt);
        Task<PromptScore?> GetUserScoreAsync(int userId);
        Task<List<(User User, int BestScore)>> GetLeaderboardAsync(int top = 3);
    }
}