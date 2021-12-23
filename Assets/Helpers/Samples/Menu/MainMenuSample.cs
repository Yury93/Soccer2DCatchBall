using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

namespace Samples
{


    public class MainMenuSample : MonoBehaviour
    {
        [Header("StartOptions")]
        [SerializeField] bool enableLoadScreen;
        [SerializeField] float loadLevelDelay = 2;
        [SerializeField] int levelId = 1;

        [Header("Drug & Drop")]
        [SerializeField] OptionPanel optionPanel;
        //отции для выхода.



        bool starting;


        void Start()
        {
            Time.timeScale = 1;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void StartGame()
        {
            if (starting) return;
            starting = true;
            if (enableLoadScreen)
            {
                StartCoroutine(startWithTips());
            }
            else
            {
                Application.LoadLevel(levelId);
            }
        }
        IEnumerator startWithTips()
        {
            LoadScreen.instance.Open();
            yield return new WaitForSeconds(loadLevelDelay);
            Application.LoadLevel(levelId);

        }
        public void OpenOptions()
        {
            optionPanel.Open();
        }
        public void ExitButton()
        {
            Application.Quit();
        }

    }
}