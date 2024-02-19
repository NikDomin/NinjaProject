using UnityEngine;

namespace Coins
{
    public class CoinsHandler : MonoBehaviour
    {
        public static CoinsHandler coinsHandler;
        [field: SerializeField] public int CurrentCoins { get; private set; }

        public void AddCoins(int coins)
        {
            if (coins < 0)
            {
                Debug.LogError("you can't add a negative amount of coins");
                return;
            }
            
            CurrentCoins += coins;
        }

        public void RemoveCoins(int coins)
        {
            if (coins < 0)
            {
                Debug.LogError("you can't remove a negative amount of coins");
                return;
            }

            CurrentCoins -= coins;
        }
        
        private void Awake()
        {
            coinsHandler = this;
        }
        
        
    }
}