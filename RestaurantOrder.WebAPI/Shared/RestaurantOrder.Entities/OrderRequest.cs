using System.Collections.Generic;

namespace RestaurantOrder.Entities
{
    /// <summary>
    /// Represents an order request.
    /// </summary>
    public class OrderRequest
    {

        #region Properties
        /// <summary>
        /// Gets or sets the day time.
        /// </summary>
        public string DayTime { get; set; }
        /// <summary>
        /// Gets or sets the dishes types to query.
        /// </summary>
        public IEnumerable<int> DishType { get; set; }
        #endregion

    }
}