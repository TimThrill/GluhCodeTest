using System;
using System.Linq;
using System.Collections.Generic;
using Gluh.CodingTest.Database;
using System.IO;

namespace Gluh.CodingTest
{
    /// <summary>
    /// Assumption
    /// 1. An order can be split into any portion as required
    /// 2. When shipping weight is between [10kg, 30kg], the method to calculate API rate is: ShippingApiClient.GetRate(weight) + 5
    /// 3. When shipping weight is between [30kg, 35kg], the method to calculate API rate is: ShippingApiClient.GetRate(weight) * 1.075
    /// </summary>
    public class ShippingCalculator
    {
        private List<ShippingPriceRate> _priceRates;
        private List<ShippingWeightRate> _weightRates;
        private List<ShippingAPIRate> _apiRates;
        private ShippingApiClient _client;

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

            _client = new ShippingApiClient();
        }

        public decimal Calculate(SalesOrder salesOrder)
        {
            var priceOptimalSolution = CalculatePriceBasedOptimalShippingAmount(salesOrder);
            var weightOptimalSolution = CalculateWeightBasedOptimalShippingAmount(salesOrder);
            var apiOptimalSolution = CalculateThridPartyBasedOptimalShippingAmount(salesOrder);
            var optimalShippingCost = 0m;
            var optimalShippingWeight = 0m;

            if(priceOptimalSolution.Item2 >= weightOptimalSolution.Item2)
            {
                optimalShippingWeight = priceOptimalSolution.Item1;
                optimalShippingCost = priceOptimalSolution.Item2;
            }
            else
            {
                optimalShippingWeight = weightOptimalSolution.Item1;
                optimalShippingCost = weightOptimalSolution.Item2;
            }

            if(optimalShippingCost < apiOptimalSolution.Item2)
            {
                optimalShippingWeight = apiOptimalSolution.Item1;
                optimalShippingCost = apiOptimalSolution.Item2;
            }

            return optimalShippingWeight;
        }

        /// <summary>
        /// Given an order, calculate the best shipping amount according to ShippingWeightRate rule
        /// </summary>
        /// <param name="order"></param>
        /// <returns>The shipping amount sent and total shipping cost</returns>
        public Tuple<decimal, decimal> CalculateWeightBasedOptimalShippingAmount(SalesOrder order)
        {
            var shippingCost = 0m;
            var shippingWeight = order.GetOrderShippingWeight();

            if(shippingWeight >= 0 && shippingWeight <= 5)
            {
                shippingCost = shippingWeight * 10;
            }
            else if(shippingWeight > 5 && shippingWeight <= 50/7)
            {
                // If weight is between 5kg and 7kg, shipping 5kg with $5 is the optimum option
                shippingCost = 50m;
                shippingWeight = 5m;
            }
            else if(shippingWeight > 50/7 && shippingWeight < 10)
            {
                shippingCost = shippingWeight * 7;
            }
            else
            {
                shippingCost = shippingWeight * 20;
            }

            return new Tuple<decimal, decimal>(shippingWeight, shippingCost);
        }

        /// <summary>
        /// Given an order, calculate the best shipping amount according to ShippingAPIRate rule
        /// </summary>
        /// <param name="order"></param>
        /// <returns>The shipping amount sent and total shipping cost</returns>
        public Tuple<decimal, decimal> CalculateThridPartyBasedOptimalShippingAmount(SalesOrder order)
        {
            var shippingCost = 0m;
            var shippingWeight = order.GetOrderShippingWeight();

            if(shippingWeight >= 0 && shippingWeight < 10)
            {
                shippingCost = shippingWeight * _client.GetRate(0, 0, shippingWeight);
            }
            else if(shippingWeight >= 10 && shippingWeight <= 30)
            {
                shippingCost = shippingWeight * (_client.GetRate(0, 0, shippingWeight) + 5);
            }
            else if(shippingWeight > 30 && shippingWeight <= 35)
            {
                shippingCost = shippingWeight * (_client.GetRate(0, 0, shippingWeight) * 1.075m);
            }
            else if(shippingWeight > 35)
            {
                shippingCost = shippingWeight * _client.GetRate(0, 0, shippingWeight);
            }

            return new Tuple<decimal, decimal>(shippingWeight, shippingCost);
        }

        /// <summary>
        /// Given an order, calculate the best shipping amount according to ShippingPriceRate rule
        /// </summary>
        /// <param name="order"></param>
        /// <returns>The shipping amount sent and total shipping cost</returns>
        public Tuple<decimal, decimal> CalculatePriceBasedOptimalShippingAmount(SalesOrder order)
        {
            var totalPrice = order.GetOrderPrice();

            // From the ShippingPriceRate rule, we can see that we should send as much amount as possible

            var shippingCost = 0m;
            var shippingWeight = order.GetOrderShippingWeight();
            if(totalPrice >= 0 && totalPrice <= 50)
            {
                shippingCost = shippingWeight * 5.5m;
            }
            else if(totalPrice > 50 && totalPrice < 100)
            {
                // As shipping rate between $50 and $100 is zero, the optimised shipping amount is max at 50kg
                shippingCost = 50 * 5.5m;
                shippingWeight = 50;
            }
            else if(totalPrice >= 100 && totalPrice <= 500)
            {
                shippingCost = shippingWeight * 10m;
            }
            else if(totalPrice > 500 && totalPrice < 1000)
            {
                shippingCost = 500 * 10m;
                shippingWeight = 500;
            }
            else if(totalPrice >= 1000)
            {
                shippingCost = shippingWeight * 15m;
            }

            return Tuple.Create(shippingWeight, shippingCost);
        }
    }
}
