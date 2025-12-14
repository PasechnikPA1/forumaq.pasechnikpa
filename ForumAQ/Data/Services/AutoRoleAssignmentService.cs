using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ForumAQ.Data.Services
{
    public interface IAutoRoleAssignmentService
    {
        Task AssignRoleOnRegistrationAsync(ApplicationUser user);
        Task AssignRoleOnEmailConfirmationAsync(string userId, string email);
    }

    public class AutoRoleAssignmentService : IAutoRoleAssignmentService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AutoRoleAssignmentService> _logger;

        public AutoRoleAssignmentService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AutoRoleAssignmentService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task AssignRoleOnRegistrationAsync(ApplicationUser user)
        {
            try
            {
                _logger.LogInformation($"Назначение роли при регистрации для: {user.Email}");

                // Проверяем существует ли роль "User"
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                    _logger.LogInformation("Создана роль 'User'");
                }

                // Проверяем, есть ли уже у пользователя роль "User"
                var isInRole = await _userManager.IsInRoleAsync(user, "User");
                if (!isInRole)
                {
                    // Добавляем пользователю роль "User"
                    await _userManager.AddToRoleAsync(user, "User");
                    _logger.LogInformation($"Пользователю {user.UserName} присвоена роль 'User'");
                }
                else
                {
                    _logger.LogInformation($"Пользователь {user.UserName} уже имеет роль 'User'");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при назначении роли пользователю {user.Email}");
            }
        }

        public async Task AssignRoleOnEmailConfirmationAsync(string userId, string email)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId) ??
                          await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    await AssignRoleOnRegistrationAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при назначении роли после подтверждения email");
            }
        }
    }
}