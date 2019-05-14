using System.Net.Http;
using Realtime.Presenter.Mobile.Common.Configuration;
using Realtime.Presenter.Mobile.Common.ErrorHandling;

namespace Realtime.Presenter.Mobile.Presentations.Services
{
    public interface IPresentationService
    {
        void GoToNextSlide();
        void GoToPreviousSlide();
    }
    
    public class PresentationsService : IPresentationService
    {
        private readonly IConfig _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IErrorHandler _errorHandler;

        private string Endpoint => _config.Endpoint;
        
        public PresentationsService(IConfig config, IHttpClientFactory httpClientFactory, IErrorHandler errorHandler)
        {
            _httpClientFactory = httpClientFactory;
            _errorHandler = errorHandler;
            _config = config;
        }

        public void GoToNextSlide()
        {
            var client = _httpClientFactory.CreateClient();
            client.PostAsync($"{Endpoint}/api/nextSlide", new StringContent(""))
                .AsFireAndForget(_errorHandler);
        }

        public void GoToPreviousSlide()
        {
            var client = _httpClientFactory.CreateClient();
            client.PostAsync($"{Endpoint}/api/previousSlide", new StringContent(""))
                .AsFireAndForget(_errorHandler);
        }
    }
}