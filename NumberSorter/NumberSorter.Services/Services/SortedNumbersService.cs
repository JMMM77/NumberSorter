using AutoMapper;
using NumberSorter.Data.Interfaces;
using NumberSorter.Common.Models;

namespace NumberSorter.Services.Services
{
    internal class SortedNumbersService(ISortedNumbersRespository sortedNumbersRepository, IMapper mapper) : ISortedNumbersService
    {
        private readonly ISortedNumbersRespository _sortedNumbersRepository = sortedNumbersRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<SortedNumbersViewModel>> GetAllAsync()
        {
            var dbModels = await _sortedNumbersRepository.GetAllAsync();

            return _mapper.Map<List<SortedNumbersViewModel>>(dbModels);
        }
    }
}
