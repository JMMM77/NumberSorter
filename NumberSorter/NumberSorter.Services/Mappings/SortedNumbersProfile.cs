using AutoMapper;
using NumberSorter.Data.Models;
using NumberSorter.Common.Models;

namespace NumberSorter.Services.Mappings
{
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
            CreateMap<SortedNumbers, SortedNumbersViewModel>(MemberList.Destination).ReverseMap();
        }
    }
}
