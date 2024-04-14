using Coins;
using UnityEngine;

namespace UI
{
    public class CoinsPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform textTransform;
        [SerializeField] private RectTransform imageTransform;


        private void Awake()
        {
            imageTransform.position = textTransform.position;
        }

        private void OnEnable()
        {
            CoinsHandler.Instance.OnChangeCoinsCount += SetImagePosition;
        }

        private void OnDisable()
        {
            CoinsHandler.Instance.OnChangeCoinsCount -= SetImagePosition;
        }
        [ContextMenu("SetImage")]
        private void SetImagePosition()
        {
            imageTransform.position = textTransform.position;
            imageTransform.position = new Vector3(textTransform.rect.width + 15, imageTransform.transform.position.y);
        }
    }
}