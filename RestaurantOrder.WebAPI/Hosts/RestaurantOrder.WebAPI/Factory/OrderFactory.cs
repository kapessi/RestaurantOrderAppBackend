using Microsoft.Extensions.Configuration;
using RestaurantOrder.Business;
using RestaurantOrder.Data;

namespace RestaurantOrder.WebAPI.Factory
{
    /// <summary>
    /// The abstract orders factory.
    /// </summary>
    public class OrderFactory
    {

        #region Public methods
        /// <summary>
        /// Creates the order business handler.
        /// </summary>
        /// <param name="configuration">The current environment configuration.</param>
        /// <returns>Returns the instanced order handler.</returns>
        public OrderBusiness Create(IConfiguration configuration)
        {
            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            var orderData = new OrderData(connectionString);
            return new OrderBusiness(orderData);
        }
        #endregion

    }
}