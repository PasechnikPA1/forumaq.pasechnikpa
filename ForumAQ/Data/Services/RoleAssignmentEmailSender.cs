using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace ForumAQ.Data.Services
{
    public class RoleAssignmentEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender<ApplicationUser> _emailSender;
        private readonly IAutoRoleAssignmentService _roleAssignmentService;
        private readonly ILogger<RoleAssignmentEmailSender> _logger;

        public RoleAssignmentEmailSender(
            IEmailSender<ApplicationUser> emailSender,
            IAutoRoleAssignmentService roleAssignmentService,
            ILogger<RoleAssignmentEmailSender> logger)
        {
            _emailSender = emailSender;
            _roleAssignmentService = roleAssignmentService;
            _logger = logger;
        }

        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            // Сначала отправляем письмо подтверждения
            await _emailSender.SendConfirmationLinkAsync(user, email, confirmationLink);

            _logger.LogInformation($"Отправлено письмо подтверждения для {user.Email}");
        }

        public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            await _emailSender.SendPasswordResetLinkAsync(user, email, resetLink);
        }

        public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            await _emailSender.SendPasswordResetCodeAsync(user, email, resetCode);
        }
    }
}