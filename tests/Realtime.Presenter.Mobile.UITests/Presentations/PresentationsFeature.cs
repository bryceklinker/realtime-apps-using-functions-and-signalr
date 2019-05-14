using FluentAssertions;
using NUnit.Framework;
using Realtime.Presenter.Mobile.UITests.Common;
using Xamarin.UITest;

namespace Realtime.Presenter.Mobile.UITests.Presentations
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class PresentationsFeature
    {
        private readonly Platform _platform;
        private PresentationsPage _page;

        public PresentationsFeature(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void Setup()
        {
            _page = new PresentationsPage(AppInitializer.Start(_platform));
        }

        [Test]
        public void GivenApplicationWhenStartedThenIShouldSeeNextButton()
        {
            _page.HasNextButton().Should().BeTrue();
        }

        [Test]
        public void GivenApplicationWhenStartedThenIShouldSeePreviousButton()
        {
            _page.HasPreviousButton().Should().BeTrue();
        }
    }
}