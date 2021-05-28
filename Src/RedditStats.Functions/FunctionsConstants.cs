using System;
namespace RedditStats.Functions
{
    static class FunctionsConstants
    {
        public const bool ShouldRunOnStartup =
#if DEBUG
            true;
#else
            false;
#endif
        public const string GET = "get";
        public const string RunOncePerDayCron = "0 0 0 * * *";
    }
}
