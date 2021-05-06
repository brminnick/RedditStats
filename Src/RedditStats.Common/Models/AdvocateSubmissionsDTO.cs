using System;
using System.Linq;

namespace RedditStats.Common
{
    public class AdvocateSubmissions
    {
        public AdvocateSubmissions(RedditData redditData)
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

        public string Author { get; }
        public string Title { get; }
        public string Subreddit { get; }
        public DateTimeOffset CreatedAt { get; }
        public double UpVoteRatio { get; }
        public int UpVotes { get; }
        public int DownVotes { get; }
        public int CommentCount { get; }
        public bool IsAwarded { get; }
        public Uri RedditUri { get; }
    }
}
