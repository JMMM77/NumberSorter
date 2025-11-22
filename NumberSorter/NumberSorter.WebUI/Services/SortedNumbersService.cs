using NumberSorter.WebUI.Dtos;
using NumberSorter.WebUI.Interfaces;
using NumberSorter.WebUI.Models.SortedNumbers;

namespace NumberSorter.WebUI.Services;

public class SortedNumbersService(ISortedNumbersApiClient numberSorterApiClient) : ISortedNumbersService
{
    public async Task<(bool Success, SortedNumbersDetailsViewModel[]? AllDetailsDtos)> GetAllAsync(CancellationToken cancellationToken)
    {
        var (result, potentialDtos) = await numberSorterApiClient.GetAllAsync(cancellationToken);

        if (!result || potentialDtos is null)
        {
            return (result, null);
        }

        var returnModels = potentialDtos.Select(x => new SortedNumbersDetailsViewModel()
        {
            Id = x.Id,
            InitialValues = x.InitialValues,
            IsAscending = x.IsAscending,
            SortedValues = x.SortedValues,
            SortTime = x.SortTime,
        }).ToArray();

        return (true, returnModels);
    }

    public async Task<(bool Success, SortedNumbersDetailsViewModel? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var (result, potentialDto) = await numberSorterApiClient.GetByIdAsync(id, cancellationToken);

        if (!result || !potentialDto.HasValue)
        {
            return (result, null);
        }

        var foundDto = potentialDto.Value;

        var returnModel = new SortedNumbersDetailsViewModel()
        {
            Id = foundDto.Id,
            InitialValues = foundDto.InitialValues,
            IsAscending = foundDto.IsAscending,
            SortedValues = foundDto.SortedValues,
            SortTime = foundDto.SortTime,
        };

        return (true, returnModel);
    }

    public async Task<(bool Success, SortedNumbersDetailsViewModel? DetailsDto)> CreateAsync(SortedNumbersCreateViewModel createViewModel, CancellationToken cancellationToken)
    {
        var createDto = new SortedNumbersCreateDto()
        {
            InitialValues = createViewModel.InitialValues.Split(','),
            IsAscending = createViewModel.IsAscending,
        };

        var (result, potentialDto) = await numberSorterApiClient.CreateAsync(createDto, cancellationToken);

        if (!result || !potentialDto.HasValue)
        {
            return (result, null);
        }

        var foundDto = potentialDto.Value;

        var returnModel = new SortedNumbersDetailsViewModel()
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
        => await numberSorterApiClient.DeleteAsync(id, cancellationToken);
}
