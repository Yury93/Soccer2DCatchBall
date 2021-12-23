using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{

    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] float shakeTimeStap;
        public static CameraShaker instance;
        Transform myTransform;
        bool shakeProcess;
        private void Awake()
        {
            instance = this;
            myTransform = GetComponent<Transform>();
        }
     
        public void Shake(float time = 2f, float force = 8f)
        {
            StartCoroutine(shake(time, force));

        }

        IEnumerator shake(float time, float force)
        {
            float t = time;
            float z = myTransform.localEulerAngles.z;
            float s = shakeTimeStap;
            while (t > 0)
            {
                float zNew = z + Random.Range(-force, force);

                myTransform.localEulerAngles = new Vector3(myTransform.localEulerAngles.x, myTransform.localEulerAngles.y, zNew);
                t -= shakeTimeStap;
                yield return new WaitForSecondsRealtime(s);
                s *= 0.9f;
                force *= 0.9f;
                myTransform.localEulerAngles = new Vector3(myTransform.localEulerAngles.x, myTransform.localEulerAngles.y, 0);
            }



        }
    }
}