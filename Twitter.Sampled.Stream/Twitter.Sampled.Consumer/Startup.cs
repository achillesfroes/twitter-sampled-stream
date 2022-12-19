using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Twitter.Sampled.Application;
using Twitter.Sampled.Infrastructure.Data;

[assembly: FunctionsStartup(typeof(Twitter.Sampled.Functions.Startup))]
namespace Twitter.Sampled.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddScoped<ITweetService, TweetService>();
            builder.Services.AddScoped<ITweetReportService, TweetReportService>();

            builder.Services.AddScoped<ITweetRepository, TweetRepository>();
            builder.Services.AddScoped<ITweetReportRepository, TweetReportRepository>();
            builder.Services.AddScoped<IHashTagReportRepository, HashTagReportRepository>();

            builder.Services.AddDbContext<TweetContext>(options =>
            {
                options.UseInMemoryDatabase("TweetDb");
            });
        }
    }
}
