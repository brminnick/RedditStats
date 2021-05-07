using System;
using System.Linq;

namespace RedditStats.Common
{
    public class AdvocateSubmissions
    {
        public AdvocateSubmissions(RedditData redditData) : this()
        {
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds((long)redditData.CreatedUtc);
            UpVoteRatio = redditData.UpvoteRatio;
            UpVotes = redditData.Ups;
            DownVotes = redditData.Downs;
            Subreddit = redditData.Subreddit;
            IsAwarded = redditData.AllAwardings.Any();
            RedditUri = new Uri("https://reddit.com" + redditData.Permalink);
            Author = redditData.Author;
            Title = redditData.Title;
            CommentCount = redditData.TotalComments;
        }

        public AdvocateSubmissions()
        {
        }

        public string Author { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Subreddit { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; }
        public double UpVoteRatio { get; init; }
        public int UpVotes { get; init; }
        public int DownVotes { get; init; }
        public int CommentCount { get; init; }
        public bool IsAwarded { get; init; }
        public Uri? RedditUri { get; init; }
    }
}
