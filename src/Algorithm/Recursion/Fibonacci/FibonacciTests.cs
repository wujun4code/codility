using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithm.Recursion.Fibonacci
{
    public class FibonacciTests
    {
        #region use memorandum
        [Fact]
        public void GetFibonacciItemWithMemorandumTest()
        {
            var n1 = 20;
            var n1Item = GetFibonacciItemWithMemorandum(n1);
            Assert.True(n1Item == 6765);
        }

        private IDictionary<int, int> memorandum = new Dictionary<int, int>();
        private int GetFibonacciItemWithMemorandum(int n)
        {
            if (n <= 1) return n;
            if (memorandum.ContainsKey(n)) return memorandum[n];
            memorandum[n] = GetFibonacciItemWithMemorandum(n - 1) + GetFibonacciItemWithMemorandum(n - 2);
            return memorandum[n];
        }
        #endregion

        #region use dp table

        [Fact]
        public void GetFibonacciItemByTableQueryTest()
        {
            var n1 = 20;
            var n1Item = GetFibonacciItemWithMemorandum(n1);
            Assert.True(n1Item == 6765);
        }

        private int GetFibonacciItemByTableQuery(int n)
        {
            var fibonacciTable = new Dictionary<int, int>();
            fibonacciTable[0] = 0;
            fibonacciTable[1] = 1;
            for (int i = 2; i < n; i++)
            {
                fibonacciTable[n] = fibonacciTable[n - 1] + fibonacciTable[n - 2];
            }
            return fibonacciTable[n];
        }
        #endregion

        #region use state transfer

        [Fact]
        public void GetFibonacciItemByStateTransferTest()
        {
            var n1 = 20;
            var n1Item = GetFibonacciItemByStateTransfer(n1);
            Assert.True(n1Item == 6765);
        }
        private int GetFibonacciItemByStateTransfer(int n)
        {
            if (n == 2 || n == 1)
                return 1;

            var previousLeft = 1;
            var previousRight = 1;

            var result = 0;

            for (int i = 2; i < n; i++)
            {
                result = previousLeft + previousRight;
                previousLeft = previousRight;
                previousRight = result;
            }
            return result;
        }
        #endregion
    }
}
