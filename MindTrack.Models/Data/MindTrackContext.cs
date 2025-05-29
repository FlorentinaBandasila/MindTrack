using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.Data
{
    public class MindTrackContext : DbContext
    {
        public MindTrackContext(DbContextOptions<MindTrackContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(builder => builder.EnableRetryOnFailure());
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Emotion> Emotions { get; set; }
        public DbSet<MoodSelection> MoodSelections { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<QuizResults> QuizResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(t => t.User_id);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(q => q.Question_id);

            //modelBuilder.Entity<Task_category>()
            //    .HasOne(tc => tc.UserTask)
            //    .WithMany(t => t.Task_categories)
            //    .HasForeignKey(tc => tc.Category_id);

            modelBuilder.Entity<Emotion>()
                .HasOne(m => m.Mood_selection)
                .WithMany(e => e.Emotions)
                .HasForeignKey(e => e.Mood_id);

            modelBuilder.Entity<Emotion>()
                .HasOne(e => e.User)
                .WithMany(u => u.Emotions)
                .HasForeignKey(e => e.User_id);

            modelBuilder.Entity<QuizResults>()
                .HasOne(t => t.User)
                .WithMany(u => u.QuizResults)
                .HasForeignKey(t => t.User_id);

            modelBuilder.Entity<UserTask>()
                .HasOne(t => t.Task_categories)
                .WithMany()
                .HasForeignKey(t => t.Category_id);

        }
    }
}
