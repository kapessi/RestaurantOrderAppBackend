using Moq;
using RestaurantOrder.Business.Contracts;
using RestaurantOrder.Entities;
using System.Collections.Generic;

namespace RestaurantOrder.UnitTest.Mocks
{
    public class OrderDataMock : Mock<IOrderData>
    {
        public void GetMenu(IEnumerable<Menu> result)
        {
            Setup(m => m.GetMenu(It.IsAny<string>(), It.IsAny<IEnumerable<int>>())).Returns(result);
        }
    }
}