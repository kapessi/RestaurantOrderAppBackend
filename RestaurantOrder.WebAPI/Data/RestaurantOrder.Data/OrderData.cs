using RestaurantOrder.Business.Contracts;
using RestaurantOrder.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantOrder.Data
{
    /// <summary>
    /// Provides access to orders data.
    /// </summary>
    public class OrderData : IOrderData
    {

        #region Fields
        private readonly string connectionString;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="OrderData"/> class.
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderData(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the menu with the specified filter.
        /// </summary>
        /// <param name="dayTime">The day time.</param>
        /// <param name="dishType">The dishes.</param>
        /// <returns>Returns a <see cref="List{T}"/> with the results.</returns>
        public IEnumerable<Menu> GetMenu(string dayTime, IEnumerable<int> dishType)
        {
            List<Menu> result = new List<Menu>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[usp_Menu_Select]";
                    command.Connection = connection;

                    command.Parameters.AddWithValue("DayTimeName", dayTime);
                    command.Parameters.AddWithValue("DishTypeId", CreateTable(dishType));

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            result.Add(new Menu { DishTypeId = reader.GetInt32(0), Dish = reader.GetString(1), AllowMultipleOrders = reader.GetBoolean(2) });
                    }
                }
            }
            return result;
        }
        #endregion

        #region Private methods
        private DataTable CreateTable(IEnumerable<int> collection)
        {
            var table = new DataTable("DishTypeId");
            table.Columns.Add("Id", typeof(int));
            foreach (var item in collection)
            {
                var row = table.NewRow();
                row[0] = item;
                table.Rows.Add(row);
            }
            return table;
        }
        #endregion

    }
}