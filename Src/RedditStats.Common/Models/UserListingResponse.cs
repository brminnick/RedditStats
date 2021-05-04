using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RedditStats.Common
{
    public class UserListingResponse
    {
        [JsonPropertyName("type")]
        public RedditType Type => Kind.ToRedditType();

        [JsonPropertyName("kind")]
        public string Kind { get; init; } = string.Empty;

        [JsonPropertyName("data")]
        public RedditData Data { get; init; } = new();
    }

    public class Image
    {
        [JsonPropertyName("source")]
        public Source Source { get; init; } = new();

        [JsonPropertyName("resolutions")]
        public IReadOnlyList<Source> Resolutions { get; init; } = Array.Empty<Source>();

        [JsonPropertyName("variants")]
        public Variants Variants { get; init; } = new();

        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;
    }

    public class Preview
    {
        [JsonPropertyName("images")]
        public IReadOnlyList<Image> Images { get; init; } = Array.Empty<Image>();

        [JsonPropertyName("enabled")]
        public bool Enabled { get; init; }
    }

    public class Source
    {
        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; init; }

        [JsonPropertyName("height")]
        public int Height { get; init; }
    }

    public class Award
    {
        [JsonPropertyName("giver_coin_reward")]
        public object? GiverCoinReward { get; init; }

        [JsonPropertyName("subreddit_id")]
        public object? SubredditId { get; init; }

        [JsonPropertyName("is_new")]
        public bool IsNew { get; init; }

        [JsonPropertyName("days_of_drip_extension")]
        public int DaysOfDripExtension { get; init; }

        [JsonPropertyName("coin_price")]
        public int CoinPrice { get; init; }

        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("penny_donate")]
        public object? PennyDonate { get; init; }

        [JsonPropertyName("award_sub_type")]
        public string AwardSubType { get; init; } = string.Empty;

        [JsonPropertyName("coin_reward")]
        public int CoinReward { get; init; }

        [JsonPropertyName("icon_url")]
        public string IconUrl { get; init; } = string.Empty;

        [JsonPropertyName("days_of_premium")]
        public int DaysOfPremium { get; init; }

        [JsonPropertyName("tiers_by_required_awardings")]
        public object? TiersByRequiredAwardings { get; init; }

        [JsonPropertyName("resized_icons")]
        public IReadOnlyList<Source> ResizedIcons { get; init; } = Array.Empty<Source>();

        [JsonPropertyName("icon_width")]
        public int IconWidth { get; init; }

        [JsonPropertyName("static_icon_width")]
        public int StaticIconWidth { get; init; }

        [JsonPropertyName("start_date")]
        public object? StartDate { get; init; }

        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; init; }

        [JsonPropertyName("awardings_required_to_grant_benefits")]
        public object? AwardingsRequiredToGrantBenefits { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; } = string.Empty;

        [JsonPropertyName("end_date")]
        public object? EndDate { get; init; }

        [JsonPropertyName("subreddit_coin_reward")]
        public int SubredditCoinReward { get; init; }

        [JsonPropertyName("count")]
        public int Count { get; init; }

        [JsonPropertyName("static_icon_height")]
        public int StaticIconHeight { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("resized_static_icons")]
        public IReadOnlyList<Source> ResizedStaticIcons { get; init; } = Array.Empty<Source>();

        [JsonPropertyName("icon_format")]
        public object? IconFormat { get; init; }

        [JsonPropertyName("icon_height")]
        public int IconHeight { get; init; }

        [JsonPropertyName("penny_price")]
        public object? PennyPrice { get; init; }

        [JsonPropertyName("award_type")]
        public string AwardType { get; init; } = string.Empty;

        [JsonPropertyName("static_icon_url")]
        public string StaticIconUrl { get; init; } = string.Empty;
    }

    public class RedditData
    {
        [JsonPropertyName("approved_at_utc")]
        public object? ApprovedAtUtc { get; init; }

        [JsonPropertyName("subreddit")]
        public string Subreddit { get; init; } = string.Empty;

        [JsonPropertyName("selftext")]
        public string Selftext { get; init; } = string.Empty;

        [JsonPropertyName("author_fullname")]
        public string AuthorFullname { get; init; } = string.Empty;

        [JsonPropertyName("saved")]
        public bool Saved { get; init; }

        [JsonPropertyName("mod_reason_title")]
        public object? ModReasonTitle { get; init; }

        [JsonPropertyName("gilded")]
        public int Gilded { get; init; }

        [JsonPropertyName("clicked")]
        public bool Clicked { get; init; }

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("link_flair_richtext")]
        public IReadOnlyList<object> LinkFlairRichtext { get; init; } = Array.Empty<object>();

        [JsonPropertyName("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; init; } = string.Empty;

        [JsonPropertyName("hidden")]
        public bool Hidden { get; init; }

        [JsonPropertyName("pwls")]
        public int? Pwls { get; init; }

        [JsonPropertyName("link_flair_css_class")]
        public object? LinkFlairCssClass { get; init; }

        [JsonPropertyName("downs")]
        public int Downs { get; init; }

        [JsonPropertyName("thumbnail_height")]
        public int? ThumbnailHeight { get; init; }

        [JsonPropertyName("top_awarded_type")]
        public object? TopAwardedType { get; init; }

        [JsonPropertyName("hide_score")]
        public bool HideScore { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("quarantine")]
        public bool Quarantine { get; init; }

        [JsonPropertyName("link_flair_text_color")]
        public string LinkFlairTextColor { get; init; } = string.Empty;

        [JsonPropertyName("upvote_ratio")]
        public double UpvoteRatio { get; init; }

        [JsonPropertyName("author_flair_background_color")]
        public object? AuthorFlairBackgroundColor { get; init; }

        [JsonPropertyName("subreddit_type")]
        public string SubredditType { get; init; } = string.Empty;

        [JsonPropertyName("ups")]
        public int Ups { get; init; }

        [JsonPropertyName("total_awards_received")]
        public int TotalAwardsReceived { get; init; }

        [JsonPropertyName("media_embed")]
        public MediaEmbed MediaEmbed { get; init; } = new();

        [JsonPropertyName("thumbnail_width")]
        public int? ThumbnailWidth { get; init; }

        [JsonPropertyName("author_flair_template_id")]
        public object? AuthorFlairTemplateId { get; init; }

        [JsonPropertyName("is_original_content")]
        public bool IsOriginalContent { get; init; }

        [JsonPropertyName("user_reports")]
        public IReadOnlyList<object> UserReports { get; init; } = Array.Empty<object>();

        [JsonPropertyName("secure_media")]
        public object? SecureMedia { get; init; }

        [JsonPropertyName("is_reddit_media_domain")]
        public bool IsRedditMediaDomain { get; init; }

        [JsonPropertyName("is_meta")]
        public bool IsMeta { get; init; }

        [JsonPropertyName("category")]
        public object? Category { get; init; }

        [JsonPropertyName("secure_media_embed")]
        public SecureMediaEmbed SecureMediaEmbed { get; init; } = new();

        [JsonPropertyName("link_flair_text")]
        public object? LinkFlairText { get; init; }

        [JsonPropertyName("can_mod_post")]
        public bool CanModPost { get; init; }

        [JsonPropertyName("score")]
        public int Score { get; init; }

        [JsonPropertyName("approved_by")]
        public object? ApprovedBy { get; init; }

        [JsonPropertyName("author_premium")]
        public bool AuthorPremium { get; init; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; init; } = string.Empty;

        [JsonPropertyName("edited")]
        public object? EditedAt_UnixOffset { get; init; }

        [JsonPropertyName("author_flair_css_class")]
        public object? AuthorFlairCssClass { get; init; }

        [JsonPropertyName("author_flair_richtext")]
        public IReadOnlyList<object> AuthorFlairRichtext { get; init; } = Array.Empty<object>();

        [JsonPropertyName("gildings")]
        public Gildings Gildings { get; init; } = new();

        [JsonPropertyName("post_hint")]
        public string PostHint { get; init; } = string.Empty;

        [JsonPropertyName("content_categories")]
        public object? ContentCategories { get; init; }

        [JsonPropertyName("is_self")]
        public bool IsSelf { get; init; }

        [JsonPropertyName("mod_note")]
        public object? ModNote { get; init; }

        [JsonPropertyName("crosspost_parent_list")]
        public IReadOnlyList<RedditData> CrosspostParentList { get; init; } = Array.Empty<RedditData>();

        [JsonPropertyName("created")]
        public double Created { get; init; }

        [JsonPropertyName("link_flair_type")]
        public string LinkFlairType { get; init; } = string.Empty;

        [JsonPropertyName("wls")]
        public int? Wls { get; init; }

        [JsonPropertyName("removed_by_category")]
        public string RemovedByCategory { get; init; } = string.Empty;

        [JsonPropertyName("banned_by")]
        public object? BannedBy { get; init; }

        [JsonPropertyName("author_flair_type")]
        public string AuthorFlairType { get; init; } = string.Empty;

        [JsonPropertyName("domain")]
        public string Domain { get; init; } = string.Empty;

        [JsonPropertyName("allow_live_comments")]
        public bool AllowLiveComments { get; init; }

        [JsonPropertyName("selftext_html")]
        public string SelftextHtml { get; init; } = string.Empty;

        [JsonPropertyName("likes")]
        public object? Likes { get; init; }

        [JsonPropertyName("suggested_sort")]
        public object? SuggestedSort { get; init; }

        [JsonPropertyName("banned_at_utc")]
        public object? BannedAtUtc { get; init; }

        [JsonPropertyName("url_overridden_by_dest")]
        public string UrlOverriddenByDest { get; init; } = string.Empty;

        [JsonPropertyName("view_count")]
        public object? ViewCount { get; init; }

        [JsonPropertyName("archived")]
        public bool Archived { get; init; }

        [JsonPropertyName("no_follow")]
        public bool NoFollow { get; init; }

        [JsonPropertyName("is_crosspostable")]
        public bool IsCrosspostable { get; init; }

        [JsonPropertyName("pinned")]
        public bool Pinned { get; init; }

        [JsonPropertyName("over_18")]
        public bool Over18 { get; init; }

        [JsonPropertyName("preview")]
        public Preview Preview { get; init; } = new();

        [JsonPropertyName("all_awardings")]
        public IReadOnlyList<Award> AllAwardings { get; init; } = Array.Empty<Award>();

        [JsonPropertyName("awarders")]
        public IReadOnlyList<object> Awarders { get; init; } = Array.Empty<object>();

        [JsonPropertyName("media_only")]
        public bool MediaOnly { get; init; }

        [JsonPropertyName("can_gild")]
        public bool CanGild { get; init; }

        [JsonPropertyName("spoiler")]
        public bool Spoiler { get; init; }

        [JsonPropertyName("locked")]
        public bool Locked { get; init; }

        [JsonPropertyName("author_flair_text")]
        public object? AuthorFlairText { get; init; }

        [JsonPropertyName("treatment_tags")]
        public IReadOnlyList<object> TreatmentTags { get; init; } = Array.Empty<object>();

        [JsonPropertyName("visited")]
        public bool Visited { get; init; }

        [JsonPropertyName("removed_by")]
        public object? RemovedBy { get; init; }

        [JsonPropertyName("num_reports")]
        public object? NumReports { get; init; }

        [JsonPropertyName("distinguished")]
        public object? Distinguished { get; init; }

        [JsonPropertyName("subreddit_id")]
        public string SubredditId { get; init; } = string.Empty;

        [JsonPropertyName("mod_reason_by")]
        public object? ModReasonBy { get; init; }

        [JsonPropertyName("removal_reason")]
        public object? RemovalReason { get; init; }

        [JsonPropertyName("link_flair_background_color")]
        public string LinkFlairBackgroundColor { get; init; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("is_robot_indexable")]
        public bool IsRobotIndexable { get; init; }

        [JsonPropertyName("report_reasons")]
        public object? ReportReasons { get; init; }

        [JsonPropertyName("author")]
        public string Author { get; init; } = string.Empty;

        [JsonPropertyName("discussion_type")]
        public object? DiscussionType { get; init; }

        [JsonPropertyName("num_comments")]
        public int NumComments { get; init; }

        [JsonPropertyName("send_replies")]
        public bool SendReplies { get; init; }

        [JsonPropertyName("whitelist_status")]
        public string WhitelistStatus { get; init; } = string.Empty;

        [JsonPropertyName("contest_mode")]
        public bool ContestMode { get; init; }

        [JsonPropertyName("mod_reports")]
        public IReadOnlyList<object> ModReports { get; init; } = Array.Empty<object>();

        [JsonPropertyName("author_patreon_flair")]
        public bool AuthorPatreonFlair { get; init; }

        [JsonPropertyName("crosspost_parent")]
        public string CrosspostParent { get; init; } = string.Empty;

        [JsonPropertyName("author_flair_text_color")]
        public object? AuthorFlairTextColor { get; init; }

        [JsonPropertyName("permalink")]
        public string Permalink { get; init; } = string.Empty;

        [JsonPropertyName("parent_whitelist_status")]
        public string ParentWhitelistStatus { get; init; } = string.Empty;

        [JsonPropertyName("stickied")]
        public bool Stickied { get; init; }

        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;

        [JsonPropertyName("subreddit_subscribers")]
        public int SubredditSubscribers { get; init; }

        [JsonPropertyName("created_utc")]
        public double CreatedUtc { get; init; }

        [JsonPropertyName("num_crossposts")]
        public int NumCrossposts { get; init; }

        [JsonPropertyName("media")]
        public object? Media { get; init; }

        [JsonPropertyName("is_video")]
        public bool IsVideo { get; init; }

        [JsonPropertyName("comment_type")]
        public object? CommentType { get; init; }

        [JsonPropertyName("link_id")]
        public string LinkId { get; init; } = string.Empty;

        [JsonPropertyName("replies")]
        public string Replies { get; init; } = string.Empty;

        [JsonPropertyName("parent_id")]
        public string ParentId { get; init; } = string.Empty;

        [JsonPropertyName("body")]
        public string Body { get; init; } = string.Empty;

        [JsonPropertyName("link_title")]
        public string LinkTitle { get; init; } = string.Empty;

        [JsonPropertyName("is_submitter")]
        public bool? IsSubmitter { get; init; }

        [JsonPropertyName("body_html")]
        public string BodyHtml { get; init; } = string.Empty;

        [JsonPropertyName("collapsed_reason")]
        public string? CollapsedReason { get; init; }

        [JsonPropertyName("associated_award")]
        public object? AssociatedAward { get; init; }

        [JsonPropertyName("score_hidden")]
        public bool? ScoreHidden { get; init; }

        [JsonPropertyName("link_permalink")]
        public string LinkPermalink { get; init; } = string.Empty;

        [JsonPropertyName("link_author")]
        public string LinkAuthor { get; init; } = string.Empty;

        [JsonPropertyName("link_url")]
        public string LinkUrl { get; init; } = string.Empty;

        [JsonPropertyName("collapsed")]
        public bool? Collapsed { get; init; }

        [JsonPropertyName("controversiality")]
        public int? Controversiality { get; init; }

        [JsonPropertyName("collapsed_because_crowd_control")]
        public bool? CollapsedBecauseCrowdControl { get; init; }

        [JsonPropertyName("modhash")]
        public string Modhash { get; init; } = string.Empty;

        [JsonPropertyName("dist")]
        public int Dist { get; init; }

        [JsonPropertyName("children")]
        public IReadOnlyList<UserListingResponse> Children { get; init; } = Array.Empty<UserListingResponse>();

        [JsonPropertyName("after")]
        public string After { get; init; } = string.Empty;

        [JsonPropertyName("before")]
        public string Before { get; init; } = string.Empty;
    }

    public class MediaEmbed
    {

    }

    public class SecureMediaEmbed
    {

    }

    public class Gildings
    {

    }

    public class Variants
    {

    }

    public enum RedditType { Unknown = 0, Comment = 1, Account = 2, Link = 3, Message = 4, Subreddit = 5, Award = 6 };

    public static class RedditTypeExtensions
    {
        public static RedditType ToRedditType(this string kind)
        {
            try
            {
                var typeNumber = int.Parse($"{kind[1]}");
                return (RedditType)typeNumber;
            }
            catch
            {
                return RedditType.Unknown;
            }
        }
    }
}

// https://stackoverflow.com/a/66136438/5953643
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public record IsExternalInit;
}