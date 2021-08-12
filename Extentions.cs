using System;
using System.Collections.Generic;
using System.Linq;
using ktg.models;

namespace ktg {

    static class Extensions
    {
        public static Order Clone (this Order order) 
        {
            return new Order() {Id = order.Id, Quantity = order.Quantity, Price = order.Price};
        }
        public static List<T> Clone<T>(this List<T> listToClone) where T: Order
        {
            return listToClone.Select(order => (T)order.Clone()).ToList();
        }

        public static List<T> PartialClone<T>(this List<T> listToClone) where T: Order
        {
            return listToClone.Select(order => order).ToList();
        }
    }
}