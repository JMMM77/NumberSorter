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

        public async Task<SortedNumbersViewModel> GetById(int sortedNumbersId)
        {
            
            var sortedNumbersViewModel = await _sortedNumbersRepository.GetById(sortedNumbersId);
            return _mapper.Map<SortedNumbersViewModel>(sortedNumbersViewModel);
        }

        public async Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel)
        {
            var sortedNumbers = _mapper.Map<SortedNumbers>(sortedNumbersViewModel);

            await _sortedNumbersRepository.CreateAsync(sortedNumbers);
            await _sortedNumbersRepository.SaveChangesAsync();

            return sortedNumbersViewModel;
        }

        public async Task<bool> DeleteAsync(int sortedNumbersId)
        {
            var sortedNumbersToDelete = await _sortedNumbersRepository.GetById(sortedNumbersId);

            if(sortedNumbersToDelete != null)
            {
                _sortedNumbersRepository.Delete(sortedNumbersToDelete);
                return await _sortedNumbersRepository.SaveChangesAsync();
            }

            return true;
        }

        public SortedNumbersViewModel CalculateSortedList(SortedNumbersViewModel sortedNumbersViewModel)
        {
            var initalValuesListed = sortedNumbersViewModel.InitialValues.Split(",").Select(int.Parse);
            var sortedValues = Enumerable.Empty<int>();

            Stopwatch stopWatch = new();

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
