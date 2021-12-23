using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Helpers
{
    public class TowardMover : MonoBehaviour
    {

        public enum MoveObject { sprite, volume, image, uiText, lightRange, lrColor, lightIntensity };
        public MoveObject moveObject;
        public float moveToFloat;
        public float moveFromFloat;
        SpriteRenderer sr;
        AudioSource audio;
        Image image;
        Text uiText;
        Light light;
        LineRenderer lr;
        public float speed = 0.5f;
        public float alpha;
        bool destroyOnEnd = true;

        void Start()
        {
            delConcurents();
            alpha = moveFromFloat;
            if (moveObject == MoveObject.sprite) { sr = GetComponent<SpriteRenderer>(); }
            if (moveObject == MoveObject.volume) { audio = GetComponent<AudioSource>(); }
            if (moveObject == MoveObject.image) { image = GetComponent<Image>(); }
            if (moveObject == MoveObject.uiText) { uiText = GetComponent<Text>(); }
            if (moveObject == MoveObject.lightRange) { light = GetComponent<Light>(); }
            if (moveObject == MoveObject.lightIntensity) { light = GetComponent<Light>(); }
            if (moveObject == MoveObject.lrColor) { lr = GetComponent<LineRenderer>(); }

        }

        // Update is called once per frame
        void Update()
        {
            if (moveObject == MoveObject.sprite)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                if (alpha == moveToFloat)
                {

                    Destroy(this);
                }
            }
            if (moveObject == MoveObject.image)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                if (alpha == moveToFloat)
                {

                    if (destroyOnEnd) Destroy(gameObject);
                    else Destroy(this);
                }
            }
            if (moveObject == MoveObject.uiText)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, alpha);
                if (alpha == moveToFloat)
                {

                    if (destroyOnEnd) Destroy(gameObject);
                    else Destroy(this);
                }
            }
            if (moveObject == MoveObject.volume)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                audio.volume = alpha;
                if (alpha == moveToFloat)
                {

                    Destroy(this);
                }
            }
            if (moveObject == MoveObject.lightRange)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                light.range = alpha;
                if (alpha == moveToFloat)
                {

                    if (destroyOnEnd) Destroy(gameObject);
                    else Destroy(this);
                }
            }
            if (moveObject == MoveObject.lightIntensity)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                light.intensity = alpha;
                if (alpha == moveToFloat)
                {

                    if (destroyOnEnd) Destroy(gameObject);
                    else Destroy(this);
                }
            }
            if (moveObject == MoveObject.lrColor)
            {
                alpha = Mathf.MoveTowards(alpha, moveToFloat, speed * Time.deltaTime);
                lr.SetColors(new Color(1, 1, 1, alpha), new Color(1, 1, 1, alpha));
                // lr.startWidth = alpha;
                //  lr.endWidth = alpha;
                if (alpha == moveToFloat)
                {

                    if (destroyOnEnd) Destroy(gameObject);
                    else Destroy(this);

                }
            }
        }
        void delConcurents()
        {
            TowardMover[] tw = gameObject.GetComponents<TowardMover>();
            foreach (var t in tw)
            {
                if (t != this) Destroy(t);
            }
        }
        public void setParams(MoveObject mo, float from, float to, float speed, bool destroyEnd = true)
        {
            moveToFloat = to;
            moveFromFloat = from;
            this.speed = speed;
            moveObject = mo;
            destroyOnEnd = destroyEnd;
        }

    }
}