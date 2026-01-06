using Microsoft.Extensions.Options;
using NumberSorter.Data.Extensions;
using NumberSorter.Services.Extensions;
using NumberSorter.Shared;
using NumberSorter.WebApis.Options;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder ConfigureBuilder()
        {
            builder.AddServiceDefaults();

            builder.Services.AddOpenApi();

            builder
                .ConfigureOutputCaching()
                .AddDataDependencies()
                .AddServiceDependencies();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAngular", policy =>
                        policy.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
            }

            return builder;
        }

        private WebApplicationBuilder ConfigureOutputCaching()
        {
            builder.Services.Configure<OutputCachingOptions>(
                builder.Configuration.GetSection(OutputCachingOptions.OutputCachingSettings));

            var options = builder.Services.BuildServiceProvider()
                .GetRequiredService<IOptions<OutputCachingOptions>>().Value;

            if (options.Enabled)
            {
                builder.Services.AddOutputCache(options =>
                {
                    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(10)));
                });
            }

            return builder;
        }
    }
}
