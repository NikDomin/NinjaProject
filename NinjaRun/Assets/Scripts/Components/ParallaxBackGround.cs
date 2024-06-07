using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ParallaxBackGround : MonoBehaviour
    {
        private float distance, temp, length, startPos;
        private Camera mainCamera;
        [SerializeField] private float parallaxEffect;

        private void Awake()
        {
            mainCamera = Camera.main;
        }
        // Use this for initialization
        void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            temp = (mainCamera.transform.position.x * (1 - parallaxEffect));

            distance = (mainCamera.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startPos + distance, 
                transform.position.y, transform.position.z);

            if (temp > startPos+ length)
            {
                startPos += length;
            }
            else if (temp<startPos - length)
            {
                startPos -= length;
            }
        }
    }
}