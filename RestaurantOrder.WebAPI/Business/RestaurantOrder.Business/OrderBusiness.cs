using RestaurantOrder.Business.Contracts;
using RestaurantOrder.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantOrder.Business
{
    /// <summary>
    /// Manages the orders requests.
    /// </summary>
    public class OrderBusiness
    {

        #region Fields
        private IOrderData orderData;
        private MenuSelector selector;
        private MenuGrouper grouper;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="OrderBusiness"/>
        /// </summary>
        /// <param name="orderData">The data object to query the menu.</param>
        /// <param name="selector">The menu selector.</param>
        /// <param name="grouper">The menu grouper.</param>
        public OrderBusiness(IOrderData orderData, MenuSelector selector, MenuGrouper grouper)
        {
            this.orderData = orderData ?? throw new ArgumentNullException(nameof(orderData));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
            this.grouper = grouper ?? throw new ArgumentNullException(nameof(grouper));
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Handles the order request.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> with the results.</returns>
        public IEnumerable<string> ProcessOrder(OrderRequest request)
        {
            var menuResult = orderData.GetMenu(request.DayTime, request.DishType);

            var order = selector.SelectMenu(request.DishType, menuResult);

            var result = grouper.GroupMenu(order);

            return result;
        }
        #endregion

    }
}