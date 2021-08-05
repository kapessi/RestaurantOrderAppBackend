using RestaurantOrder.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Business
{
    /// <summary>
    /// Menu grouper helper.
    /// </summary>
    public class MenuGrouper
    {

        #region Public methods
        /// <summary>
        /// Groups an order.
        /// </summary>
        /// <param name="order">The order to group.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> with the results.</returns>
        public IEnumerable<string> GroupMenu(IEnumerable<Menu> order)
        {
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