namespace AIWorkshop.MVC.Data.Entities
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = [];
        public int CorrectOptionIndex { get; set; }
        public string? Explanation { get; set; }
    }
}
