namespace RestaurantOrder.Entities
{
    /// <summary>
    /// Represents a menu item.
    /// </summary>
    public class Menu
    {

        #region Properties
        /// <summary>
        /// Gets or sets the dish type.
        /// </summary>
        public int DishTypeId { get; set; }
        /// <summary>
        /// Gets or sets the dish name.
        /// </summary>
        public string Dish { get; set; }
        /// <summary>
        /// Gets or sets the allow allow multiple orders flag.
        /// </summary>
        public bool AllowMultipleOrders { get; set; }
        #endregion

    }
}