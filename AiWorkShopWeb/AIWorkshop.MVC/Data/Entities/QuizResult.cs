namespace AIWorkshop.MVC.Data.Entities
{
    public class QuizResult
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Score { get; set; }
        public List<QuestionResult> Details { get; set; } = [];
    }

}
