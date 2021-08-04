using RestaurantOrder.Business.Contracts;
using RestaurantOrder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Business
{
    /// <summary>
    /// Manages the orders requests.
    /// </summary>
    public class OrderBusiness
    {

        #region Fields
        private IOrderData orderData;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="OrderBusiness"/>
        /// </summary>
        /// <param name="orderData">The data object to query the menu.</param>
        public OrderBusiness(IOrderData orderData)
        {
            this.orderData = orderData ?? throw new ArgumentNullException(nameof(orderData));
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Handles the order request.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <returns>Returns a <see cref="List{T}"/> with the results.</returns>
        public IEnumerable<string> ProcessOrder(OrderRequest request)
        {
            var order = new List<Menu>();
            var menuResult = orderData.GetMenu(request.DayTime, request.DishType);

            foreach (var dishTypeId in request.DishType)
            {
                Menu menu;
                if (menuResult.Any(m => m.DishTypeId == dishTypeId))
                    menu = menuResult.First(m => m.DishTypeId == dishTypeId);
                else
                    menu = new Menu { DishTypeId = dishTypeId, AllowMultipleOrders = false, Dish = "error" };

                if (menu.AllowMultipleOrders || !order.Any(o => o.DishTypeId == menu.DishTypeId))
                    order.Add(menu);
            }

            var result = new List<string>();
            var grouping = order.OrderBy(r => r.DishTypeId).GroupBy(g => g.Dish);
            foreach (var item in grouping)
            {
                if (item.Count() > 1)
                    result.Add($"{item.Key}(x{item.Count()})");
                else
                    result.Add(item.Key);
            }

            return result;
        }
        #endregion

    }
}