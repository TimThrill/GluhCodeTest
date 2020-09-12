using Gluh.CodingTest.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Gluh.CodingTest.Test
{
    public class OptimalShippingAmountTest
    {
        private readonly ShippingCalculator _calculator;
        private readonly ShippingApiClient _apiClient;
        
        public OptimalShippingAmountTest()
        {
            _calculator = new ShippingCalculator();
            _apiClient = new ShippingApiClient();
        }

        [Fact]
        public void TestWeightOverTenKg()
        {
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Price = 1000,
                        Product = new Product
                        {
                            Weight = 10m
                        }
                    }
                }
            };

            // Act
            var shippingAmount = _calculator.Calculate(order);

            var expectedShippingAmmount = 10m;
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
    }
}
