using System;
using System.Collections.Generic;
using Xunit;
using ktg.models;
using ktg.services;

namespace ktg.tests
{
    public class InternalTransferTest
    {
        InternalTransfer service;

        public InternalTransferTest() {
            service = new InternalTransfer();
        }
        [Fact]
        public void Test1()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 10, Price = 100},
                new Order() {Id = 2, Quantity = -5, Price = 100},
                new Order() {Id = 3, Quantity = -5, Price = 100},
                new Order() {Id = 4, Quantity = 5, Price = 100},
                new Order() {Id = 5, Quantity = -5, Price = 100},
                new Order() {Id = 6, Quantity = 2, Price = 100}
            };

            var result = service.Collect(orders);

            Assert.Equal(new List<int>() {1,2,3,4,5}, result);
        }

        [Fact]
        public void Test2()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 10, Price = 100},
                new Order() {Id = 2, Quantity = -5, Price = 100}
            };

            var result = service.Collect(orders);

            Assert.Equal(new List<int>(), result);
        }

        [Fact]
        public void Test3()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 10, Price = 100},
                new Order() {Id = 2, Quantity = -3, Price = 100},
                new Order() {Id = 3, Quantity = -5, Price = 100},
                new Order() {Id = 4, Quantity = 5, Price = 100},
                new Order() {Id = 5, Quantity = -2, Price = 100}
            };

            var result = service.Collect(orders);

            Assert.Equal(new List<int>() {1,2,3,5}, result);
        }

        [Fact]
        public void Test4()
        {
            var orders = new List<Order> {
                new Order() {Id = 1, Quantity = 10, Price = 100},
                new Order() {Id = 2, Quantity = -10, Price = 200}
            };

            var result = service.Collect(orders);

            Assert.Equal(new List<int>(), result);
        }
    }
}
