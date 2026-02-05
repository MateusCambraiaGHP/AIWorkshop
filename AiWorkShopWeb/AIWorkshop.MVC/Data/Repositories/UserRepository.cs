using AIWorkshop.MVC.Data;
using AIWorkshop.MVC.Data.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace AIWorkshop.MVC.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly ProtectedSessionStorage _sessionStorage;
        private const string UserSessionKey = "CurrentUserId";

        public UserRepository(
            AppDbContext db, 
            ProtectedSessionStorage sessionStorage)
        {
            _db = db;
            _sessionStorage = sessionStorage;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            try
            {
                var result = await _sessionStorage.GetAsync<int>(UserSessionKey);
                if (!result.Success) return null;

                return await _db.Users.FindAsync(result.Value);
            }
            catch
            {
                return null;
            }
        }

        public async Task<PromptScore?> GetUserScoreAsync(int userId)
        {
            return await _db.PromptScores
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Score)
                .FirstOrDefaultAsync();
        }

        public async Task<User> LoginOrCreateAsync(string username, string? email = null)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                user = new User { Username = username, Email = email };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }

            await SetCurrentUserAsync(user);
            return user;
        }

        public async Task SetCurrentUserAsync(User user)
        {
            await _sessionStorage.SetAsync(UserSessionKey, user.Id);
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.DeleteAsync(UserSessionKey);
        }

        public async Task SaveScoreAsync(PromptScore promptScore)
        {
            _db.PromptScores.Add(promptScore);
            await _db.SaveChangesAsync();
        }

        public async Task<List<LeaderboardEntry>> GetCombinedLeaderboardAsync(int top = 100)
        {
            var users = await _db.Users
                .Include(u => u.PromptScores)
                .Include(u => u.QuizScores)
                .ToListAsync();

            var leaderboard = users
                .Select(u => new LeaderboardEntry
                {
                    User = u,
                    PromptScore = u.PromptScores.OrderByDescending(p => p.Score).FirstOrDefault()?.Score ?? 0,
                    QuizScore = u.QuizScores.OrderByDescending(q => q.Score).FirstOrDefault()?.Score ?? 0,
                    TotalScore = (u.PromptScores.OrderByDescending(p => p.Score).FirstOrDefault()?.Score ?? 0) +
                                 (u.QuizScores.OrderByDescending(q => q.Score).FirstOrDefault()?.Score ?? 0)
                })
                .Where(e => e.TotalScore > 0)
                .OrderByDescending(e => e.TotalScore)
                .Take(top)
                .ToList();

            return leaderboard;
        }
    }
}