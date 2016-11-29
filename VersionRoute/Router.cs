using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mark.VersionRoute
{
    public class Router
    {
        private List<Release> _releaseList;
        private List<Group> _groupList;

        public Router(List<Release> releases, List<Group> groups)
        {
            _releaseList = releases;
            _groupList = groups;
        }

        public Entry Match(string nativeName, string nativeVersion, string uid)
        {
            List<Entry> entries = new List<Entry>();
            _releaseList.ForEach(x =>
            {
                x.Native.ForEach(n =>
                {
                    var entry = new Entry(n.Name, n.User, n.Group, n.Url, n.Version, x.Version);
                    entries.Add(entry);
                });
            });

            List<Entry> namedNative = entries.Where(x => VerifyName(x, nativeName)).ToList();

            var resultList = namedNative.Where(x =>
                Entry.ParseVersion(nativeVersion) >= x.NativeVersion
                && VerifyUser(x, uid, _groupList)).ToList();
            var resultItem = resultList.OrderByDescending(x => x.NativeVersion).FirstOrDefault();
            if (resultItem != null)
            {
                resultItem = resultList.Where(x => x.NativeVersion == resultItem.NativeVersion)
                    .OrderByDescending(x => x.ReleaseVersion)
                    .FirstOrDefault();
            }

            return resultItem == null ? namedNative.FirstOrDefault() : resultItem;
        }

        private bool VerifyName(Entry entry, string name)
        {
            if (string.IsNullOrWhiteSpace(entry.Name) || entry.Name == "*")
                return true;
            else
                return string.Equals(entry.Name, name, StringComparison.OrdinalIgnoreCase);
        }

        private bool VerifyUser(Entry entry, string user, List<Group> groupList)
        {
            if (user == null) user = "";
            if (!string.IsNullOrWhiteSpace(entry.User))
            {
                if (entry.User == "*") return true;
                if (entry.User.Split(',').Any(x => x.Equals(user, StringComparison.OrdinalIgnoreCase)))
                    return true;
            }

            if (!string.IsNullOrWhiteSpace(entry.Group) && groupList != null && groupList.Count > 0)
            {
                if (entry.Group == "*" && groupList.Any(x => x.User.Any(u => user.Equals(u, StringComparison.OrdinalIgnoreCase))))
                    return true;

                var group = groupList.FirstOrDefault(x => x.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
                if (group != null && group.User.Any(x => user.Equals(x, StringComparison.OrdinalIgnoreCase)))
                    return true;
            }

            return false;
        }

    }
}
