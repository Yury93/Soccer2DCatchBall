using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{


    public class ScreenBloodEffect : MonoBehaviour
    {
        [SerializeField] Transform content;
        [SerializeField] List<GameObject> bloodPrefabs;

        [SerializeField] Vector2 randomPositionX, randomPositionY, randomScaleX, randomScaleY, randomRotation;
        [SerializeField] Vector2Int randomBloodCount;
        [SerializeField] float instanceRate = 0.1f, fadeSpeed = 1, fadeDelay = 1;
       
        public static ScreenBloodEffect instance;

        private void Awake()
        {
            instance = this;
        }

        public void CreateEffect()
        {
            StartCoroutine(creator());
        }
        IEnumerator creator()
        {
            int randomCount = Random.Range(randomBloodCount.x, randomBloodCount.y);
            for (int i = 0; i < randomCount; i++)
            {
                Vector3 position = new Vector3(Random.Range(randomPositionX.x, randomPositionX.y),
                    Random.Range(randomPositionY.x, randomPositionY.y),0);

                GameObject go = Instantiate(bloodPrefabs[Random.Range(0, bloodPrefabs.Count)], content);
                go.GetComponent<RectTransform>().anchoredPosition  =  position;
                go.GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,Random.Range(randomRotation.x,randomRotation.y));
                go.transform.localScale = new Vector3(Random.Range(randomScaleX.x, randomScaleX.y), Random.Range(randomScaleY.x, randomScaleY.y), 0);
                go.AddComponent<TowardMover>().setParams(TowardMover.MoveObject.image, 1, 0, 1, true);
                go.GetComponent<Image>().raycastTarget = false;



                yield return new WaitForSeconds(instanceRate);
            }
           
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CreateEffect();
            }
        }
    }
}