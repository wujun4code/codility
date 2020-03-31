using System;
using System.Collections.Generic;
using System.Linq;
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

        private void PrettyPrintDictionary<T, K, V>(Dictionary<T, Dictionary<K, V>> dictionary)
        {
            //[1 : [1:1,2:0,5:0]]
            foreach (var t in dictionary)
            {
                Console.WriteLine($"{{{t.Key}: {{{string.Join(", ", t.Value.Select(kvp => $"[{kvp.Key.ToString()}: {kvp.Value.ToString()}]"))}}}}}");
            }
        }

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
            var result1 = CoinSolutionDetail(testCoins1, testAmount);

        }

        private Dictionary<int, int> CoinSolutionDetail(int[] coins, int amount)
        {
            // 首先定义查询表
            var queryTable = new Dictionary<int, Dictionary<int, int>>();
            //下面这一句很重要，它起到了 2 个作用
            // 1. 定义了如果总数为 0，那么所有货币使用张数都为 0 的解决方案
            // 2. 保证了后面  queryTable[remaining] 这一句代码不会出现异常，因为 0 的初始值我们已经插进字典了
            queryTable[0] = coins.ToDictionary(x => x, y => 0);

            // 从 0 开始建立到 amount 的查询表
            for (int i = 0; i <= amount; i++)
            {
                // 轮训每一个面值的货币，是否存在使用该货币的时候，张数存在最小值的解决方案
                foreach (var c in coins)
                {
                    var remaining = i - c;
                    if (remaining < 0) continue;
                    // 如果当前存在用其他面值的货币的解决方案，需要进行如下判断
                    if (queryTable.ContainsKey(i))
                    {
                        // 计算出当前解决方案使用货币的张数
                        var currentRequired = queryTable[i].Values.Sum();
                        // 计算余额，也就是之前较小面额的货币张数，请注意，此时较小面额的张数一定是该面额下最优的，为什么？）
                        // 举个例子，因为我们是从总额 0 建立到总额 11 的查询表，在过程中我们会找到总额为 5 的解决方案，它的解决方案就是 1 张 5 元面值的即可
                        // 那么我们接下去找总额为 6 的最优解决方案，就是在 5 元的基础上，加 1 个 1 块钱，这就是最优的
                        var subRequired = queryTable[remaining].Values.Sum();
                        // 如果你之前用 3 个 2 元的组成了一个 6 元，那么此刻你应该切换到用 1 个 5 元的加 1 个 1 元的组成 6 元
                        if (currentRequired > subRequired + 1)
                        {
                            // 切换更优解决方案，这个就是状态转移的那一瞬间
                            queryTable[i] = SetSolutionDetail(queryTable[remaining], c);
                        }
                    }
                    // 也就是说之前没有对这个总额做过记录，那么就用当前面额先凑齐这个总额
                    else
                    {
                        queryTable[i] = SetSolutionDetail(queryTable[remaining], c);
                    }
                }
            }
            // 所有查询表建立完毕
            // 开始直接查询 amount 的解决方案，如果它使用的张数比 amount 还大，那就说明，使用当前种类面值的货币，根本无法凑齐正好这个总额，返回一个空字典即可
            // 如果找到了，直接返回，这个就是使用最少货币的解决方案下，对应的各面值货币使用情况
            return (queryTable[amount].Values.Sum() == amount + 1) ? new Dictionary<int, int>() : queryTable[amount];
        }
        private Dictionary<int, int> SetSolutionDetail(Dictionary<int, int> remaining, int coin)
        {
            // 这个是一个坑，注意，我们要的是浅拷贝，直接用 = 号赋值会导致你所有的总额的解决方案是操作同一个字典，在 C# 中字典是引用类型
            var copy = remaining.ToDictionary(entry => entry.Key, entry => entry.Value);
            // 如果之前已经使用该面额的货币，就在该货币使用张数上继续加 1
            if (copy.ContainsKey(coin))
            {
                copy[coin] += 1;
            }
            // 如果没有，就直接用 1 张这个面额的货币
            else
            {
                copy[coin] = 1;
            }
            return copy;
        }
        #endregion
    }
}
