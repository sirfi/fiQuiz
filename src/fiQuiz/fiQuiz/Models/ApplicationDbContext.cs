using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace fiQuiz.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }



        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
            foreach (EntityEntry entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    DateTime now = DateTime.UtcNow;
                    string user = GetCurrentUser();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.Properties.First(x => x.Metadata.Name == "CreatedAt").IsModified = false;
                            entry.Properties.First(x => x.Metadata.Name == "CreatedBy").IsModified = false;
                            trackable.LastUpdatedAt = now;
                            if (string.IsNullOrEmpty(trackable.LastUpdatedBy))
                                trackable.LastUpdatedBy = user;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            if (string.IsNullOrEmpty(trackable.CreatedBy))
                                trackable.CreatedBy = user;
                            trackable.LastUpdatedAt = now;
                            if (string.IsNullOrEmpty(trackable.LastUpdatedBy))
                                trackable.LastUpdatedBy = user;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            return _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "system";
        }
        #endregion

        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<QuestionReport> QuestionReports { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }
        public DbSet<QuizUsedJoker> QuizUsedJokers { get; set; }
        [DbFunction("RAND")]
        public static double Rand()
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        public IQueryable<Question> SelectQuestions(int questionCount)
        {
            return Questions.FromSql("CALL selectquestions(@QuestionCount)",
                new MySqlParameter("QuestionCount", MySqlDbType.Int32) { Value = questionCount });
        }
        public async Task<Question> SelectQuestionAsync(int questionCount, int questionNumber, int excludeQuestionId)
        {
            return await Questions.FromSql("CALL selectquestionsbase(@QuestionCount,@QuestionNumber,@ExcludeQuestionId)",
                new MySqlParameter("QuestionCount", MySqlDbType.Int32) { Value = questionCount },
                new MySqlParameter("QuestionNumber", MySqlDbType.Int32) { Value = questionNumber },
                new MySqlParameter("ExcludeQuestionId", MySqlDbType.Int32) { Value = excludeQuestionId }).FirstOrDefaultAsync();
        }
    }
}
