using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.UI;
namespace Samples
{


    public class OptionPanel : MonoBehaviour
    {
        [Header("Drug & drop")]
        [SerializeField] Slider soundSlider;
        [SerializeField] Slider musicSlider;
        [SerializeField] Toggle pitchCheck;

        void Start()
        {

        }

        void Update()
        {

        }

        public void Open()
        {
            setSlidersFromSettings();
            gameObject.SetActive(true);
        }
        void Close()
        {
            gameObject.SetActive(false);
        }
        public void SaveAndClose()
        {
            Helpers.AudioSettings.settings.SaveSettings();
            Close();
        }
        public void CloseWithoutSave()
        {
            returnSettings();
            Close();
        }
        void returnSettings()
        {
            
            musicSlider.value = PlayerPrefs.GetFloat("AudioSettings.music");
            soundSlider.value = PlayerPrefs.GetFloat("AudioSettings.soundVolume");

            string bol = PlayerPrefs.GetString("AudioSettings.ignorePitchEffects");
            pitchCheck.isOn = bol == "True" ? true : false;
          

            Helpers.AudioSettings.settings.music = musicSlider.value;
            Helpers.AudioSettings.settings.soundVolume = soundSlider.value;
            Helpers.AudioSettings.settings.ignorePitchEffects = pitchCheck.isOn;
        }
       
        void setSlidersFromSettings()
        {
            musicSlider.value = Helpers.AudioSettings.settings.music;
            soundSlider.value = Helpers.AudioSettings.settings.soundVolume;


            pitchCheck.isOn = Helpers.AudioSettings.settings.ignorePitchEffects;

        }

        public void OnMusicSlider()
        {
            Helpers.AudioSettings.settings.music = musicSlider.value;
        }
        public void OnSoundSlider()
        {
            Helpers.AudioSettings.settings.soundVolume = soundSlider.value;
        }
        public void OnPitchCkekBox()
        {
            Helpers.AudioSettings.settings.ignorePitchEffects = pitchCheck.isOn;
        }
    }
}