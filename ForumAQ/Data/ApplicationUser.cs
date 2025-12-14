using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ForumAQ.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Отображаемое имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 50 символов")]
        public string DisplayName { get; set; } = string.Empty;

        [Display(Name = "Благодарности")]
        public int ThanksCount { get; set; } = 0;

        [Display(Name = "О себе")]
        [StringLength(500, ErrorMessage = "Максимум 500 символов")]
        public string? Bio { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Display(Name = "Дата обновления")]
        public DateTime? UpdatedDate { get; set; }

        // Навигационные свойства для вопросов и ответов
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

        // Дополнительные свойства для статистики
        [Display(Name = "Задал вопросов")]
        public int QuestionsAsked { get; set; } = 0;

        [Display(Name = "Дал ответов")]
        public int AnswersGiven { get; set; } = 0;

        [Display(Name = "Получено благодарностей")]
        public int ThanksReceived { get; set; } = 0;
    }
}