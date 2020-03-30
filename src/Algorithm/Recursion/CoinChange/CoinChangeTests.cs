using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithm.Recursion.CoinChange
{
    public class CoinChangeTests
    {
        #region Violence solution
        [Fact]
        public void GetCoinChangeWithViolenceTest()
        {

        }

        private int CoinChangeWithViolence(int[] coins, int amount)
        {
            if (amount < 0) return -1;
            if (amount == 0) return 0;

            var result = int.MaxValue;
            foreach (var c in coins)
            {
                var remaining = amount - c;
                var remainingResult = CoinChangeWithViolence(coins, remaining);
                if (remainingResult == -1) continue;
                result = Math.Min(remainingResult + 1, remaining);
            }
            return result != int.MaxValue ? result : -1;
        }

        #endregion
        #region use Recursion with Memorandum
        [Fact]
        public void GetCoinChangeWithMemorandumTest()
        {

        }
        private IDictionary<int, int> memorandum = new Dictionary<int, int>();
        private int CoinChangeWithMemorandum(int[] coins, int amount)
        {
            if (amount < 0) return -1;
            if (amount == 0) return 0;

            if (memorandum.ContainsKey(amount)) return memorandum[amount];
            var result = int.MaxValue;
            foreach (var c in coins)
            {
                var remaining = amount - c;
                var remainingResult = CoinChangeWithMemorandum(coins, remaining);
                if (remainingResult == -1) continue;
                result = Math.Min(remainingResult + 1, remaining);
            }
            memorandum[amount] = result != int.MaxValue ? result : -1;
            return memorandum[amount];
        }
        #endregion

        #region use Table Query with Memorandum

        [Fact]
        public void GetCoinChangeWithTableQueryTest()
        {
            var testCoins1 = new int[] { 1, 2, 5 };
            var testAmount = 11;
            var expectedResult1 = 3;

            var testResult1 = CoinChangeWithTableQuery(testCoins1, testAmount);
            Assert.True(testResult1 == expectedResult1);
        }

        private int CoinChangeWithTableQuery(int[] coins, int amount)
        {
            var queryTable = new Dictionary<int, int>()
            {
                { 0, 0 }
            };

            for (int i = 0; i <= amount; i++)
            {
                foreach (var c in coins)
                {
                    var remaining = i - c;
                    if (remaining < 0) continue;
                    queryTable[i] = queryTable.ContainsKey(i) ? Math.Min(queryTable[i], queryTable[remaining] + 1) : queryTable[remaining] + 1;
                }
            }

            return (queryTable[amount] == amount + 1) ? -1 : queryTable[amount];
        }
        #endregion

        #region get Coin solution
        [Fact]
        public void GetCoinChangeSolutionDetailWithTableQueryTest()
        {
            var testCoins1 = new int[] { 1, 2, 5 };
            var testAmount = 11;
            var expectedResult1 = new Dictionary<int, int>()
            {
                { 1, 1 },
                { 2, 0 },
                { 5, 2 }
            };
        }

        private IDictionary<int, int> CoinSolutionDetail(int[] coins, int amount) 
        {
            var queryTable = new Dictionary<int, Dictionary<int, int>>();
            for (int i = 0; i <= amount; i++)
            {
                var coinUsage = new Dictionary<int, int>();
                foreach (var c in coins)
                {
                    var remaining = i - c;
                    if (remaining < 0) continue;
                    
                    queryTable[i] = queryTable.ContainsKey(i) ? Math.Min(queryTable[i], queryTable[remaining] + 1) : queryTable[remaining] + 1;
                }
            }
        }
        #endregion
    }
}
