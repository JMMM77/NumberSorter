using NumberSorter.Services.Helpers;

namespace NumberSorter.Services.Tests.Helpers;

public class SortNumbersHelperTests
{
    [Theory]
    [MemberData(nameof(CalculateSortedList_ReturnsSortedArrayWithTime_TestData))]
    public void CalculateSortedList_ReturnsSortedArrayWithTime(int[] initialValues, bool isAscending)
    {
        // Arrange
        var expectedSortedArray = isAscending ? initialValues.Order() : initialValues.OrderByDescending(x => x);

        // Act
        var (resultSortedArray, timeTaken) = SortNumbersHelper.CalculateSortedList(initialValues, isAscending);

        Assert.Equal(expectedSortedArray, resultSortedArray);
        Assert.True(timeTaken > TimeSpan.Zero);
    }

    public static TheoryData<int[], bool> CalculateSortedList_ReturnsSortedArrayWithTime_TestData()
        => new()
        {
            {
                [0, 2, 3, 1],
                true
            },
            {
                [0, -2, 3, 1],
                true
            },
            {
                [0, -2, -3, -1],
                true
            },
            {
                [0, 2, 3, 1],
                false
            },
            {
                [0, -2, 3, 1],
                false
            },
            {
                [0, -2, -3, -1],
                false
            },
        };
}
