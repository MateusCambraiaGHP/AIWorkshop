namespace AIWorkshop.MVC.Data.Entities
{
    public class QuizScore : EntityBase
    {
        public int UserId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public string Answers { get; set; } = string.Empty;

        public User User { get; set; } = null!;
    }
}
