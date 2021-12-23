using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{

    public class TimeScaler : MonoBehaviour
    {
        public static TimeScaler instance;
        Coroutine slowCor;
        public bool IsPause { get; private set; }
        /// <summary>
        /// фактор таймскейла оригинального
        /// </summary>
        public float TimeScaleEffect { get; private set; }
        float lastTimeScaleEffect;
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            IsPause = false;
            Time.timeScale = 1;
            TimeScaleEffect = 1;
        }
        private void Update()
        {
            if (IsPause)
            {
                Time.timeScale = 0.0001f;
            }
            else Time.timeScale = TimeScaleEffect;


        }
        public void PauseStart()
        {
            if (IsPause) return;
            IsPause = true;
        }
        public void PauseStop()
        {
            if (!IsPause) return;
            IsPause = false;

        }

        IEnumerator slowProcess(float waitAfterComplete = 1, float end = 0.2f, float changeSpeed = 1)
        {
            //isProcess = true;
            while (TimeScaleEffect != end)
            {
                if (!IsPause) TimeScaleEffect = Mathf.MoveTowards(TimeScaleEffect, end, changeSpeed * Time.unscaledDeltaTime);

                yield return null;
            }

            yield return new WaitForSecondsRealtime(1);

            while (TimeScaleEffect != 1)
            {
                if (!IsPause) TimeScaleEffect = Mathf.MoveTowards(TimeScaleEffect, 1, changeSpeed * Time.unscaledDeltaTime);
                yield return null;
            }
            slowCor = null;
        }
        /// <summary>
        /// замедляет время и через секунду восстанавливает
        /// </summary>
        /// <param name="time"></param>
        /// <param name="start"></param>
        /// <param name="scalePoint"></param>
        /// <param name="changeSpeed"></param>
        public void CreateSlowEffect(float waitAfterComplete, float scalePoint = 0.2f, float changeSpeed = 1)
        {
            if (slowCor == null) slowCor = StartCoroutine(slowProcess(waitAfterComplete, scalePoint, changeSpeed));
        }
        /// <summary>
        /// устанавливает тайм скейл в позицию таргет.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public void SetTimeScale(float target, float speed = 10)
        {
            
            if (slowCor == null) slowCor = StartCoroutine(setScale(target, speed));
            else
            {
                StopCoroutine(slowCor);
                slowCor = StartCoroutine(setScale(target, speed));

            }
        }
        IEnumerator setScale(float target, float speed)
        {
            while (TimeScaleEffect != target)
            {
                if (!IsPause) TimeScaleEffect = Mathf.MoveTowards(TimeScaleEffect, target, speed * Time.unscaledDeltaTime);

                yield return null;
            }
            slowCor = null;
        }

    }
}
