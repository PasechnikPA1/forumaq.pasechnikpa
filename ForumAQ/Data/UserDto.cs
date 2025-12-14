using System.ComponentModel.DataAnnotations;

namespace ForumAQ.Data
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Отображаемое имя")]
        public string DisplayName { get; set; } = string.Empty;

        [Display(Name = "Роли")]
        public List<string> Roles { get; set; } = new List<string>();

        [Display(Name = "Отправлено благодарностей")]
        public int ThanksCount { get; set; }

        [Display(Name = "Получено благодарностей")]
        public int ThanksReceived { get; set; }

        [Display(Name = "Задал вопросов")]
        public int QuestionsAsked { get; set; }

        [Display(Name = "Дал ответов")]
        public int AnswersGiven { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Email подтвержден")]
        public bool EmailConfirmed { get; set; }
    }
}