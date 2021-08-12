using System.Collections.Generic;
using System.Linq;
using ktg.models;

namespace ktg.services {

    class InternalTransfer
    {
        public List<int> Collect(List<Order> orders)
        {
            var dict = new Dictionary<decimal, List<Order>>();

            //Group orders by a price
            foreach (Order o in orders) {
                if (dict.ContainsKey(o.Price)) {
                    dict[o.Price].Add(o);
                } else {
                    dict[o.Price] = new List<Order> {o};
                }
            }

            var longestZeroSumList = new List<Order>();

            foreach (List<Order> l in dict.Values) {
                var result = this.Check(l);
                if (result.Count > longestZeroSumList.Count) longestZeroSumList = result;
            }

            return longestZeroSumList.Select(o => o.Id).ToList();
        }       

        List<Order> Check(List<Order> orders) 
        {
            if (orders.Sum(o => o.Quantity) == 0) return orders;

            var longestZeroSumList = new List<Order>();
            for (int i = 0; i < orders.Count; i++) {
                List<Order> new_orders = orders.PartialClone();
                new_orders.RemoveAt(i);
                var result = this.Check(new_orders);

                if (result.Count > longestZeroSumList.Count) longestZeroSumList = result;
            }

            return longestZeroSumList;
        }
    } 
}