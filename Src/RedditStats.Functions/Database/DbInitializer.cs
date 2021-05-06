using System.Threading.Tasks;

namespace RedditStats.Functions
{
    static class DbInitializer
    {
        public static Task Initialize(AdvocateStatisticsDbContext advocateStatistisDbContext) => advocateStatistisDbContext.Database.EnsureCreatedAsync();
    }
}
