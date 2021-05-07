using Microsoft.EntityFrameworkCore;
using RedditStats.Common;

namespace RedditStats.Functions
{
    class AdvocateStatisticsDbContext : DbContext
    {
        public AdvocateStatisticsDbContext(DbContextOptions<AdvocateStatisticsDbContext> options) : base(options)
        {
        }

        public DbSet<AdvocateSubmissions> AdvocateSubmissions => Set<AdvocateSubmissions>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdvocateSubmissions>().HasKey(x => x.RedditUri);
        }
    }
}
