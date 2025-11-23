using NumberSorter.WebUI.Models.SortedNumbers;

namespace NumberSorter.WebUI.Interfaces;

public interface ISortedNumbersService
{
    Task<(bool Success, SortedNumbersDetailsViewModel[]? AllDetailsDtos)> GetAllAsync(CancellationToken cancellationToken);
    Task<(bool Success, SortedNumbersDetailsViewModel? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<(bool Success, SortedNumbersDetailsViewModel? DetailsDto)> CreateAsync(SortedNumbersCreateViewModel createViewModel, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}