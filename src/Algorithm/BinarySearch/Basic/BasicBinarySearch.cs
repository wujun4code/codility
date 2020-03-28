using System;
using Xunit;

namespace Algorithm.BinarySearch.Basic
{
    public class BasicBinarySearch
    {
        #region use basic
        [Fact]
        public void BinarySearchTest()
        {
            var testArray1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Assert.True(6 == BinarySearch(testArray1, 6));
            Assert.True(10 == BinarySearch(testArray1, 10));
        }

        private int BinarySearch(int[] A, int target)
        {
            int left = 0;
            int right = A.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (A[mid] == target)
                    return mid;
                else if (A[mid] < target)
                    left = mid + 1;
                else if (A[mid] > target)
                    right = mid - 1;
            }
            return -1;
        }
        #endregion

        #region search left bound
        
        #endregion
    }
}
