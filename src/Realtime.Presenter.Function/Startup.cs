using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Realtime.Presenter.Function;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Realtime.Presenter.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            builder.Services.AddRealTimePresenter(config);
        }
    }

    // Use this if you can't upgrade 'Microsoft.NET.Sdk.Functions' to version 1.0.28
    // Or if you can't add 'Microsoft.Azure.Functions.Extensions' 1.0.0
//    public class WebJobsStartup : IWebJobsStartup
//    {
//        public void Configure(IWebJobsBuilder builder)
//        {
//            var config = new ConfigurationBuilder()
//                .AddEnvironmentVariables()
//                .Build();
//            builder.Services.AddRealTimePresenter(config);
//        }
//    }
}