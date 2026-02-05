using AIWorkshop.MVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AIWorkshop.MVC.Data.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _db;

        // Hardcoded questions about AI/Prompt Engineering
        private static readonly List<QuizQuestion> _questions =
        [
            new()
        {
            Id = 1,
            Question = "What does 'TCREI' stand for in prompt engineering?",
            Options = [
                "Task, Context, References, Evaluate, Iterate",
                "Think, Create, Review, Execute, Improve",
                "Test, Code, Run, Edit, Implement",
                "Train, Classify, Refine, Extract, Infer"
            ],
            CorrectOptionIndex = 0,
            Explanation = "TCREI is Google's framework: Task, Context, References, Evaluate, Iterate."
        },
        new()
        {
            Id = 2,
            Question = "Which of these is a best practice when writing prompts for AI?",
            Options = [
                "Be as vague as possible to give AI freedom",
                "Use complex jargon without explanation",
                "Provide clear context and specific instructions",
                "Write everything in one long sentence"
            ],
            CorrectOptionIndex = 2,
            Explanation = "Clear context and specific instructions help AI understand exactly what you need."
        },
        new()
        {
            Id = 3,
            Question = "What is 'few-shot prompting'?",
            Options = [
                "Asking the AI to guess the answer",
                "Providing examples in the prompt to guide the AI",
                "Using the AI only for short tasks",
                "Limiting the AI's response length"
            ],
            CorrectOptionIndex = 1,
            Explanation = "Few-shot prompting includes examples in your prompt to show the AI the pattern you want."
        },
        new()
        {
            Id = 4,
            Question = "What is the purpose of the 'Context' in TCREI?",
            Options = [
                "To confuse the AI",
                "To provide background information and constraints",
                "To make the prompt longer",
                "To test the AI's memory"
            ],
            CorrectOptionIndex = 1,
            Explanation = "Context provides background, audience info, constraints, and purpose for the task."
        },
        new()
        {
            Id = 5,
            Question = "Which technique helps reduce AI hallucinations?",
            Options = [
                "Asking the AI to make up information",
                "Providing reference materials and asking to cite sources",
                "Using shorter prompts",
                "Avoiding questions entirely"
            ],
            CorrectOptionIndex = 1,
            Explanation = "Providing references and asking for citations helps ground the AI in factual information."
        },
        new()
        {
            Id = 6,
            Question = "What is 'chain-of-thought' prompting?",
            Options = [
                "Asking multiple unrelated questions",
                "Encouraging the AI to show its reasoning step by step",
                "Chaining multiple AI models together",
                "Writing very long prompts"
            ],
            CorrectOptionIndex = 1,
            Explanation = "Chain-of-thought prompting asks the AI to explain its reasoning process step by step."
        },
        new()
        {
            Id = 7,
            Question = "What does 'Iterate' mean in the TCREI framework?",
            Options = [
                "Give up after the first try",
                "Copy the same prompt multiple times",
                "Refine and improve the prompt based on results",
                "Iterate through all AI models"
            ],
            CorrectOptionIndex = 2,
            Explanation = "Iteration involves refining your prompt based on the AI's responses to improve results."
        },
        new()
        {
            Id = 8,
            Question = "Which is a good way to specify the output format you want?",
            Options = [
                "Hope the AI guesses correctly",
                "Explicitly state the format (e.g., 'Respond in JSON format')",
                "Use only emojis",
                "Ask for the longest possible response"
            ],
            CorrectOptionIndex = 1,
            Explanation = "Explicitly stating the desired format helps ensure you get a usable response."
        },
        new()
        {
            Id = 9,
            Question = "What is a 'system prompt'?",
            Options = [
                "A prompt about computer systems",
                "Instructions that define the AI's behavior and persona",
                "An error message from the AI",
                "A prompt written by the AI itself"
            ],
            CorrectOptionIndex = 1,
            Explanation = "System prompts set up the AI's role, personality, and behavioral guidelines."
        },
        new()
        {
            Id = 10,
            Question = "Why is it important to specify what to avoid in a prompt?",
            Options = [
                "It's not important",
                "To make the prompt longer",
                "To prevent unwanted content or formats in the response",
                "To confuse the AI"
            ],
            CorrectOptionIndex = 2,
            Explanation = "Specifying what to avoid helps prevent the AI from including unwanted content."
        }
        ];

        public QuizRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<QuizQuestion> GetQuestions()
        {
            // Return questions WITHOUT the correct answer index for the frontend
            return _questions.Select(q => new QuizQuestion
            {
                Id = q.Id,
                Question = q.Question,
                Options = q.Options,
                CorrectOptionIndex = -1, // Hide correct answer
                Explanation = null // Hide explanation
            }).ToList();
        }

        public async Task<QuizResult> SubmitQuizAsync(int userId, List<QuizAnswer> answers)
        {
            var result = new QuizResult
            {
                TotalQuestions = _questions.Count,
                Details = []
            };

            foreach (var question in _questions)
            {
                var userAnswer = answers.FirstOrDefault(a => a.QuestionId == question.Id);
                var isCorrect = userAnswer?.SelectedOptionIndex == question.CorrectOptionIndex;

                if (isCorrect) result.CorrectAnswers++;

                result.Details.Add(new QuestionResult
                {
                    QuestionId = question.Id,
                    Question = question.Question,
                    IsCorrect = isCorrect,
                    SelectedOptionIndex = userAnswer?.SelectedOptionIndex ?? -1,
                    CorrectOptionIndex = question.CorrectOptionIndex,
                    Explanation = question.Explanation
                });
            }

            // Calculate score (0-25 to match prompt analysis scale)
            result.Score = (int)Math.Round((double)result.CorrectAnswers / result.TotalQuestions * 25);

            // Save to database
            var quizScore = new QuizScore
            {
                UserId = userId,
                Score = result.Score,
                TotalQuestions = result.TotalQuestions,
                CorrectAnswers = result.CorrectAnswers,
                Answers = JsonSerializer.Serialize(answers)
            };

            _db.QuizScores.Add(quizScore);
            await _db.SaveChangesAsync();

            return result;
        }

        public async Task<QuizScore?> GetUserQuizScoreAsync(int userId)
        {
            return await _db.QuizScores
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.Score)
                .FirstOrDefaultAsync();
        }
    }
}