using UnityEngine;

namespace Coins
{
    public class Coins : MonoBehaviour
    {
        [SerializeField] private int coinsToAdd;

        public void AddCoins()
        {
            CoinsHandler.coinsHandler.AddCoins(coinsToAdd);
        }
    }
}