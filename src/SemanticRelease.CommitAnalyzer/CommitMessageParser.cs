using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SemanticRelease.Extensibility;
using SemanticRelease.Extensibility.Model;

[assembly: InternalsVisibleTo("SemanticRelease.CommitAnalyzer.Tests")]
namespace SemanticRelease.CommitAnalyzer
{
    internal class CommitMessageParser
    {
        private readonly IEnumerable<ReleaseCommit> _commitsSinceRelease;

        public CommitMessageParser(IEnumerable<ReleaseCommit> commitsSinceRelease)
        {
            _commitsSinceRelease = commitsSinceRelease;
        }

        public ReleaseType GetReleaseType()
        {
            var releaseType = ReleaseType.NONE;

            var multiLineIgnoreCase = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant;

            var majorRelease = new Regex(@"(major|breaking|break)((\s+)|(:+|-+|\s+))(\(.*\):?)?", multiLineIgnoreCase);
            var minorRelease = new Regex(@"(feat|feature)((\s+)|(:+|-+|\s+))(\(.*\):?)?", multiLineIgnoreCase);
            var patchRelease = new Regex(@"(fix|perf|security)((\s+)|(:+|-+|\s+))(\(.*\):?)?", multiLineIgnoreCase);

            foreach (var commit in _commitsSinceRelease)
            {
                if (majorRelease.IsMatch(commit.Message))
                {
                    releaseType = ReleaseType.MAJOR;
                    break;
                }

                if (minorRelease.IsMatch(commit.Message) || releaseType == ReleaseType.MINOR)
                {
                    releaseType = ReleaseType.MINOR;
                    continue;
                }

                if (patchRelease.IsMatch(commit.Message))
                {
                    releaseType = ReleaseType.PATCH;
                }
            }

            return releaseType;
        }
    }
}