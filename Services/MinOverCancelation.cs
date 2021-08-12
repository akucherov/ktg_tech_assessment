using System;
using System.Collections.Generic;
using System.Linq;
using ktg.models;

namespace ktg.services {

    class MinOverCancelation {

        public static IEnumerable<int[]> Indexes(int k, int l) {
            int[] result = new int[k];
            Stack<int> temp = new Stack<int>(k);
            temp.Push(0);
            while (temp.Count > 0) {
                int i = temp.Count - 1;
                int v = temp.Pop();
                while (v < l) {
                    result[i++] = v++;
                    temp.Push(v);
                    if (i != k) continue;
                    yield return (int[])result.Clone();
                    break;
                }
            }
        }

        public List<int> Collect(List<Order> orders, int cancelTarget) {
            int bestResult = int.MaxValue;
            var result = new List<Order>();

            for (int i = 1; i < orders.Count; i++) {
                foreach (int[] indexes in Indexes(i, orders.Count)) {
                    int sum = 0;
                    foreach (int index in indexes) {
                        sum += orders[index].Quantity;
                    }
                    if (sum >= cancelTarget && sum < bestResult) {
                        result = new List<Order>();
                        bestResult = sum;
                        foreach (int index in indexes) {
                            result.Add(orders[index]);
                        }
                        if (bestResult == cancelTarget) break;
                    }
                }
                if (bestResult == cancelTarget) break;
            }

            return result.Select(o => o.Id).ToList();
        }
    }
}