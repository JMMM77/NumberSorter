using NumberSorter.WebUI.Dtos;
using NumberSorter.WebUI.Interfaces;
using NumberSorter.WebUI.Models.SortedResults;

namespace NumberSorter.WebUI.Services;

public class SortedResultsService(ISortedResultsApiClient sortedResultsApiClient) : ISortedResultsService
{
    public async Task<(bool Success, SortedResultsDetailsViewModel[]? AllDetailsDtos)> GetAllAsync(CancellationToken cancellationToken)
    {
        var (result, potentialDtos) = await sortedResultsApiClient.GetAllAsync(cancellationToken);

        if (!result || potentialDtos is null)
        {
            return (result, null);
        }

        var returnModels = potentialDtos.Select(x => new SortedResultsDetailsViewModel()
        {
            Id = x.Id,
            InitialValues = x.InitialValues,
            IsAscending = x.IsAscending,
            SortedValues = x.SortedValues,
            SortTime = x.SortTime,
        }).ToArray();

        return (true, returnModels);
    }

    public async Task<(bool Success, SortedResultsDetailsViewModel? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var (result, potentialDto) = await sortedResultsApiClient.GetByIdAsync(id, cancellationToken);

        if (!result || !potentialDto.HasValue)
        {
            return (result, null);
        }

        var foundDto = potentialDto.Value;

        var returnModel = new SortedResultsDetailsViewModel()
        {
            Id = foundDto.Id,
            InitialValues = foundDto.InitialValues,
            IsAscending = foundDto.IsAscending,
            SortedValues = foundDto.SortedValues,
            SortTime = foundDto.SortTime,
        };

        return (true, returnModel);
    }

    public async Task<(bool Success, SortedResultsDetailsViewModel? DetailsDto)> CreateAsync(SortedResultsCreateViewModel createViewModel, CancellationToken cancellationToken)
    {
        var createDto = new SortedResultsCreateDto()
        {
            InitialValues = [.. createViewModel.InitialValues.Split(',').Select(int.Parse)],
            IsAscending = createViewModel.IsAscending,
        };

        var (result, potentialDto) = await sortedResultsApiClient.CreateAsync(createDto, cancellationToken);

        if (!result || !potentialDto.HasValue)
        {
            return (result, null);
        }

        var foundDto = potentialDto.Value;

        var returnModel = new SortedResultsDetailsViewModel()
        {
            Id = foundDto.Id,
            InitialValues = foundDto.InitialValues,
            IsAscending = foundDto.IsAscending,
            SortedValues = foundDto.SortedValues,
            SortTime = foundDto.SortTime,
        };

        return (true, returnModel);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        => await sortedResultsApiClient.DeleteAsync(id, cancellationToken);
}
