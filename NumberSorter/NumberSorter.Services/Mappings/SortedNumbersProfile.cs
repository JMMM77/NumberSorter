using AutoMapper;
using NumberSorter.Data.Models;
using NumberSorter.Common.Models;

namespace NumberSorter.Services.Mappings
{
    public class SortedNumbersProfile : Profile
    {
        public SortedNumbersProfile()
        {
            CreateMap<SortedNumbers, SortedNumbersViewModel>(MemberList.Destination).ReverseMap();
        }
    }
}
