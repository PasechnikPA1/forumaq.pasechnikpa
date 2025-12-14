using ForumAQ.Data;

namespace ForumAQ.Data.Services
{
    public interface IForumService
    {
        // Методы для вопросов
        Task<List<Question>> GetQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<Question> CreateQuestionAsync(Question question, string userId);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int id);
        Task IncrementViewCountAsync(int questionId);

        // Методы для ответов
        Task<List<Answer>> GetAnswersForQuestionAsync(int questionId);
        Task<Answer> CreateAnswerAsync(Answer answer, string userId);
        Task UpdateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int id);
        Task MarkAnswerAsHelpfulAsync(int answerId);

        // Вспомогательные методы
        Task<List<Question>> SearchQuestionsAsync(string searchTerm);
        Task<List<Question>> GetQuestionsByUserAsync(string userId);
        Task<bool> AddThanksToAnswerAsync(int answerId, string userId);
        Task<int> GetUserThanksCountAsync(string userId);
        Task<bool> HasUserThankedAnswerAsync(int answerId, string userId);
        Task<int> GetUserQuestionsCountAsync(string userId);
        Task<int> GetUserAnswersCountAsync(string userId);
        Task<int> GetUserThanksReceivedCountAsync(string userId);
        Task<List<Question>> GetUserQuestionsAsync(string userId);
        Task<List<Answer>> GetUserAnswersAsync(string userId);
    }
}