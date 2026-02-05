namespace AIWorkshop.MVC.Data.Entities
{
    public class LeaderboardEntry
    {
        public User User { get; set; } = null!;
        public int PromptScore { get; set; }
        public int QuizScore { get; set; }
        public int TotalScore { get; set; }
    }
}
