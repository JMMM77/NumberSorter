using NumberSorter.WebUI.Dtos;

namespace NumberSorter.WebUI.Interfaces;

public interface ISortedNumbersApiClient
{
    Task<(bool Success, SortedNumbersDetailsDto[]? DetailsDto)> GetAllAsync(CancellationToken cancellationToken);
    Task<(bool Success, SortedNumbersDetailsDto? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<(bool Success, SortedNumbersDetailsDto? DetailsDto)> CreateAsync(SortedNumbersCreateDto createDto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}