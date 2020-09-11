using System;
using System.Linq;
using System.Collections.Generic;
using Gluh.CodingTest.Database;
using System.IO;

namespace Gluh.CodingTest
{
    public class ShippingCalculator
    {

        private List<ShippingPriceRate> _priceRates;
        private List<ShippingWeightRate> _weightRates;
        private List<ShippingAPIRate> _apiRates;

        public ShippingCalculator()
        {
            _priceRates = new List<ShippingPriceRate>
            {
                new ShippingPriceRate { PriceMin = 0, PriceMax = 50, Rate = 5.50m },
                new ShippingPriceRate { PriceMin = 0, PriceMax = 100, Rate = 0.00m },
                new ShippingPriceRate { PriceMin = 100, PriceMax = 500, Rate = 10.00m },
                new ShippingPriceRate { PriceMin = 1000, PriceMax = null, Rate = 15.00m },
            };
            _weightRates = new List<ShippingWeightRate>
            {
                new ShippingWeightRate { WeighMin = 1, WeighMax = 5, Rate = 10.00m },
                new ShippingWeightRate { WeighMin = 5.01m, WeighMax = 10.00m, Rate = 7.00m },
                new ShippingWeightRate { WeighMin = 10.00m, WeighMax = null, Rate = 20.00m }
            };
            _apiRates = new List<ShippingAPIRate>
            {
                new ShippingAPIRate { WeighMin = 10, WeighMax = 30, RateAdjustmentPrice = 5m },
                new ShippingAPIRate { WeighMin = 30, WeighMax = 35, RateAdjustmentPercent = 7.5m },
                new ShippingAPIRate { WeighMin = 35, WeighMax = null }
            };
        }

        /// <summary>
        /// To get the optimal (the most expensive) shipping amount
        /// OrderShippingAmount = Rate * Weight
        /// As Weight is a constant, so that we need to find the highest 
        /// rate for this order via using PriceRate, WeightRate, or ApiRate
        /// </summary>
        /// <ShippingAPIRate>
        /// According to ShippingApiClient, there is a amplification factor for shipping weight,
        /// so that the actual rate for ShippingAPIRate can be following numbers
        /// Actual weight: [0, 4),                      rate:   n/a
        /// Actual weight: [4, 5],                      rate:   5 * 2.5 = 12.25
        /// Actual weight: (5, 10/1.5=20/3),            rate:   n/a
        /// Actual weight: [20/3, 10],                  rate:   5 * 1.5 = 7.5
        /// Actual weight: (10, 20],                    rate:   5 * 1.25 = 6.25
        /// Actual weight: (20, 30/1.15=600/23),        rate:   5 * 1.15 = 5.75
        /// Actual weight: [600/23, 35/1.15=700/23],    rate:   7.5 * 1.15 = 8.625
        /// Actual weight: (700/23, max),               rate:   n/a
        /// </ShippingAPIRate>
        public decimal Calculate(SalesOrder salesOrder)
        {
            var price = salesOrder.GetOrderPrice();
            var weight = salesOrder.GetOrderShippingWeight();
            decimal optimalAmount = 0m;
            // Combined ShippingAPIRate chart above with ShippingWeightRate
            // We can conclude the most expensive rate based on weight can following
            // [0, 4)       =>  10      (ShippingWeightRate)
            // [4, 5]       =>  12.25   (ShippingAPIRate)
            // (5, 20/3)    =>  7       (ShippingWeightRate)
            // [20/3, 10)   =>  7.5     (ShippingAPIRate)
            // [10, max)    =>  20      (ShippingWeightRate)

            if(price < 0 || weight < 0)
            {
                throw new InvalidDataException("Price or weight cannot be less than zero");
            }

            // Combined above chart with ShippingPriceRate
            // We can conclude that
            if(price >= 100 && price <= 500 && weight > 5 && weight < 10)
            {
                // Apply ShippingPriceRate 10
                optimalAmount = weight * 10m;
            }
            else if(price >= 1000 && weight < 10)
            {
                // Apply ShippingPriceRate 15
                optimalAmount = weight * 15m;
            }
            else if( weight >= 0 && weight < 4)
            {
                optimalAmount = weight * 10m;
            }
            else if( weight >= 4 && weight <= 5)
            {
                optimalAmount = weight * 12.25m;
            }
            else if( weight > 5 && weight < 20/3)
            {
                optimalAmount = weight * 7m;
            }
            else if(weight >= 20/3 && weight < 10)
            {
                optimalAmount = weight * 7.5m;
            } 
            else
            {
                optimalAmount = weight * 20m;
            }

            return optimalAmount;
        }

    }
}
