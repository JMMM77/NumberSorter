using AutoMapper;
using NumberSorter.Data.Interfaces;
using NumberSorter.Common.Models;
using NumberSorter.Data.Models;
using System.Diagnostics;

namespace NumberSorter.Services.Services
{
    internal class SortedNumbersService(ISortedNumbersRespository sortedNumbersRepository, IMapper mapper) : ISortedNumbersService
    {
        private readonly ISortedNumbersRespository _sortedNumbersRepository = sortedNumbersRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieves all sorted numbers asynchronously.
        /// </summary>
        /// <returns>A list of SortedNumbersViewModel.</returns>
        public async Task<List<SortedNumbersViewModel>> GetAllAsync()
        {
            var dbModels = await _sortedNumbersRepository.GetAllAsync();

            return _mapper.Map<List<SortedNumbersViewModel>>(dbModels);
        }

        /// <summary>
        /// Retrieves a sorted number by its ID asynchronously.
        /// </summary>
        /// <param name="sortedNumbersId">The ID of the sorted numbers.</param>
        /// <returns>The SortedNumbersViewModel with the specified ID.</returns>
        public async Task<SortedNumbersViewModel> GetById(int sortedNumbersId)
        {
            
            var sortedNumbersViewModel = await _sortedNumbersRepository.GetById(sortedNumbersId);
            return _mapper.Map<SortedNumbersViewModel>(sortedNumbersViewModel);
        }

        /// <summary>
        /// Creates a new sorted number asynchronously.
        /// </summary>
        /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel to create.</param>
        /// <returns>The created SortedNumbersViewModel.</returns>
        public async Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel)
        {
            var sortedNumbers = _mapper.Map<SortedNumbers>(sortedNumbersViewModel);

            await _sortedNumbersRepository.CreateAsync(sortedNumbers);
            await _sortedNumbersRepository.SaveChangesAsync();

            return _mapper.Map<SortedNumbersViewModel>(sortedNumbers);
        }

        /// <summary>
        /// Deletes a sorted number asynchronously by its ID.
        /// </summary>
        /// <param name="sortedNumbersId">The ID of the sorted numbers to delete.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Sorts a list of numbers based on the sorting criteria provided in the SortedNumbersViewModel.
        /// </summary>
        /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel containing sorting criteria.</param>
        /// <returns>The SortedNumbersViewModel with sorted values and sort time.</returns>
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
