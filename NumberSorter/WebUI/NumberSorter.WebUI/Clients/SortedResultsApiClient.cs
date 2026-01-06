using NumberSorter.WebUI.Dtos;
using NumberSorter.WebUI.Interfaces;

namespace NumberSorter.WebUI.Clients;

public sealed class SortedResultsApiClient(HttpClient httpClient, ILogger<SortedResultsApiClient> logger) : ISortedResultsApiClient
{
    private const string API_PATH = "sorted-results";

    public async Task<(bool Success, SortedResultsDetailsDto[]? DetailsDto)> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var returnedDto = await httpClient.GetFromJsonAsync<SortedResultsDetailsDto[]>(CreateApiPath(), cancellationToken);

            return (Success: true, returnedDto);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error getting the sorted results.");
            }

            return (false, null);
        }
    }

    public async Task<(bool Success, SortedResultsDetailsDto? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var returnedDto = await httpClient.GetFromJsonAsync<SortedResultsDetailsDto>(CreateApiPathWithId(id), cancellationToken);

            return (Success: true, returnedDto);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error getting the sorted results for Id: '{Id}'", id);
            }

            return (false, null);
        }
    }

    public async Task<(bool Success, SortedResultsDetailsDto? DetailsDto)> CreateAsync(SortedResultsCreateDto createDto, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;

        try
        {
            response = await httpClient.PostAsJsonAsync(CreateApiPath(), createDto, cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error creating the sorted result entry for payload: {Payload}", createDto);
            }

            return (false, null);
        }

        if (!response.IsSuccessStatusCode)
        {
            return (false, null);
        }

        SortedResultsDetailsDto detailsDto;

        try
        {
            detailsDto = await response.Content.ReadFromJsonAsync<SortedResultsDetailsDto>(cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error deserilising the created sorted result entry for payload: {Payload}", createDto);
            }

            return (false, null);
        }

        return (true, detailsDto);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;

        try
        {
            response = await httpClient.DeleteAsync(CreateApiPathWithId(id), cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error deleting the sorted result entry with Id: '{Id}'", id);
            }

            return false;
        }

        if (!response.IsSuccessStatusCode)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError("Error deleting the sorted result entry with Id: '{Id}'", id);
            }

            return false;
        }

        return true;
    }

    private static string CreateApiPath()
        => $"/{API_PATH}";

    private static string CreateApiPathWithId(int id)
        => $"/{API_PATH}/{id}";

}
