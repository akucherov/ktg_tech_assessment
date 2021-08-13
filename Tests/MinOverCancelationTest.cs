using System;
using System.Collections.Generic;
using Xunit;
using ktg.models;
using ktg.services;

namespace ktg.tests
{
    public class MinOverCancelationTest
    {
        MinOverCancelation service;

        public MinOverCancelationTest() {
            service = new MinOverCancelation();
        }
        
        [Fact]
        public void EmptyListTest() {
            var orders = new List<Order>();

            var result = service.Collect(orders, 0);

            Assert.Equal(new List<int>(), result);
        }

        [Fact]
        public void UexpectedCancelationTargetTest() {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 3, Price = 100},
                new Order() {Id = 2, Quantity = 2, Price = 100},
                new Order() {Id = 3, Quantity = 3, Price = 100},
                new Order() {Id = 4, Quantity = 1, Price = 100},
                new Order() {Id = 5, Quantity = 5, Price = 100}
            };

            var result = service.Collect(orders, int.MaxValue);

            Assert.Equal(new List<int>(), result);
        }

        [Fact]
        public void Test1()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 3, Price = 100},
                new Order() {Id = 2, Quantity = 2, Price = 100},
                new Order() {Id = 3, Quantity = 3, Price = 100},
                new Order() {Id = 4, Quantity = 1, Price = 100},
                new Order() {Id = 5, Quantity = 5, Price = 100}
            };

            var result = service.Collect(orders, 2);

            Assert.Equal(new List<int>() {2}, result);
        }

        [Fact]
        public void Test2()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 3, Price = 100},
                new Order() {Id = 2, Quantity = 2, Price = 100},
                new Order() {Id = 3, Quantity = 3, Price = 100},
                new Order() {Id = 4, Quantity = 1, Price = 100},
                new Order() {Id = 5, Quantity = 5, Price = 100}
            };


            var result = service.Collect(orders, 7);

            Assert.Equal(new List<int>() {2, 5}, result);
        }

        [Fact]
        public void Test3()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 5, Price = 100},
                new Order() {Id = 2, Quantity = 2, Price = 100},
                new Order() {Id = 3, Quantity = 7, Price = 100},
                new Order() {Id = 4, Quantity = 2, Price = 100}
            };

            var result = service.Collect(orders, 3);

            Assert.Equal(new List<int>() {2, 4}, result);
        }

        [Fact]
        public void Test4()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 5, Price = 100},
                new Order() {Id = 2, Quantity = 5, Price = 100},
                new Order() {Id = 3, Quantity = 5, Price = 100},
                new Order() {Id = 4, Quantity = 6, Price = 100},
                new Order() {Id = 5, Quantity = 15, Price = 100},
                new Order() {Id = 6, Quantity = 55, Price = 100}
            };

            var result = service.Collect(orders, 12);

            Assert.Equal(new List<int>() {5}, result);
        }

        [Fact(Skip="optional")]
        public void StressTest()
        {
            int size = 20;
            int sum = 0;
            var orders = new List<Order>();
            var expectedResult = new List<int>();
            for (int i = 1; i <= size; i++) {
                var order = new Order() {Id = i, Quantity = i, Price = 100 };
                orders.Add(order);
                expectedResult.Add(i);
            }

            var result = service.Collect(orders, sum);

            AssemblyTraitAttribute.Equals(expectedResult, result);
        }
    }
}
