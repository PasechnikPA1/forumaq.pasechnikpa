using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ForumAQ.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для вопросов и ответов
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ThanksHistory> ThanksHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Создание ролей
            var roles = new[]
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Настройка связей между пользователями и вопросами
            builder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Настройка связей между пользователями и ответами
            builder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Настройка связей между вопросами и ответами
            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Настройка связей для истории благодарностей
            builder.Entity<ThanksHistory>()
                .HasOne(th => th.Answer)
                .WithMany() // У ответа может быть много благодарностей
                .HasForeignKey(th => th.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ThanksHistory>()
                .HasOne(th => th.User)
                .WithMany() // У пользователя может быть много отправленных благодарностей
                .HasForeignKey(th => th.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Уникальный индекс чтобы пользователь мог поблагодарить только один раз за ответ
            builder.Entity<ThanksHistory>()
                .HasIndex(th => new { th.AnswerId, th.UserId })
                .IsUnique();

            // Добавим тестовые вопросы (БЕЗ UserId - будет установлено позже)
            builder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    Title = "Как создать проект на Blazor?",
                    Description = "Подскажите, с чего начать создание проекта на Blazor Server? Какие шаги нужно выполнить?",
                    Tags = "blazor c# web",
                    CreatedDate = DateTime.Now.AddDays(-2),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                    ViewCount = 15,
                    AnswerCount = 3
                },
                new Question
                {
                    Id = 2,
                    Title = "Проблема с Entity Framework Core",
                    Description = "Не могу выполнить миграцию, выдает ошибку подключения к базе данных. В чем может быть проблема?",
                    Tags = "entity-framework sql database",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    UpdatedDate = DateTime.Now.AddDays(-1),
                    ViewCount = 8,
                    AnswerCount = 1
                }
            );

            // Не добавляем тестовые ответы здесь, они будут создаваться динамически
        }
    }
}