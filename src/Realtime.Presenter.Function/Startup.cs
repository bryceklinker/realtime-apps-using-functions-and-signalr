using Microsoft.Azure.Functions.Extensions.DependencyInjection;
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
}