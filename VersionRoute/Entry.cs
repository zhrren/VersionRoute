using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mark.VersionRoute
{
    public class Entry
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Url { get; set; }
        public Version NativeVersion { get; set; }
        public Version ReleaseVersion { get; set; }

        public Entry(string name, string user, string group, string url, string nativeVer, string releaseVer)
        {
            Name = name;
            User = user;
            Group = group;
            Url = url;

            NativeVersion = ParseVersion(nativeVer);
            ReleaseVersion = ParseVersion(releaseVer);
        }

        public static Version ParseVersion(string version)
        {
            Regex reg = new Regex(@"([0-9\.]*)");
            var match = reg.Match(version);
            var ver = match.Groups[1].Value;
            return FormatVersion(new Version(ver));
        }

        public static Version FormatVersion(Version version)
        {
            return new Version(
                version.Major >= 0 ? version.Major : 0,
                version.Minor >= 0 ? version.Minor : 0,
                version.Build >= 0 ? version.Build : 0,
                version.Revision >= 0 ? version.Revision : 0);
        }
    }
}
