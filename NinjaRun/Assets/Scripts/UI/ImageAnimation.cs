using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite[] frames;
        [SerializeField] private float framesPerSecond = 10f;
        private Image image;
        private int frameIndex;
        
        // int currentFrame = 0;
        //
        float frameTime;
        float frameTimer = 0;

        
        private void Awake()
        {
            image = GetComponent<Image>();
            // StartCoroutine(Animate());
        }

        private void OnEnable()
        {
            // StartCoroutine(Animate());
            frameIndex = 0;
            frameTime = 1 / framesPerSecond;
            image.overrideSprite = frames[0];
        }

        // private void OnDisable()
        // {
        //     // StopCoroutine(Animate());
        // }
        
        // private void OnEnable()
        // {
        //     frameTime = 1 / framesPerSecond;
        //     image.sprite = frames[0];
        //     currentFrame = 0;
        //     frameTimer = 0;
        // }

        // private void Update()
        // {
        //     if (frameTimer < frameTime)
        //         frameTimer += Time.deltaTime;
        //     else
        //     {
        //         image.overrideSprite = frames[currentFrame];
        //         Debug.Log(currentFrame);
        //         currentFrame = (currentFrame + 1) % (frames.Length);
        //         frameTimer = 0;
        //     }
        // }


        // private void FixedUpdate()
        // {
        //     int frame = (int)(Time.time * framesPerSecond);
        //
        //     // loop
        //     frame = frame % frames.Length;
        //
        //     // set sprite
        //     image.sprite = frames[frame];
        // }

        private void FixedUpdate()
        {
            if (frameTimer < frameTime)
            {
                frameTimer += Time.deltaTime;
                return;                
            }

            frameTimer = 0;
            
            if (frameIndex > frames.Length)
            {
                frameIndex = 0;
            }
            Debug.Log(frameIndex);
            image.overrideSprite = frames[frameIndex];
            frameIndex++;
        }

        // IEnumerator Animate()
        // {
        //     int frameIndex = 0;
        //     while (true)
        //     {
        //         if (frameIndex > frames.Length)
        //         {
        //             frameIndex = 0;
        //         }
        //         Debug.Log(frameIndex);
        //         image.overrideSprite = frames[frameIndex];
        //         frameIndex++;
        //         yield return new WaitForSeconds(1.0f / framesPerSecond);
        //     }
        //     // while (true)
        //     // {
        //     //     for (int i = 0; i < frames.Length; i++)
        //     //     {
        //     //         image.overrideSprite = frames[i];
        //     //         Debug.Log(i);
        //     //         yield return new WaitForSeconds(1.0f / framesPerSecond);
        //     //         // frameIndex = (frameIndex + 1) % frames.Length;
        //     //         
        //     //     }
        //     //     
        //     // }
        // }
    }
}