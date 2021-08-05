using NUnit.Framework;
using RestaurantOrder.Business;
using RestaurantOrder.Entities;
using RestaurantOrder.UnitTest.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.UnitTest
{
    public class OrderUnitTest
    {
        private OrderDataMock OrderData;
        private MenuSelector selector;
        private MenuGrouper grouper;

        [SetUp]
        public void Setup()
        {
            OrderData = new OrderDataMock();
            selector = new MenuSelector();
            grouper = new MenuGrouper();
        }

        [Test]
        public void TestMorningSimpleSuccess()
        {
            var request = new OrderRequest { DayTime = "morning", DishType = new List<int> { 1, 2, 3 } };
            var expectedResult = new List<string> { "eggs", "Toast", "coffee" };
            OrderData.GetMenu(CreateMenuDataMockResult(request));
            var business = new OrderBusiness(OrderData.Object, selector, grouper);
            var result = business.ProcessOrder(request);
            var valuecheck1 = result.Except(expectedResult).ToList();
            var valuecheck2 = expectedResult.Except(result).ToList();
            Assert.IsTrue(!valuecheck1.Any() && !valuecheck2.Any());
        }

        [Test]
        public void TestNightSimpleSuccess()
        {
            var request = new OrderRequest { DayTime = "night", DishType = new List<int> { 1, 2, 3, 4 } };
            var expectedResult = new List<string> { "steak", "potato", "wine", "cake" };
            OrderData.GetMenu(CreateMenuDataMockResult(request));
            var business = new OrderBusiness(OrderData.Object, selector, grouper);
            var result = business.ProcessOrder(request);
            var valuecheck1 = result.Except(expectedResult).ToList();
            var valuecheck2 = expectedResult.Except(result).ToList();
            Assert.IsTrue(!valuecheck1.Any() && !valuecheck2.Any());
        }

        [Test]
        public void TestMorningSimpleError()
        {
            var request = new OrderRequest { DayTime = "morning", DishType = new List<int> { 1, 2, 3, 4 } };
            var expectedResult = new List<string> { "eggs", "Toast", "coffee", "error" };
            OrderData.GetMenu(CreateMenuDataMockResult(request));
            var business = new OrderBusiness(OrderData.Object, selector, grouper);
            var result = business.ProcessOrder(request);
            var valuecheck1 = result.Except(expectedResult).ToList();
            var valuecheck2 = expectedResult.Except(result).ToList();
            Assert.IsTrue(!valuecheck1.Any() && !valuecheck2.Any());
        }

        [Test]
        public void TestNightSimpleError()
        {
            var request = new OrderRequest { DayTime = "night", DishType = new List<int> { 1, 2, 3, 4, 5 } };
            var expectedResult = new List<string> { "steak", "potato", "wine", "cake", "error" };
            OrderData.GetMenu(CreateMenuDataMockResult(request));
            var business = new OrderBusiness(OrderData.Object, selector, grouper);
            var result = business.ProcessOrder(request);
            var valuecheck1 = result.Except(expectedResult).ToList();
            var valuecheck2 = expectedResult.Except(result).ToList();
            Assert.IsTrue(!valuecheck1.Any() && !valuecheck2.Any());
        }

        [Test]
        public void TestNightVariation1()
        {
            var request = new OrderRequest { DayTime = "night", DishType = new List<int> { 1, 2, 2, 3, 4, 5 } };
            var expectedResult = new List<string> { "steak", "potato(x2)", "wine", "cake", "error" };
            OrderData.GetMenu(CreateMenuDataMockResult(request));
            var business = new OrderBusiness(OrderData.Object, selector, grouper);
            var result = business.ProcessOrder(request);
            var valuecheck1 = result.Except(expectedResult).ToList();
            var valuecheck2 = expectedResult.Except(result).ToList();
            Assert.IsTrue(!valuecheck1.Any() && !valuecheck2.Any());
        }

        [Test]
        public void TestMorningVariation1()
        {
            var request = new OrderRequest { DayTime = "morning", DishType = new List<int> { 4, 3, 2, 1 } };
            var expectedResult = new List<string> { "eggs", "Toast", "coffee", "error" };
            OrderData.GetMenu(CreateMenuDataMockResult(request));
            var business = new OrderBusiness(OrderData.Object, selector, grouper);
            var result = business.ProcessOrder(request);
            var valuecheck1 = result.Except(expectedResult).ToList();
            var valuecheck2 = expectedResult.Except(result).ToList();
            Assert.IsTrue(!valuecheck1.Any() && !valuecheck2.Any());
        }

        private IEnumerable<Menu> CreateMenuDataMockResult(OrderRequest request)
        {
            var list = new List<Menu>();
            if (request.DayTime == "morning")
                list = new List<Menu>
                {
                    new Menu{ DishTypeId = 1, Dish = "eggs", AllowMultipleOrders = false },
                    new Menu{ DishTypeId = 2, Dish = "Toast", AllowMultipleOrders = false },
                    new Menu{ DishTypeId = 3, Dish = "coffee", AllowMultipleOrders = true }
                };

            if (request.DayTime == "night")
                list = new List<Menu>
                {
                    new Menu{ DishTypeId = 1, Dish = "steak", AllowMultipleOrders = false },
                    new Menu{ DishTypeId = 2, Dish = "potato", AllowMultipleOrders = true },
                    new Menu{ DishTypeId = 3, Dish = "wine", AllowMultipleOrders = false },
                    new Menu{ DishTypeId = 4, Dish = "cake", AllowMultipleOrders = false }
                };

            var test = list.Where(m => request.DishType.Contains(m.DishTypeId));
            return test;
        }
    }
}