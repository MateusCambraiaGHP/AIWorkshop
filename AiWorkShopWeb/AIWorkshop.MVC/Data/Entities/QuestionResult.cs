namespace AIWorkshop.MVC.Data.Entities
{
    public class QuestionResult
    {
        public int QuestionId { get; set; }
        public string Question { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int SelectedOptionIndex { get; set; }
        public int CorrectOptionIndex { get; set; }
        public string? Explanation { get; set; }
    }
}
