using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RedditStats.Functions;

static class DbInitializer
{
	public static Task Initialize(AdvocateStatisticsDbContext advocateStatistisDbContext) => advocateStatistisDbContext.Database.MigrateAsync();
}