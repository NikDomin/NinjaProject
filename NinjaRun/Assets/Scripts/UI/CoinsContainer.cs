using System;
using Coins;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinsContainer : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private CoinsHandler coinsHandler;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        
        private void Start()
        {
            coinsHandler = CoinsHandler.Instance;
            coinsHandler.OnChangeCoinsCount += PrintText;
            text.text = coinsHandler.CurrentCoins.ToString();
        }

        private void OnDisable()
        {
            coinsHandler.OnChangeCoinsCount -= PrintText;
        }

        private void PrintText()
        {
            text.text = coinsHandler.CurrentCoins.ToString();
        }
    }
}