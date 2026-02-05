using AIWorkshop.MVC.Application.Services.Interfaces;
using AIWorkshop.MVC.Data;
using AIWorkshop.MVC.Data.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace AIWorkshop.MVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly ProtectedSessionStorage _sessionStorage;
        private const string UserSessionKey = "CurrentUserId";

        public UserService(
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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);

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

        public async Task SaveScoreAsync(
            int userId, 
            string prompt, 
            int score, 
            string rating,
            string componentScores, 
            List<string> recommendations, 
            string improvedPrompt)
        {
            var promptScore = new PromptScore
            {
                UserId = userId,
                Prompt = prompt,
                Score = score,
                Rating = rating,
                ComponentScores = componentScores,
                Recommendations = string.Join("|||", recommendations),
                ImprovedPrompt = improvedPrompt
            };

            _db.PromptScores.Add(promptScore);
            await _db.SaveChangesAsync();
        }

        public async Task<List<(User User, int BestScore)>> GetLeaderboardAsync(int top = 3)
        {
            var leaderboard = await _db.PromptScores
                .Include(p => p.User)
                .GroupBy(p => p.User)
                .Select(g => new { User = g.Key, BestScore = g.Max(p => p.Score) })
                .OrderByDescending(x => x.BestScore)
                .Take(top)
                .ToListAsync();

            return leaderboard.Select(x => (x.User, x.BestScore)).ToList();
        }
    }
}