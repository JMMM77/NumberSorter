using System.Net.Http.Json;
using NumberSorter.Shared.Models;
using NumberSorter.WebApis.Apis;
using NumberSorter.WebApis.Tests.IntegrationTests.TestHelpers;

namespace NumberSorter.WebApis.Tests.IntegrationTests.Apis;

public class SortedNumbersApisTests(AspireAppFixture aspireAppFixture) : IClassFixture<AspireAppFixture>
{
    private readonly HttpClient _httpClient = aspireAppFixture.GetHttpClient();

    [Fact]
    public async Task Post_ReturnsCreatedModel()
    {
        // Arrange
        var sortedNumbersViewModel = new SortedNumbersViewModel()
        {
            SortedValues = [1, 2, 3],
            InitialValues = "3,2,1",
            IsAscending = true
        };

        var result = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", sortedNumbersViewModel);

        Assert.NotNull(result);

        var content = await result.Content.ReadAsStringAsync();

        Assert.True(result.IsSuccessStatusCode,
            $"Status: {(int)result.StatusCode}, Body: {content}");

        var resultSortedNumbersViewModel = await result.Content.ReadFromJsonAsync<SortedNumbersViewModel>();

        Assert.NotNull(resultSortedNumbersViewModel);
        Assert.Equal(sortedNumbersViewModel.SortedValues, resultSortedNumbersViewModel.SortedValues);
        Assert.Equal(sortedNumbersViewModel.InitialValues, resultSortedNumbersViewModel.InitialValues);
        Assert.Equal(sortedNumbersViewModel.IsAscending, resultSortedNumbersViewModel.IsAscending);
    }
}
