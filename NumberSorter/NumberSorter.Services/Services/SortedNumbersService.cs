using AutoMapper;
using NumberSorter.Data.Interfaces;
using NumberSorter.Common.Models;
using System;
using NumberSorter.Data.Models;
using System.Diagnostics;

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

        public async Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel)
        {
            var sortedNumbers = _mapper.Map<SortedNumbers>(sortedNumbersViewModel);

            await _sortedNumbersRepository.CreateAsync(sortedNumbers);
            await _sortedNumbersRepository.SaveChangesAsync();

            return sortedNumbersViewModel;
        }

        public SortedNumbersViewModel CalculateSortedList(SortedNumbersViewModel sortedNumbersViewModel)
        {
            var initalValuesListed = sortedNumbersViewModel.InitialValues.Split(",").Select(int.Parse);
            var sortedValues = Enumerable.Empty<int>();

            Stopwatch stopWatch = new Stopwatch();

            if (sortedNumbersViewModel.IsAscending)
            {
                stopWatch.Start();

                sortedValues = initalValuesListed.Order();

                stopWatch.Stop();
            }
            else
            {
                stopWatch.Start();

                sortedValues = initalValuesListed.OrderByDescending(num => num);

                stopWatch.Stop();
            }

            sortedNumbersViewModel.SortedValues = sortedValues;
            sortedNumbersViewModel.SortTime = stopWatch.Elapsed;

            return sortedNumbersViewModel;
        }
    }
}
