using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Coins
{
    public class Coins : MonoBehaviour
    {
        public UnityEvent OnPickUpCoins;
        
        [SerializeField] private int coinsToAdd;

        public void AddCoins()
        {
            CoinsHandler.Instance.AddCoins(coinsToAdd);
            PopupText.Instance.GetTextCanvas("+" + $" {coinsToAdd}", transform.position);
            OnPickUpCoins?.Invoke();
        }

    }
}