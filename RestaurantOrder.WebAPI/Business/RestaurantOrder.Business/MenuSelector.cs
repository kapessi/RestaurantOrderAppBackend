using RestaurantOrder.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Business
{
    /// <summary>
    /// Menu selector helper.
    /// </summary>
    public class MenuSelector
    {

        #region Public methods
        /// <summary>
        /// Executes the select of a menu.
        /// </summary>
        /// <param name="dishTypes">The slection list.</param>
        /// <param name="menuResult">The list to filter.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> with the results.</returns>
        public IEnumerable<Menu> SelectMenu(IEnumerable<int> dishTypes, IEnumerable<Menu> menuResult)
        {
            var order = new List<Menu>();
            foreach (var dishTypeId in dishTypes)
            {
                Menu menu;
                if (menuResult.Any(m => m.DishTypeId == dishTypeId))
                    menu = menuResult.First(m => m.DishTypeId == dishTypeId);
                else
                    menu = new Menu { DishTypeId = dishTypeId, AllowMultipleOrders = false, Dish = "error" };

                if (menu.AllowMultipleOrders || !order.Any(o => o.DishTypeId == menu.DishTypeId))
                    order.Add(menu);
            }
            return order;
        }
        #endregion

    }
}