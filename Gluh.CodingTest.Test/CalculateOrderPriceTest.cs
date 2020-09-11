using Gluh.CodingTest.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Gluh.CodingTest.Test
{
    public class CalculateOrderPriceTest
    {
        [Fact]
        public void CalculateOrderPriceWithNullOrderLine_ReturnZero()
        {
            var order = new SalesOrder();

            // Act
            var price = order.GetOrderPrice();

            // Assert
            Assert.Equal(0, price);
        }

        [Fact]
        public void CalculateOrderPrice_ReturnCorrectPrice()
        {
            var order = new SalesOrder
            {
                Lines = new List<SalesOrderLine>
                {
                    new SalesOrderLine
                    {
                        Price = 10
                    },
                    new SalesOrderLine
                    {
                        Price = 20
                    }
                }
            };

            // Act
            var price = order.GetOrderPrice();

            // Assert
            Assert.Equal(30, price);
        }
    }
}
