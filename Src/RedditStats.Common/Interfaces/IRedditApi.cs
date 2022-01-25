using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace RedditStats.Common;

[Headers("User-Agent: " + nameof(RedditStats), "Accept-Encoding: gzip", "Accept: application/json")]
public interface IRedditApi
{
	[Get("/user/{username}/submitted")]
	Task<ApiResponse<UserListingResponse>> GetSubmissions(string username, CancellationToken cancellationToken, string? after = null);
}