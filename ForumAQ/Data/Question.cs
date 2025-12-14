using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAQ.Data
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Заголовок должен быть от 5 до 200 символов")]
        [Display(Name = "Заголовок вопроса")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Описание вопроса")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Теги")]
        public string? Tags { get; set; } // Пример: "c# blazor asp.net"

        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Дата последнего обновления")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Количество просмотров")]
        public int ViewCount { get; set; } = 0;

        [Display(Name = "Количество ответов")]
        public int AnswerCount { get; set; } = 0;

        [Display(Name = "Решен ли вопрос")]
        public bool IsSolved { get; set; } = false;

        // Внешний ключ для пользователя
        [ForeignKey("User")]
        public string? UserId { get; set; }

        // Навигационное свойство
        public virtual ApplicationUser? User { get; set; }

        // Навигационное свойство для ответов
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}