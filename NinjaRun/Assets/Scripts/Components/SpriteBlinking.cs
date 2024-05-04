using System;
using UnityEngine;

namespace Components
{
    public class SpriteBlinking : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
    }
}