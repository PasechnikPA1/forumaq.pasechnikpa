using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ForumAQ.Data.Services
{
    public class ForumService : IForumService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ForumService> _logger;

        public ForumService(ApplicationDbContext context, ILogger<ForumService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // === ВОПРОСЫ ===

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Question> CreateQuestionAsync(Question question, string userId)
        {
            question.UserId = userId;
            question.CreatedDate = DateTime.Now;
            question.UpdatedDate = DateTime.Now;

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Создан новый вопрос: {question.Title} (ID: {question.Id})");
            return question;
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            question.UpdatedDate = DateTime.Now;
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementViewCountAsync(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                question.ViewCount++;
                await _context.SaveChangesAsync();
            }
        }

        // === ОТВЕТЫ ===

        public async Task<List<Answer>> GetAnswersForQuestionAsync(int questionId)
        {
            return await _context.Answers
                .Include(a => a.User)
                .Where(a => a.QuestionId == questionId)
                .OrderBy(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<Answer> CreateAnswerAsync(Answer answer, string userId)
        {
            answer.UserId = userId;
            answer.CreatedDate = DateTime.Now;

            _context.Answers.Add(answer);

            // Увеличиваем счетчик ответов у вопроса
            var question = await _context.Questions.FindAsync(answer.QuestionId);
            if (question != null)
            {
                question.AnswerCount++;
                question.UpdatedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Добавлен ответ к вопросу ID: {answer.QuestionId}");
            return answer;
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            answer.UpdatedDate = DateTime.Now;
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswerAsync(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer != null)
            {
                // Уменьшаем счетчик ответов у вопроса
                var question = await _context.Questions.FindAsync(answer.QuestionId);
                if (question != null)
                {
                    question.AnswerCount--;
                }

                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAnswerAsHelpfulAsync(int answerId)
        {
            var answer = await _context.Answers.FindAsync(answerId);
            if (answer != null)
            {
                answer.IsHelpful = true;
                await _context.SaveChangesAsync();

                // Можно также отметить вопрос как решенный
                var question = await _context.Questions.FindAsync(answer.QuestionId);
                if (question != null)
                {
                    question.IsSolved = true;
                    await _context.SaveChangesAsync();
                }
            }
        }

        // === ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ===

        public async Task<List<Question>> SearchQuestionsAsync(string searchTerm)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Where(q => q.Title.Contains(searchTerm) ||
                           q.Description.Contains(searchTerm) ||
                           (q.Tags != null && q.Tags.Contains(searchTerm)))
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByUserAsync(string userId)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();
        }
        public async Task<bool> AddThanksToAnswerAsync(int answerId, string userId)
        {
            try
            {
                // Проверяем, не благодарил ли уже пользователь этот ответ
                var alreadyThanked = await _context.ThanksHistories
                    .AnyAsync(th => th.AnswerId == answerId && th.UserId == userId);

                if (alreadyThanked)
                {
                    return false; // Уже благодарили
                }

                var answer = await _context.Answers
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => a.Id == answerId);

                if (answer == null || answer.User == null)
                {
                    return false;
                }

                // Добавляем благодарность в историю
                var thanks = new ThanksHistory
                {
                    AnswerId = answerId,
                    UserId = userId,
                    ThankedDate = DateTime.Now
                };

                _context.ThanksHistories.Add(thanks);

                // Увеличиваем счетчик благодарностей у ответа
                answer.ThanksCount++;

                // Увеличиваем счетчик полученных благодарностей у пользователя, который дал ответ
                answer.User.ThanksReceived++;

                // Увеличиваем счетчик отправленных благодарностей у пользователя, который отправляет
                var thankingUser = await _context.Users.FindAsync(userId);
                if (thankingUser != null)
                {
                    thankingUser.ThanksCount++;
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Пользователь {userId} поблагодарил ответ {answerId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при добавлении благодарности к ответу {answerId}");
                return false;
            }
        }

        public async Task<int> GetUserThanksCountAsync(string userId)
        {
            return await _context.ThanksHistories
                .CountAsync(th => th.UserId == userId);
        }

        public async Task<bool> HasUserThankedAnswerAsync(int answerId, string userId)
        {
            return await _context.ThanksHistories
                .AnyAsync(th => th.AnswerId == answerId && th.UserId == userId);
        }

        public async Task<int> GetUserQuestionsCountAsync(string userId)
        {
            return await _context.Questions
                .CountAsync(q => q.UserId == userId);
        }

        public async Task<int> GetUserAnswersCountAsync(string userId)
        {
            return await _context.Answers
                .CountAsync(a => a.UserId == userId);
        }

        public async Task<int> GetUserThanksReceivedCountAsync(string userId)
        {
            return await _context.Answers
                .Where(a => a.UserId == userId)
                .SumAsync(a => a.ThanksCount);
        }

        public async Task<List<Question>> GetUserQuestionsAsync(string userId)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Answer>> GetUserAnswersAsync(string userId)
        {
            return await _context.Answers
                .Include(a => a.User)
                .Include(a => a.Question)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }
    }
}