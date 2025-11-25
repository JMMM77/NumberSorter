using Aspire.Hosting;
using Aspire.Hosting.Testing;
using NumberSorter.Services.Options;
using NumberSorter.Shared.Constants;
using NumberSorter.WebApis.Options;

namespace NumberSorter.WebApis.Tests.IntegrationTests.TestHelpers;

public class AspireAppFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    public async Task InitializeAsync()
    {
        // Disable caching
        Environment.SetEnvironmentVariable($"{OutputCachingOptions.OutputCachingSettings}__{nameof(OutputCachingOptions.Enabled)}", "false", EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable($"{DistributedCachingOptions.DistributedCachingSettings}__{nameof(DistributedCachingOptions.Enabled)}", "false", EnvironmentVariableTarget.Process);

        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.NumberSorter_AppHost>();

        _app = builder.Build();

        await _app.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (_app != null)
        {
            await _app.DisposeAsync();
        }
    }

    public HttpClient GetHttpClient()
        => _app?.CreateHttpClient(AspireResourceNameConstants.WebApiProjectName)
            ?? throw new NullReferenceException();
}