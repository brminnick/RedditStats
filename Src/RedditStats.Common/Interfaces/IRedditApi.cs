using System.Threading.Tasks;
using Refit;

namespace RedditStats.Common
{
    public interface IRedditApi
    {
        [Get("/u/{username}.json")]
        Task<ApiResponse<UserListingResponse>> GetUserListing(string username, string? after = null);
    }
}
