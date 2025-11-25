using System.Net;
using System.Net.Http.Json;
using NumberSorter.Services.Dtos;
using NumberSorter.WebApis.Apis;
using NumberSorter.WebApis.Tests.IntegrationTests.TestHelpers;

namespace NumberSorter.WebApis.Tests.IntegrationTests.Apis;

public class SortedResultsApisTests(AspireAppFixture aspireAppFixture) : IClassFixture<AspireAppFixture>
{
    private readonly HttpClient _httpClient = aspireAppFixture.GetHttpClient();

    private static readonly int[] s_initialValues = [3, 2, 1];
    private static readonly int[] s_sortedValues = [1, 2, 3];

    private static readonly SortedResultsCreateDto s_exampleSortedResultsCreateDto = new()
    {
        InitialValues = s_initialValues,
        IsAscending = true,
    };

    private static readonly SortedResultsDetailsDto s_exampleSortedResultsDetailsDto = new()
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
        var response = await _httpClient.PostAsJsonAsync($"/{SortedResultsApis.SortedResultsApiPath}", s_exampleSortedResultsCreateDto);

        // Assert
        Assert.NotNull(response);

        var resultDto = await response.Content.ReadFromJsonAsync<SortedResultsDetailsDto>();

        AssertExpectedDto(resultDto);
    }

    [Fact]
    public async Task GetById_ReturnsItem()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedResultsApis.SortedResultsApiPath}", s_exampleSortedResultsCreateDto);
        var created = await post.Content.ReadFromJsonAsync<SortedResultsDetailsDto>();

        // Act
        var result = await _httpClient.GetFromJsonAsync<SortedResultsDetailsDto>($"/{SortedResultsApis.SortedResultsApiPath}/{created!.Id}");

        // Assert
        AssertExpectedDto(result);
    }

    [Fact]
    public async Task GetAll_ReturnsItems()
    {
        // Arrange
        var post = await _httpClient.PostAsJsonAsync($"/{SortedResultsApis.SortedResultsApiPath}", s_exampleSortedResultsCreateDto);
        var created = await post.Content.ReadFromJsonAsync<SortedResultsDetailsDto>();

        // Act
        var result = await _httpClient.GetFromJsonAsync<SortedResultsDetailsDto[]>($"/{SortedResultsApis.SortedResultsApiPath}");

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
        var post = await _httpClient.PostAsJsonAsync($"/{SortedResultsApis.SortedResultsApiPath}", s_exampleSortedResultsCreateDto);
        var created = await post.Content.ReadFromJsonAsync<SortedResultsDetailsDto>();
        var byIdUri = $"/{SortedResultsApis.SortedResultsApiPath}/{created!.Id}";

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
        var getAllInitialResult = _httpClient.GetFromJsonAsAsyncEnumerable<SortedResultsDetailsDto>($"/{SortedResultsApis.SortedResultsApiPath}");
        var initialCount = await getAllInitialResult.CountAsync();

        Assert.NotNull(getAllInitialResult);

        // Test posting
        var postResult = await _httpClient.PostAsJsonAsync($"/{SortedResultsApis.SortedResultsApiPath}", s_exampleSortedResultsCreateDto);

        Assert.NotNull(postResult);

        var postResultDetailsDto = await postResult.Content.ReadFromJsonAsync<SortedResultsDetailsDto>();

        AssertExpectedDto(postResultDetailsDto);

        // Confirm post worked
        var getByIdResultAfterPost = await _httpClient.GetFromJsonAsync<SortedResultsDetailsDto>($"/{SortedResultsApis.SortedResultsApiPath}/{postResultDetailsDto.Id}");

        AssertExpectedDto(getByIdResultAfterPost);

        // Confirm Get all includes posted item
        var getAllResultAfterPost = _httpClient.GetFromJsonAsAsyncEnumerable<SortedResultsDetailsDto>($"/{SortedResultsApis.SortedResultsApiPath}");

        Assert.NotNull(getAllResultAfterPost);
        Assert.Equal(initialCount + 1, await getAllResultAfterPost.CountAsync());

        var foundPostedItem = await getAllResultAfterPost.FirstOrDefaultAsync(x => x.Id == postResultDetailsDto.Id);

        AssertExpectedDto(foundPostedItem);

        // Test deleting
        var deleteResult = await _httpClient.DeleteAsync($"/{SortedResultsApis.SortedResultsApiPath}/{postResultDetailsDto.Id}");
        var getByIdResultAfterDelete = await _httpClient.GetAsync($"/{SortedResultsApis.SortedResultsApiPath}/{postResultDetailsDto.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getByIdResultAfterDelete.StatusCode);
    }

    private static void AssertExpectedDto(SortedResultsDetailsDto detailsDto)
    {
        Assert.Equal(s_exampleSortedResultsDetailsDto.SortedValues, detailsDto.SortedValues);
        Assert.Equal(s_exampleSortedResultsDetailsDto.InitialValues, detailsDto.InitialValues);
        Assert.Equal(s_exampleSortedResultsDetailsDto.IsAscending, detailsDto.IsAscending);
    }
}
