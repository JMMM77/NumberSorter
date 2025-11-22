using System.Net;
using System.Net.Http.Json;
using NumberSorter.Shared.Models;
using NumberSorter.WebApis.Apis;
using NumberSorter.WebApis.Tests.IntegrationTests.TestHelpers;

namespace NumberSorter.WebApis.Tests.IntegrationTests.Apis;

public class SortedNumbersApisTests(AspireAppFixture aspireAppFixture) : IClassFixture<AspireAppFixture>
{
    private readonly HttpClient _httpClient = aspireAppFixture.GetHttpClient();

    private static readonly SortedNumbersViewModel s_exampleSortedNumbersViewModel = new()
    {
        SortedValues = [1, 2, 3],
        InitialValues = "3,2,1",
        IsAscending = true
    };

    [Fact]
    public async Task Create_ReturnsCreatedItem()
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersViewModel);

        // Assert
        Assert.NotNull(response);

        var created = await response.Content.ReadFromJsonAsync<SortedNumbersViewModel>();

        AssertExpectedViewModel(created);
    }

    [Fact]
    public async Task GetById_ReturnsItem()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersViewModel);
        var created = await post.Content.ReadFromJsonAsync<SortedNumbersViewModel>();

        // Act
        var result = await _httpClient.GetFromJsonAsync<SortedNumbersViewModel>($"/{SortedNumbersApis.SortedNumbersApiPath}/{created!.Id}");

        // Assert
        AssertExpectedViewModel(result);
    }

    [Fact]
    public async Task Delete_RemovesItem()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersViewModel);
        var created = await post.Content.ReadFromJsonAsync<SortedNumbersViewModel>();
        var byIdUri = $"/{SortedNumbersApis.SortedNumbersApiPath}/{created!.Id}";

        // Act
        await _httpClient.DeleteAsync(byIdUri);

        // Assert
        var afterDelete = await _httpClient.GetAsync(byIdUri);

        Assert.Equal(HttpStatusCode.NotFound, afterDelete.StatusCode);
    }

    [Fact]
    public async Task CrudWorkflow_WorksEndToEnd()
    {
        // Get intial values
        var getAllInitialResult = _httpClient.GetFromJsonAsAsyncEnumerable<SortedNumbersViewModel>($"/{SortedNumbersApis.SortedNumbersApiPath}");
        var initialCount = await getAllInitialResult.CountAsync();

        Assert.NotNull(getAllInitialResult);

        // Test posting
        var postResult = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersViewModel);

        Assert.NotNull(postResult);

        var postResultViewModel = await postResult.Content.ReadFromJsonAsync<SortedNumbersViewModel>();

        AssertExpectedViewModel(postResultViewModel);

        // Confirm post worked
        var getByIdResultAfterPost = await _httpClient.GetFromJsonAsync<SortedNumbersViewModel>($"/{SortedNumbersApis.SortedNumbersApiPath}/{postResultViewModel.Id}");

        AssertExpectedViewModel(getByIdResultAfterPost);

        // Confirm Get all includes posted item
        var getAllResultAfterPost = _httpClient.GetFromJsonAsAsyncEnumerable<SortedNumbersViewModel>($"/{SortedNumbersApis.SortedNumbersApiPath}");

        Assert.NotNull(getAllResultAfterPost);
        Assert.Equal(initialCount + 1, await getAllResultAfterPost.CountAsync());

        var foundPostedItem = await getAllResultAfterPost.FirstOrDefaultAsync(x => x is not null && x.Id == postResultViewModel.Id);

        AssertExpectedViewModel(foundPostedItem);

        // Test deleting
        var deleteResult = await _httpClient.DeleteAsync($"/{SortedNumbersApis.SortedNumbersApiPath}/{postResultViewModel.Id}");
        var getByIdResultAfterDelete = await _httpClient.GetAsync($"/{SortedNumbersApis.SortedNumbersApiPath}/{postResultViewModel.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getByIdResultAfterDelete.StatusCode);
    }

    private static void AssertExpectedViewModel(SortedNumbersViewModel? sortedNumbersViewModel)
    {
        Assert.NotNull(sortedNumbersViewModel);
        Assert.Equal(s_exampleSortedNumbersViewModel.SortedValues, sortedNumbersViewModel.SortedValues);
        Assert.Equal(s_exampleSortedNumbersViewModel.InitialValues, sortedNumbersViewModel.InitialValues);
        Assert.Equal(s_exampleSortedNumbersViewModel.IsAscending, sortedNumbersViewModel.IsAscending);
    }
}
