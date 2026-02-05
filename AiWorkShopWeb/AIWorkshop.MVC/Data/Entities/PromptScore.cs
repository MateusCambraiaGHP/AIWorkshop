namespace AIWorkshop.MVC.Data.Entities
{
    public class PromptScore : EntityBase
    {
        public int UserId { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public int Score { get; set; }
        public string Rating { get; set; } = string.Empty;
        public string ComponentScores { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public string ImprovedPrompt { get; set; } = string.Empty;
        

        public User User { get; set; } = null!;
    }
}
