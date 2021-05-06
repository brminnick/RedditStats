using Microsoft.EntityFrameworkCore;

namespace RedditStats.Functions
{
    class AdvocateStatisticsDbContext : DbContext
    {
        public AdvocateStatisticsDbContext(DbContextOptions<AdvocateStatisticsDbContext> options) : base(options)
        {
        }

        public DbSet<GetAdvocateSubmissions> AdvocateSubmissions => Set<GetAdvocateSubmissions>();
    }
}
