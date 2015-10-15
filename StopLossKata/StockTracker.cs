using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StopLossKata
{
    class StockTracker
    {
        private Broker broker;
        private bool priceDecreased;
        private DateTime priceChangedAt;
        private int initialPrice;

        public StockTracker(Broker broker)
        {
            this.broker = broker;
        }

        public void Buy(int price, DateTime time)
        {
            initialPrice = price;
        }

        public void PriceChanged(int price, DateTime time)
        {
            priceDecreased = (price < initialPrice);
            priceChangedAt = time;
        }

        public void Refresh(DateTime time)
        {
            if (!priceDecreased) return;
            if ((time - priceChangedAt).Seconds >= 30)
                broker.Sell();
        }
    }
}
