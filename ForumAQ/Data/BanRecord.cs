using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ForumAQ.Data
{
    [Index(nameof(UserId))]
    [Index(nameof(ModeratorId))]
    [Index(nameof(BannedUntil))]
    [Index(nameof(BannedAt))]
    public class BanRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        [Required]
        [ForeignKey("Moderator")]
        public string ModeratorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Moderator { get; set; }

        [Required]
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;

        [Required]
        public DateTime BannedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime BannedUntil { get; set; }

        [Required]
        public BanType BanType { get; set; }

        [NotMapped]
        public bool IsActive => DateTime.Now < BannedUntil;
    }

    public enum BanType
    {
        QuestionViolation = 0,   // Нарушение в вопросе
        AnswerViolation = 1,     // Нарушение в ответе
        Other = 2                // Другое нарушение
    }
}