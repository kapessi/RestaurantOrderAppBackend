using RestaurantOrder.Entities;
using System.Collections.Generic;

namespace RestaurantOrder.Business.Contracts
{
    /// <summary>
    /// Specififes the access to orders data.
    /// </summary>
    public interface IOrderData
    {

        #region Public methods
        /// <summary>
        /// Gets the menu with the specified filter.
        /// </summary>
        /// <param name="dayTime">The day time.</param>
        /// <param name="dishType">The dishes.</param>
        /// <returns>Returns a <see cref="List{T}"/> with the results.</returns>
        IEnumerable<Menu> GetMenu(string dayTime, IEnumerable<int> dishType);
        #endregion

    }
}