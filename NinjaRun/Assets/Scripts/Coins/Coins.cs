using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Coins
{
    public class Coins : MonoBehaviour
    {
        public UnityEvent OnPickUpCoins;
        
        [SerializeField] private int coinsToAdd;

        public void AddCoins()
        {
            CoinsHandler.coinsHandler.AddCoins(coinsToAdd);
            OnPickUpCoins?.Invoke();
        }
    }
}