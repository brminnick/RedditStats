using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace RedditStats.Common;

public class AdvocateService
{
	readonly IAdvocateApi _advocateApiClient;

	public AdvocateService(IAdvocateApi advocateApiClient) => _advocateApiClient = advocateApiClient;

	public Task<IReadOnlyList<AdvocateModel>> GetCurrentAdvocates(CancellationToken cancellationToken) => _advocateApiClient.GetCurrentAdvocates(cancellationToken);

	public async IAsyncEnumerable<string> GetRedditUsernames([EnumeratorCancellation] CancellationToken cancellationToken)
	{
		var advocates = await GetCurrentAdvocates(cancellationToken).ConfigureAwait(false);

		foreach (var advocate in advocates)
		{
			cancellationToken.ThrowIfCancellationRequested();

			if (!string.IsNullOrWhiteSpace(advocate.RedditUserName))
				yield return advocate.RedditUserName;
		}
	}
}
