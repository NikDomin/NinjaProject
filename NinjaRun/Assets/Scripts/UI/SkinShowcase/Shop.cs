using System;
using Coins;

namespace UI.SkinShowcase
{
    public class Shop
    {
        public event Action OnSuccessPurchase;
        public event Action OnUnsuccessfulPurchase;
        
        private CoinsHandler coinsHandler;

        public Shop(CoinsHandler coinsHandler)
        {
            this.coinsHandler = coinsHandler;
        }
        public void TryBuy(int price)
        {
            if (coinsHandler.CurrentCoins >= price)
            {
                coinsHandler.RemoveCoins(price);
                OnSuccessPurchase?.Invoke();
            }
            else
            {
                OnUnsuccessfulPurchase?.Invoke();
            }
        }
    }
}