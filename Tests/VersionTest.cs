using Mark.VersionRoute;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Tests
{
    public class VersionTest
    {
        [Fact]
        public void MatchVersionTest()
        {
            var rules = new List<Package>()
            {
                CreatePublish("1.0",
                    CreateRule("android","1.0","*","url:android:1.0"),
                    CreateRule("ios","1.0","*","url:ios:1.0")),
                CreatePublish("2.0",
                    CreateRule("android","2.0","*","url:android:2.0"),
                    CreateRule("ios","2.0","*","url:ios:2.0")),
                CreatePublish("3.0",
                    CreateRule("android","3.0","*","url:android:3.0"),
                    CreateRule("ios","3.0","*","url:ios:3.0"))
            };
            var vc = CreateNavigator(rules, null);
            Assert.Equal("url:android:1.0", vc.Match("android", "1.0", "zhrren").Url);
            Assert.Equal("url:ios:1.0", vc.Match("ios", "1.0", "zhrren").Url);
            Assert.Equal("url:android:2.0", vc.Match("android", "2.0", "zhrren").Url);
            Assert.Equal("url:ios:2.0", vc.Match("ios", "2.0", "zhrren").Url);
            Assert.Equal("url:android:3.0", vc.Match("android", "3.0", "zhrren").Url);
            Assert.Equal("url:ios:3.0", vc.Match("ios", "3.0", "zhrren").Url);
        }

        /// <summary>
        /// 版本向下兼容
        /// </summary>
        [Fact]
        public void Test2()
        {
            var rules = new List<Package>()
                {
                    CreatePublish("1.0",
                        CreateRule("android","1.0","*","url:android:1.0"),
                        CreateRule("ios","1.0","*","url:ios:1.0")),
                    CreatePublish("2.0",
                        CreateRule("android","2.0","*","url:android:2.0"),
                        CreateRule("ios","2.0","*","url:ios:2.0")),
                    CreatePublish("3.0",
                        CreateRule("android","3.0","*","url:android:3.0"),
                        CreateRule("ios","3.0","*","url:ios:3.0"))
                };
            var vc = CreateNavigator(rules, null);
            Assert.Equal("url:android:2.0", vc.Match("android", "2.1", "zhrren").Url);
            Assert.Equal("url:ios:2.0", vc.Match("ios", "2.1", "zhrren").Url);
        }

        /// <summary>
        /// 指定用户
        /// </summary>
        [Fact]
        public void Test3()
        {
            var rules = new List<Package>()
                {
                    CreatePublish("1.0",
                        CreateRule("android","1.0","*","url:android:1.0"),
                        CreateRule("ios","1.0","*","url:ios:1.0")),
                    CreatePublish("2.0",
                        CreateRule("android","2.0","*","url:android:2.0"),
                        CreateRule("ios","2.0","*","url:ios:2.0")),
                    CreatePublish("3.0",
                        CreateRule("android","3.0","user,zhrren","url:android:3.0"),
                        CreateRule("ios","3.0","admin,zhrren","url:ios:3.0"))
                };
            var vc = CreateNavigator(rules, null);
            Assert.Equal("url:android:2.0", vc.Match("android", "2.1", "abc").Url);
            Assert.Equal("url:ios:2.0", vc.Match("ios", "2.1.1", "abc").Url);
            Assert.Equal("url:android:3.0", vc.Match("android", "3.0", "zhrren").Url);
            Assert.Equal("url:ios:3.0", vc.Match("ios", "3.0", "admin").Url);
        }

        /// <summary>
        /// 指定用户组
        /// </summary>
        [Fact]
        public void Test4()
        {
            var groups = new List<Group>()
                {
                    new Group("develop", new string[] {"zhrren","mark"}),
                    new Group("external", new string[] {"jm","boss","abc"}),
                };

            var rules = new List<Package>()
                {
                    CreatePublish("1.0",
                        CreateRule("android","1.0",null,"url:android:1.0"),
                        CreateRule("ios","1.0",null,"url:ios:1.0")),
                    CreatePublish("2.0",
                        CreateRule("android","2.0",null,"url:android:2.0", "*"),
                        CreateRule("ios","2.0",null,"url:ios:2.0","develop")),
                    CreatePublish("3.0",
                        CreateRule("android","3.0",null,"url:android:3.0","develop"),
                        CreateRule("ios","3.0",null,"url:ios:3.0"))
                };
            var vc = CreateNavigator(rules, groups);
            Assert.Equal("url:android:2.0", vc.Match("android", "2.1", "abc").Url);
            Assert.NotEqual("url:ios:2.0", vc.Match("ios", "2.1.1", "abc").Url);
            Assert.Equal("url:android:3.0", vc.Match("android", "3.0", "zhrren").Url);
        }

        /// <summary>
        /// 所有平台
        /// </summary>
        [Fact]
        public void Test5()
        {
            var rules = new List<Package>()
                {
                    CreatePublish("1.0",
                        CreateRule("*","1.0","*","url:1.0"), null),
                    CreatePublish("2.0",
                        CreateRule("*","2.0","*","url:2.0"), null),
                    CreatePublish("3.0",
                        CreateRule("*","3.0","*","url:3.0"), null)
                };
            var vc = CreateNavigator(rules, null);
            Assert.Equal("url:1.0", vc.Match("android", "1.0", "zhrren").Url);
            Assert.Equal("url:1.0", vc.Match("ios", "1.0", "zhrren").Url);
            Assert.Equal("url:2.0", vc.Match("android", "2.0", "zhrren").Url);
            Assert.Equal("url:2.0", vc.Match("ios", "2.0", "zhrren").Url);
            Assert.Equal("url:3.0", vc.Match("android", "3.0", "zhrren").Url);
            Assert.Equal("url:3.0", vc.Match("ios", "3.0", "zhrren").Url);
        }

        /// <summary>
        /// 编译版本号不正确
        /// </summary>
        [Fact]
        public void Test6()
        {
            var rules = new List<Package>()
                {
                    CreatePublish("1.0.0.1",
                        CreateRule("*","2.0","*","url:1.0"), null),
                    CreatePublish("1.0.0.2",
                        CreateRule("*","2.0","*","url:2.0"), null),
                };
            var vc = CreateNavigator(rules, null);
            Assert.Equal("url:2.0", vc.Match("android", "2.0", "zhrren").Url);
        }

        #region 工具方法

        private Router CreateNavigator(List<Package> packages, List<Group> groups)
        {
            var vc = new Router(packages, groups);
            return vc;
        }
        private Package CreatePublish(string version, Client androidClient, Client iosClient)
        {
            var package = new Package()
            {
                Version = version,
                Client = new List<Client>()
            };

            if (androidClient != null) package.Client.Add(androidClient);
            if (iosClient != null) package.Client.Add(iosClient);

            return package;
        }
        private Client CreateRule(string app, string version, string user, string url, string group = null)
        {
            return new Client()
            {
                Name = app,
                Version = version,
                User = user,
                Group = group,
                Url = url
            };
        }

        #endregion

    }

}
