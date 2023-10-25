using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class FPSMeter : MonoBehaviour
    {
        private float fps;
        [SerializeField]private TMPro.TextMeshProUGUI fpsText;


        // Use this for initialization
        private void Start()
        {
            InvokeRepeating(nameof(GetFPS),1,1 );
        }

        private void GetFPS()
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps.ToString();
        }
    }
}