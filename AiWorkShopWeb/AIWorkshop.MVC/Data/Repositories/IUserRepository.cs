using AIWorkshop.MVC.Data.Entities;

namespace AIWorkshop.MVC.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetCurrentUserAsync();
        Task<User> LoginOrCreateAsync(string username, string? email = null);
        Task SetCurrentUserAsync(User user);
        Task LogoutAsync();
        Task SaveScoreAsync(PromptScore promptScore);
        Task<PromptScore?> GetUserScoreAsync(int userId);
        Task<List<LeaderboardEntry>> GetCombinedLeaderboardAsync(int top = 100);
    }
}