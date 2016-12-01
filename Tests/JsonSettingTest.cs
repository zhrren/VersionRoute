using System;
using Xunit;
using Tests.Base;
using Mark.SettingsContext;
using Newtonsoft.Json;
using System.IO;
using Mark.VersionRouter;

namespace Tests
{

    public class JsonSettingTest
    {
        [Fact]
        public void KeepWeb_PublishApp()
        {
            var settingsContext = new SettingsContext("KeepWeb_PublishApp.json");
            var renew = settingsContext.Renew<Settings>();
            var vc = new Router(renew.Packages, null);
            var match = vc.Match("ios", "4.0.0", "11111111111");
            Assert.Equal("http://m.wfzkd.com/v/1.0.0/#!/auth_4.0.0?", match.Url);
            Console.WriteLine("abc");
        }
        
        [Fact]
        public void KeepApp_PublishWeb()
        {
            var settingsContext = new SettingsContext("KeepApp_PublishWeb.json");
            var renew = settingsContext.Renew<Settings>();
            var vc = new Router(renew.Packages, null);
            var match = vc.Match("ios", "4.0.0", "11111111111");
            Assert.Equal("http://m.wfzkd.com/v/1.1.0/#!/auth?", match.Url);
        }

        [Fact]
        public void PulishNew()
        {
            var settingsContext = new SettingsContext("PulishNew.json");
            var renew = settingsContext.Renew<Settings>();
            var vc = new Router(renew.Packages, null);
            var match = vc.Match("android", "4.0", "00000000000");
            Assert.Equal("http://m.wfzkd.com/v/1.0.0/#!/auth?4.0&", match.Url);

            match = vc.Match("ios", "4.0", "");
            Assert.Equal("http://m.wfzkd.com/v/1.0.0/#!/auth?4.0&", match.Url);


            match = vc.Match("ios", "2.8.6", "");
            Assert.Equal("http://www.wfzkd.com/auth?", match.Url);

            match = vc.Match("ios", "2.8.6", "18925179135");
            Assert.Equal("http://m.wfzkd.com/v/1.0.0/#!/auth?", match.Url);
        }


        [Fact]
        public void Bug1()
        {
            var settingsContext = new SettingsContext("Bug1.json");
            var renew = settingsContext.Renew<Settings>();
            var vc = new Router(renew.Packages, null);
            var match = vc.Match("android", "2.0.0", "");
            Assert.Equal("http://things.loocaa.com/apps/update/things_2.0.0.10061_d34ec788.wgt", match.Url);
        }
    }
}
