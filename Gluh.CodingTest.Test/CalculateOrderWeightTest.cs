using Gluh.CodingTest.Database;
using System;
using System.Collections.Generic;
using Xunit;

namespace Gluh.CodingTest.Test
{
    public class CalculateOrderWeightTest
    {
        private readonly ShippingCalculator _calculator;

        public CalculateOrderWeightTest()
        {
            _calculator = new ShippingCalculator();
        }

        [Fact]
        public void CalculateOrderWeightWithNullOrder_ReturnZero()
        {
            // Act
            var weight = _calculator.GetOrderShippingWeight(null);

            // Assert
            Assert.Equal(0m, weight);
        }

        [Fact]
        public void CalculateOrderWeightWithNullProductList_ReturnZero()
        {
            var order = new SalesOrder();

            // Act
            var weight = _calculator.GetOrderShippingWeight(order);

            // Assert
            Assert.Equal(0m, weight);
        }

        [Fact]
        public void CalculateOrderWeight_ReturnCorrectWeight()
        {
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Product = new Product
                        {
                            Weight = 12m
                        }
                    },
                    new SalesOrderLine
                    {
                        Product = new Product
                        {
                            Weight = 18m
                        }
                    }
                }
            };

            // Act
            var weight = _calculator.GetOrderShippingWeight(order);

            // Assert
            Assert.Equal(30, weight);
        }
    }
}
