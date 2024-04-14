using System;
using System.Collections;
using ObjectsPool;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class PopupText : MonoBehaviour
    {
        public static PopupText Instance;

        [SerializeField] private GameObject canvas;
        
        private GameObjectPool TextCanvasPool;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            TextCanvasPool = new GameObjectPool(canvas, 5);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            TextCanvasPool.ReturnAll();
        }

        public void GetTextCanvas(string text, Vector2 position)
        {
            var canvasObject = TextCanvasPool.Get();
            canvasObject.transform.position = position;
            TextMeshProUGUI textMeshPro;
            try
            {
                textMeshPro = canvasObject.GetComponentInChildren<TextMeshProUGUI>();
                textMeshPro.text = text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            StartCoroutine(ReturnTextRoutine(canvasObject));
        }

        private IEnumerator ReturnTextRoutine(GameObject canvasObject)
        {
            yield return new WaitForSeconds(0.5f);
            TextCanvasPool.Return(canvasObject);
        }
        
    }
}