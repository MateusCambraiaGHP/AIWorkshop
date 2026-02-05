namespace AIWorkshop.MVC.Data.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }

        public ICollection<PromptScore> PromptScores { get; set; } = [];
        public ICollection<QuizScore> QuizScores { get; set; } = [];
    }
}
