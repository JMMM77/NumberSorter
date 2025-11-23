using System.Net;
using System.Net.Http.Json;
using NumberSorter.Services.Dtos;
using NumberSorter.WebApis.Apis;
using NumberSorter.WebApis.Tests.IntegrationTests.TestHelpers;
using NumberSorter.WebUI.Dtos;

namespace NumberSorter.WebApis.Tests.IntegrationTests.Apis;

public class SortedNumbersApisTests(AspireAppFixture aspireAppFixture) : IClassFixture<AspireAppFixture>
{
    private readonly HttpClient _httpClient = aspireAppFixture.GetHttpClient();

    private const int ID = 1;
    private static readonly int[] s_initialValues = [3, 2, 1];
    private static readonly int[] s_sortedValues = [1, 2, 3];

    private static readonly SortedNumbersCreateDto s_exampleSortedNumbersCreateDto = new()
    {
        InitialValues = s_initialValues,
        IsAscending = true,
    };

    private static readonly SortedNumbersDetailsDto s_exampleSortedNumbersDetailsDto = new()
    {
        Id = 1,
        SortedValues = s_sortedValues,
        InitialValues = s_initialValues,
        IsAscending = true,
        SortTime = TimeSpan.Zero,
    };

    [Fact]
    public async Task Create_ReturnsCreatedItem()
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersCreateDto);

        // Assert
        Assert.NotNull(response);

        var resultDto = await response.Content.ReadFromJsonAsync<SortedNumbersDetailsDto>();

        AssertExpectedDto(resultDto);
    }

    [Fact]
    public async Task GetById_ReturnsItem()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersCreateDto);
        var created = await post.Content.ReadFromJsonAsync<SortedNumbersDetailsDto>();

        // Act
        var result = await _httpClient.GetFromJsonAsync<SortedNumbersDetailsDto>($"/{SortedNumbersApis.SortedNumbersApiPath}/{created!.Id}");

        // Assert
        AssertExpectedDto(result);
    }

    [Fact]
    public async Task GetAll_ReturnsItems()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersCreateDto);
        var created = await post.Content.ReadFromJsonAsync<SortedNumbersDetailsDto>();

        // Act
        var result = await _httpClient.GetFromJsonAsync<SortedNumbersDetailsDto[]>($"/{SortedNumbersApis.SortedNumbersApiPath}");

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        var foundCreatedBom = Assert.Single(result, x => x.Id == created.Id);

        AssertExpectedDto(foundCreatedBom);
    }

    [Fact]
    public async Task Delete_RemovesItem()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersCreateDto);
        var created = await post.Content.ReadFromJsonAsync<SortedNumbersDetailsDto>();
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
        var getAllInitialResult = _httpClient.GetFromJsonAsAsyncEnumerable<SortedNumbersDetailsDto>($"/{SortedNumbersApis.SortedNumbersApiPath}");
        var initialCount = await getAllInitialResult.CountAsync();

        Assert.NotNull(getAllInitialResult);

        // Test posting
        var postResult = await _httpClient.PostAsJsonAsync($"/{SortedNumbersApis.SortedNumbersApiPath}", s_exampleSortedNumbersCreateDto);

        Assert.NotNull(postResult);

        var postResultViewModel = await postResult.Content.ReadFromJsonAsync<SortedNumbersDetailsDto>();

        AssertExpectedDto(postResultViewModel);

        // Confirm post worked
        var getByIdResultAfterPost = await _httpClient.GetFromJsonAsync<SortedNumbersDetailsDto>($"/{SortedNumbersApis.SortedNumbersApiPath}/{postResultViewModel.Id}");

        AssertExpectedDto(getByIdResultAfterPost);

        // Confirm Get all includes posted item
        var getAllResultAfterPost = _httpClient.GetFromJsonAsAsyncEnumerable<SortedNumbersDetailsDto>($"/{SortedNumbersApis.SortedNumbersApiPath}");

        Assert.NotNull(getAllResultAfterPost);
        Assert.Equal(initialCount + 1, await getAllResultAfterPost.CountAsync());

        var foundPostedItem = await getAllResultAfterPost.FirstOrDefaultAsync(x => x.Id == postResultViewModel.Id);

        AssertExpectedDto(foundPostedItem);

        // Test deleting
        var deleteResult = await _httpClient.DeleteAsync($"/{SortedNumbersApis.SortedNumbersApiPath}/{postResultViewModel.Id}");
        var getByIdResultAfterDelete = await _httpClient.GetAsync($"/{SortedNumbersApis.SortedNumbersApiPath}/{postResultViewModel.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getByIdResultAfterDelete.StatusCode);
    }

    private static void AssertExpectedDto(SortedNumbersDetailsDto sortedNumbersViewModel)
    {
        Assert.Equal(s_exampleSortedNumbersDetailsDto.SortedValues, sortedNumbersViewModel.SortedValues);
        Assert.Equal(s_exampleSortedNumbersDetailsDto.InitialValues, sortedNumbersViewModel.InitialValues);
        Assert.Equal(s_exampleSortedNumbersDetailsDto.IsAscending, sortedNumbersViewModel.IsAscending);
    }
}
