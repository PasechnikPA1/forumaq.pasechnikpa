using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAQ.Data
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Текст ответа")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Дата последнего редактирования")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Полезный ответ")]
        public bool IsHelpful { get; set; } = false;

        [Display(Name = "Количество благодарностей")]
        public int ThanksCount { get; set; } = 0;

        // Внешний ключ для вопроса
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        // Навигационное свойство
        public virtual Question? Question { get; set; }

        // Внешний ключ для пользователя
        [ForeignKey("User")]
        public string? UserId { get; set; }

        // Навигационное свойство
        public virtual ApplicationUser? User { get; set; }
    }
}