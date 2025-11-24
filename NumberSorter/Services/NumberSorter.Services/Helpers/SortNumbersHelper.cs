using System.Diagnostics;

namespace NumberSorter.Services.Helpers;

internal static class SortNumbersHelper
{

    /// <summary>
    /// Sorts a list of numbers based on the sorting criteria provided in the dto.
    /// </summary>
    /// <param name="dto">The dto containing sorting criteria.</param>
    /// <returns>The dto with sorted values and sort time.</returns>
    public static (int[] SortedValues, TimeSpan SortTime) CalculateSortedList(int[] valuesToSort, bool isAscending)
    {
        int[] sortedValues;

        Stopwatch stopWatch = new();

        if (isAscending)
        {
            stopWatch.Start();

            sortedValues = [.. valuesToSort.Order()];

            stopWatch.Stop();
        }
        else
        {
            stopWatch.Start();

            sortedValues = [.. valuesToSort.OrderByDescending(num => num)];

            stopWatch.Stop();
        }

        return (sortedValues, stopWatch.Elapsed);
    }
}