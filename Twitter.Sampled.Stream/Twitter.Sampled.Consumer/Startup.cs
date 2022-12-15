using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Twitter.Sampled.Application;
using Twitter.Sampled.Infrastructure.Data;

[assembly: FunctionsStartup(typeof(Twitter.Sampled.Consumer.Startup))]
namespace Twitter.Sampled.Consumer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddScoped<ITweetService, TweetService>();
            builder.Services.AddScoped<ITweetReportService, TweetReportService>();

            builder.Services.AddScoped<ITweetRepository, TweetRepository>();

            builder.Services.AddDbContext<TweetContext>(options => options.UseInMemoryDatabase("TweetDb"));
            //builder.Services.AddLogging();

        }
    }
}
