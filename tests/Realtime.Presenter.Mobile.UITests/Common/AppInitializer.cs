using System;
using System.IO;
using Xamarin.UITest;

namespace Realtime.Presenter.Mobile.UITests.Common
{
    public static class AppInitializer
    {
        private static readonly string SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "src");
        private static readonly string ApkPath = Path.Combine(SourcePath, "Realtime.Presenter.Mobile.Android", "bin", "Release", "com.klinker.realtime_presenter_mobile-Signed.apk");
        private static readonly string AppBundlePath = Path.Combine(SourcePath, "Realtime.Presenter.Mobile.iOS", "bin", "iPhoneSimulator", "Release", "Realtime.Presenter.Mobile.iOS.app");
        
        public static IApp Start(Platform platform)
        {
            switch (platform)
            {
                case Platform.Android:
                    return StartAndroid();
                case Platform.iOS:
                    return StartIOS();
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
            }
        }

        private static IApp StartAndroid()
        {
            return ConfigureApp
                .Android
                .PreferIdeSettings()
                .Debug()
                .ApkFile(ApkPath)
                .StartApp();
        }

        private static IApp StartIOS()
        {
            return ConfigureApp
                .iOS
                .Debug()
                .PreferIdeSettings()
                .AppBundle(AppBundlePath)
                .StartApp();
        }
    }
}