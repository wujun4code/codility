using System;
using Xunit;
using System.Linq;

namespace Algorithm.BinarySearch.MinMaxDivision
{
    public class MinMaxDivisionTests
    {
        [Fact]
        public void Test1()
        {
            var testArray1 = new int[] { 2, 1, 5, 1, 2, 2, 2 };
            solution(3, 5, testArray1);
        }

        public int solution(int K, int M, int[] A)
        {
            var start = A.Max();
            var end = A.Sum();
            var mid = 0;
            while (start <= end)
            {
                mid = (end + start) / 2;
                var blocksNeeded = GetBlocks(mid, A);
                if (K >= blocksNeeded)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }
            return start;
        }


        private int GetBlocks(int sum, int[] A)
        {
            var currentSum = 0;
            var currentBlocks = 0;
            for (int i = 0; i < A.Length; i++)
            {
                currentSum += A[i];
                if (currentSum > sum)
                {
                    currentSum = A[i];
                    currentBlocks += 1;
                }
            }
            return ++currentBlocks;
        }
    }


}
