using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantOrder.Entities;
using RestaurantOrder.WebAPI.Factory;
using System;
using System.Collections.Generic;

namespace RestaurantOrder.WebAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        #region Fields
        private IConfiguration configuration;
        private readonly ILogger<OrderController> logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="OrderController"/>
        /// </summary>
        /// <param name="configuration">The current environment configuration.</param>
        /// <param name="logger">The logger to work.</param>
        public OrderController(IConfiguration configuration, ILogger<OrderController> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Process the get order operation.
        /// </summary>
        /// <param name="request">The request body to process.</param>
        /// <returns>Returns a <see cref="List{T}"/> with the results.</returns>
        [HttpGet]
        [Route("get")]
        public IEnumerable<string> Get([FromBody] OrderRequest request)
        {
            try
            {
                var factory = new OrderFactory();
                var business = factory.Create(configuration);
                var result = business.ProcessOrder(request);
                logger.LogInformation("order/get", request);
                return result;
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, request);
                return new List<string> { exception.Message };
            }
        }
        #endregion

    }
}