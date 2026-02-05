using System.ComponentModel;

namespace AIWorkshop.MVC.Application.Responses
{
    public class PromptAnalysisResponse
    {
        [Description("Overall TCREI score out of 25 points")]
        public int OverallScore { get; set; }

        [Description("Rating category based on score: Excellent (23-25), Very Good (20-22), Good (17-19), Fair (14-16), or Needs Revision (<14)")]
        public string Rating { get; set; } = string.Empty;

        [Description("Individual scores for each TCREI component: Task, Context, References, Evaluate, Iterate (each scored 1-5)")]
        public string ComponentScores { get; set; } = string.Empty;

        [Description("Top 3 actionable recommendations to improve the prompt quality")]
        public List<string> Recommendations { get; set; } = new List<string>();

        [Description("Improved version of the prompt incorporating all TCREI principles (optional)")]
        public string ImprovedPrompt { get; set; } = string.Empty;
    }
}
