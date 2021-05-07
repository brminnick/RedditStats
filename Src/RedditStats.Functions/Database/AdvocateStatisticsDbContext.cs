using Microsoft.EntityFrameworkCore;
using RedditStats.Common;

namespace RedditStats.Functions
{
    class AdvocateStatisticsDbContext : DbContext
    {
        public AdvocateStatisticsDbContext(DbContextOptions<AdvocateStatisticsDbContext> options) : base(options)
        {
        }

        public DbSet<RedditSubmission> Submissions => Set<RedditSubmission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RedditSubmission>().HasKey(x => x.RedditUri);

            modelBuilder.Entity<RedditSubmission>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
            modelBuilder.Entity<RedditSubmission>().Property(x => x.UpdatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");

            modelBuilder.Entity<RedditSubmission>().Property(x => x.UpdatedAt).ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);
        }
    }
}
