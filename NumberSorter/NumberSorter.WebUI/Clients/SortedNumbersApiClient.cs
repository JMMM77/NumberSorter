using NumberSorter.WebUI.Dtos;
using NumberSorter.WebUI.Interfaces;

namespace NumberSorter.WebUI.Clients;

public sealed class SortedNumbersApiClient(IHttpClientFactory httpClientFactory, ILogger<SortedNumbersApiClient> logger) : ISortedNumbersApiClient
{
    private const string API_PATH = "sorted-numbers";

    public async Task<(bool Success, SortedNumbersDetailsDto[]? DetailsDto)> GetAllAsync(CancellationToken cancellationToken)
    {
        var http = httpClientFactory.CreateClient();

        try
        {
            var returnedDto = await http.GetFromJsonAsync<SortedNumbersDetailsDto[]>(CreateApiPath(), cancellationToken);

            return (Success: true, returnedDto);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error getting the sorted numbers.");
            }

            return (false, null);
        }
    }

    public async Task<(bool Success, SortedNumbersDetailsDto? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var http = httpClientFactory.CreateClient();

        try
        {
            var returnedDto = await http.GetFromJsonAsync<SortedNumbersDetailsDto>(CreateApiPathWithId(id), cancellationToken);

            return (Success: true, returnedDto);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error getting the sorted numbers for Id: '{Id}'", id);
            }

            return (false, null);
        }
    }

    public async Task<(bool Success, SortedNumbersDetailsDto? DetailsDto)> CreateAsync(SortedNumbersCreateDto createDto, CancellationToken cancellationToken)
    {
        var http = httpClientFactory.CreateClient();
        HttpResponseMessage response;

        try
        {
            response = await http.PostAsJsonAsync(CreateApiPath(), createDto, cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error creating sorted number entry for payload: {Payload}", createDto);
            }

            return (false, null);
        }

        if (!response.IsSuccessStatusCode)
        {
            return (false, null);
        }

        SortedNumbersDetailsDto detailsDto;

        try
        {
            detailsDto = await response.Content.ReadFromJsonAsync<SortedNumbersDetailsDto>(cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error deserilising created sorted number entry for payload: {Payload}", createDto);
            }

            return (false, null);
        }

        return (true, detailsDto);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var http = httpClientFactory.CreateClient();
        HttpResponseMessage response;

        try
        {
            response = await http.DeleteAsync(CreateApiPathWithId(id), cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error deleting sorted number entry with Id: '{Id}'", id);
            }

            return false;
        }

        if (!response.IsSuccessStatusCode)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError("Error deleting sorted number entry with Id: '{Id}'", id);
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
