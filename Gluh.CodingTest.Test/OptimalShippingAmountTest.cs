﻿using Gluh.CodingTest.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Gluh.CodingTest.Test
{
    public class OptimalShippingAmountTest
    {
        private readonly ShippingCalculator _calculator;
        
        public OptimalShippingAmountTest()
        {
            _calculator = new ShippingCalculator();
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

        #region Price is $1
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

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

            // Expect ship 5kg with ShippingWeightRate $10/kg
            var expectedShippingAmmount = _calculator.CalculateWeightBasedOptimalShippingAmount(order).Item2;

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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsOneWeightIsThirtyThreeKg()
        {
            var price = 1m;
            var weight = 33m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsOneWeightIsThirtySixKg()
        {
            var price = 1m;
            var weight = 36m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
        #endregion

        #region Price is $60
        [Fact]
        public void TestPriceIsSixtyWeightIsFiveKg()
        {
            var price = 60m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsSixtyWeightIsFivePointOneKg()
        {
            var price = 60m;
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

            // Expect using ShippingWeightRate
            var expectedShippingAmmount = _calculator.CalculateWeightBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsSixtyWeightIsTenKg()
        {
            var price = 60m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsSixtyWeightIsThirtyThreeKg()
        {
            var price = 60m;
            var weight = 33m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsSixtyWeightIsThirtySixKg()
        {
            var price = 60m;
            var weight = 36m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
        #endregion

        #region Price is $150
        [Fact]
        public void TestPriceIsAHundredAndFiftyWeightIsFiveKg()
        {
            var price = 150m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAHundredAndFiftyWeightIsFivePointOneKg()
        {
            var price = 150m;
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

            // Expect using ShippingPriceRate
            var expectedShippingAmmount = _calculator.CalculatePriceBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAHundredAndFiftyWeightIsTenKg()
        {
            var price = 150m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAHundredAndFiftyWeightIsThirtyThreeKg()
        {
            var price = 150m;
            var weight = 33m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAHundredAndFiftyWeightIsThirtySixKg()
        {
            var price = 150m;
            var weight = 36m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
        #endregion

        #region Price is $800
        [Fact]
        public void TestPriceIsEightHundredWeightIsFiveKg()
        {
            var price = 800m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsEightHundredWeightIsFivePointOneKg()
        {
            var price = 800m;
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

            // Expect using ShippingWeightRate
            var expectedShippingAmmount = _calculator.CalculateWeightBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsEightHundredWeightIsTenKg()
        {
            var price = 800m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsEightHundredWeightIsThirtyThreeKg()
        {
            var price = 800m;
            var weight = 33m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsEightHundredWeightIsThirtySixKg()
        {
            var price = 800m;
            var weight = 36m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
        #endregion

        #region Price is $1000
        [Fact]
        public void TestPriceIsAThousandWeightIsFiveKg()
        {
            var price = 1000m;
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

            // Expect using ShippingPriceRate
            var expectedShippingAmmount = _calculator.CalculatePriceBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAThousandWeightIsFivePointOneKg()
        {
            var price = 1000m;
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

            // Expect using ShippingPriceRate
            var expectedShippingAmmount = _calculator.CalculatePriceBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAThousandWeightIsTenKg()
        {
            var price = 1000m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAThousandWeightIsThirtyThreeKg()
        {
            var price = 1000m;
            var weight = 33m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }

        [Fact]
        public void TestPriceIsAThousandWeightIsThirtySixKg()
        {
            var price = 1000m;
            var weight = 36m;
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

            // Expect using ShippingAPIRate
            var expectedShippingAmmount = _calculator.CalculateThridPartyBasedOptimalShippingAmount(order).Item2;

            // Assert
            Assert.Equal(expectedShippingAmmount, shippingAmount);
        }
        #endregion
    }
}
