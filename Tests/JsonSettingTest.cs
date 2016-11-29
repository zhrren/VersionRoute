using System;
using Mark.Settings;

namespace Tests
{
    
    //public class JsonSettingTest
    //{
    //    [TestMethod]
    //    public void WEB版本保持不变_APP发布版本()
    //    {
    //        SettingsManager<Settings> settings = new SettingsManager<Settings>();
    //        var renew = settings.Renew("WEB版本保持不变_APP发布版本.json");
    //        var vc = new Mark.Navigator.Navigator(renew.Release, null);
    //        var match = vc.Match("ios", "4.0.0", "11111111111");
    //        Assert.AreEqual("http://m.wfzkd.com/v/1.0.0/#!/auth_4.0.0?", match.Url);
    //    }

    //    [TestMethod]
    //    public void APP版本保持不变_WEB发布版本()
    //    {
    //        SettingsManager<Settings> settings = new SettingsManager<Settings>();
    //        var renew = settings.Renew("APP版本保持不变_WEB发布版本.json");
    //        var vc = new Mark.Navigator.Navigator(renew.Release, null);
    //        var match = vc.Match("ios", "4.0.0", "11111111111");
    //        Assert.AreEqual("http://m.wfzkd.com/v/1.1.0/#!/auth?", match.Url);
    //    }

    //    [TestMethod]
    //    public void 发布新版本()
    //    {
    //        SettingsManager<Settings> settings = new SettingsManager<Settings>();
    //        var renew = settings.Renew("发布新版本.json");
    //        var vc = new Mark.Navigator.Navigator(renew.Release, null);
    //        var match = vc.Match("android", "4.0", "00000000000");
    //        Assert.AreEqual("http://m.wfzkd.com/v/1.0.0/#!/auth?4.0&", match.Url);

    //        match = vc.Match("ios", "4.0", "");
    //        Assert.AreEqual("http://m.wfzkd.com/v/1.0.0/#!/auth?4.0&", match.Url);


    //        match = vc.Match("ios", "2.8.6", "");
    //        Assert.AreEqual("http://www.wfzkd.com/auth?", match.Url);

    //        match = vc.Match("ios", "2.8.6", "18925179135");
    //        Assert.AreEqual("http://m.wfzkd.com/v/1.0.0/#!/auth?", match.Url);
    //    }


    //    [TestMethod]
    //    public void 多个版本_不能取得最新的10061()
    //    {
    //        SettingsManager<Settings> settings = new SettingsManager<Settings>();
    //        var renew = settings.Renew("多个版本_不能取得最新的10061.json");
    //        var vc = new Mark.Navigator.Navigator(renew.Release, null);
    //        var match = vc.Match("android", "2.0.0", "");
    //        Assert.AreEqual("http://things.loocaa.com/apps/update/things_2.0.0.10061_d34ec788.wgt", match.Url);
    //    }
    //}
}
