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
        public void TestWeightIsZeroKg()
        {
            var price = 1m;
            var weight = 0m;
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Price = price,
                        Product = new Product
                        {
                            Weight = weight
                        }
                    }
                }
            };

            // Act
            var shippingAmount = _calculator.Calculate(order);

            var expectedShippingAmmount = 0m;
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsOneWeightIsFiveKg()
        {
            var price = 1m;
            var weight = 5m;
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Price = price,
                        Product = new Product
                        {
                            Weight = weight
                        }
                    }
                }
            };

            // Act
            var shippingAmount = _calculator.Calculate(order);

            // Expect 7kg with API shipping rate
            var expectedShippingAmmount = 5m;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsOneWeightIsFivePointOneKg()
        {
            var price = 1m;
            var weight = 5.1m;
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Price = price,
                        Product = new Product
                        {
                            Weight = weight
                        }
                    }
                }
            };

            // Act
            var shippingAmount = _calculator.Calculate(order);

            // Expect 5kg with weight shipping rate
            var expectedShippingAmmount = 5m;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsOneWeightIsTenKg()
        {
            var price = 1m;
            var weight = 10m;
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Price = price,
                        Product = new Product
                        {
                            Weight = weight
                        }
                    }
                }
            };

            // Act
            var shippingAmount = _calculator.Calculate(order);

            // Expect 5kg with weight shipping rate
            var expectedShippingAmmount = 10m;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
    }
}
