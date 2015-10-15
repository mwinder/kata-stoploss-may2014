using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StopLossKata
{
    public class StopLoss : Broker
    {
        bool stockSold;
        StockTracker stockTracker;

        public StopLoss()
        {
            stockTracker = new StockTracker(this);
        }

        public void Sell()
        {
            stockSold = true;
        }

        [Fact]
        public void StockSoldIfPriceFallsToThresholdForMoreThan30Seconds()
        {
            stockTracker.Buy(10, DateTime.Now);
            stockTracker.PriceChanged(9, DateTime.Now);

            stockTracker.Refresh(DateTime.Now.AddSeconds(30));

            Assert.True(stockSold, "Stock was not sold");
        }

        [Fact]
        public void StockNotSoldIfPriceDoesNotFall()
        {
            stockTracker.Buy(10, DateTime.Now);

            stockTracker.Refresh(DateTime.Now.AddSeconds(30));

            Assert.False(stockSold, "Stock was sold");
        }

        [Fact]
        public void StockNotSoldIfPriceFallsForLessThan30Seconds()
        {
            stockTracker.Buy(10, DateTime.Now);
            stockTracker.PriceChanged(9, DateTime.Now);

            stockTracker.Refresh(DateTime.Now.AddSeconds(25));

            Assert.False(stockSold, "Stock was sold");
        }

        [Fact]
        public void StockNotSoldIfIncreasesFor30Seconds()
        {
            stockTracker.Buy(10, DateTime.Now);
            stockTracker.PriceChanged(11, DateTime.Now);

            stockTracker.Refresh(DateTime.Now.AddSeconds(30));

            Assert.False(stockSold, "Stock was sold");
        }
    }
}
