using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAQ.Data
{
    public class ThanksHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Answer")]
        public int AnswerId { get; set; }

        public virtual Answer? Answer { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        public virtual ApplicationUser? User { get; set; }

        [Display(Name = "Дата благодарности")]
        public DateTime ThankedDate { get; set; } = DateTime.Now;
    }
}