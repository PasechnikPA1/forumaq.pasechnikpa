using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForumAQ.Data.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // Получить всех пользователей с их ролями и статистикой
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // Получаем актуальную статистику из базы данных
                var questionsAsked = await _context.Questions
                    .CountAsync(q => q.UserId == user.Id);

                var answersGiven = await _context.Answers
                    .CountAsync(a => a.UserId == user.Id);

                var thanksReceived = await _context.Answers
                    .Where(a => a.UserId == user.Id)
                    .SumAsync(a => a.ThanksCount);

                // Обновляем поля пользователя в базе данных
                user.QuestionsAsked = questionsAsked;
                user.AnswersGiven = answersGiven;
                user.ThanksReceived = thanksReceived;

                // Сохраняем обновления
                await _userManager.UpdateAsync(user);

                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Roles = roles.ToList(),
                    ThanksCount = user.ThanksCount, // Отправленные благодарности
                    ThanksReceived = user.ThanksReceived, // Полученные благодарности
                    QuestionsAsked = user.QuestionsAsked,
                    AnswersGiven = user.AnswersGiven,
                    RegistrationDate = user.RegistrationDate,
                    EmailConfirmed = user.EmailConfirmed
                });
            }

            return userDtos;
        }

        // Получить все доступные роли
        public async Task<List<string>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
        }

        // Обновить роли пользователя
        public async Task<bool> UpdateUserRolesAsync(string userId, List<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Удалить старые роли
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded) return false;

            // Добавить новые роли
            var addResult = await _userManager.AddToRolesAsync(user, selectedRoles);
            return addResult.Succeeded;
        }

        // Удалить пользователя
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        // Получить пользователя по ID
        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            // Получаем актуальную статистику
            var questionsAsked = await _context.Questions
                .CountAsync(q => q.UserId == user.Id);

            var answersGiven = await _context.Answers
                .CountAsync(a => a.UserId == user.Id);

            var thanksReceived = await _context.Answers
                .Where(a => a.UserId == user.Id)
                .SumAsync(a => a.ThanksCount);

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Roles = roles.ToList(),
                ThanksCount = user.ThanksCount,
                ThanksReceived = thanksReceived,
                QuestionsAsked = questionsAsked,
                AnswersGiven = answersGiven,
                RegistrationDate = user.RegistrationDate,
                EmailConfirmed = user.EmailConfirmed
            };
        }
    }
}