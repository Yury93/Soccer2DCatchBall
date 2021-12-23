using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Helpers
{


    public class LoadScreen : MonoBehaviour
    {
        [TextArea(5,5)]
        [SerializeField] List<string> tips;
        [Tooltip("�� ������� ����� Image , ������� �������� ����� ��������")]
        [SerializeField] GameObject LoacPanelBackGround;
        [Tooltip("Text ������� ���������� �� ����������� ������")]
        [SerializeField] Text tipText;
        [Tooltip("�������� ����������")]
        [SerializeField] float fadeSpeed;
        public static LoadScreen instance;
        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            LoacPanelBackGround.SetActive(false);
            Image im = LoacPanelBackGround.GetComponent<Image>();
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 0);
            im.color = new Color(im.color.r, im.color.g, im.color.b, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Open()
        {
            StartCoroutine(open());
        }
        IEnumerator open()
        {
            if (tips.Count > 0) tipText.text = tips[Random.Range(0, tips.Count)];
            LoacPanelBackGround.SetActive(true);
            LoacPanelBackGround.AddComponent<TowardMover>().setParams(TowardMover.MoveObject.image, 0, 1, fadeSpeed * 2, false);
            yield return new WaitForSeconds(fadeSpeed / 2);
            tipText.gameObject.AddComponent<TowardMover>().setParams(TowardMover.MoveObject.uiText, 0, 1, fadeSpeed * 2, false);
            yield return new WaitForSeconds(fadeSpeed / 2);

        }
    }
}