using Aspire.Hosting;
using Aspire.Hosting.Testing;
using NumberSorter.Shared.Constants;

namespace NumberSorter.WebApis.Tests.IntegrationTests.TestHelpers;

public class AspireAppFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    public async Task InitializeAsync()
    {
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