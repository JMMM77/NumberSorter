using AutoMapper;
using NumberSorter.Shared.Models;
using NumberSorter.Data.Models;

namespace NumberSorter.Services.Mappings;

/// <summary>
/// Represents the AutoMapper profile for mapping between SortedNumbers and SortedNumbersViewModel.
/// </summary>
public class SortedNumbersProfile : Profile
{

    /// <summary>
    /// Initializes a new instance of the SortedNumbersProfile class.
    /// </summary>
    public SortedNumbersProfile()
    {
        this.CreateMap<SortedNumbers, SortedNumbersViewModel>(MemberList.Destination).ReverseMap();
    }
}
