using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Realtime.Presenter.Mobile.UITests.Presentations
{
    public class PresentationsPage
    {
        private readonly IApp _app;

        private Func<AppQuery, AppQuery> NextButton => app => app.Marked("NextButton");
        private Func<AppQuery, AppQuery> PreviousButton => app => app.Marked("PreviousButton");
        
        public PresentationsPage(IApp app)
        {
            _app = app;
        }

        public bool HasNextButton()
        {
            return _app.Query(NextButton).Any();
        }

        public bool HasPreviousButton()
        {
            return _app.Query(PreviousButton).Any();
        }
    }
}