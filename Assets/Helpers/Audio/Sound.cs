using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{

    [RequireComponent(typeof(AudioSource))]
    public class Sound : MonoBehaviour
    {
        [SerializeField] bool getStartVolumeFromInspector = false;
        public enum SoundType { music, shoot, spell, hit, ui, areaFon, weather, voice };
        [SerializeField] public SoundType type;
        [SerializeField] float currentClipVolume;

        public bool isLoop;
        public bool destroyOnFinish;
        public bool playOnStart = true;
        public bool ignoreVolumeEffect = false;
        AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
            if (getStartVolumeFromInspector) currentClipVolume = source.volume;

        }
        void Start()
        {
            Helpers.AudioSettings.settings.onChangeVolume += changeValume;
            Helpers.AudioSettings.settings.onChangePlaySpeed += changePlaySpeed;
            changeValume();
            if (playOnStart) source.Play();

        }
        private void OnLevelWasLoaded(int level)
        {
            Helpers.AudioSettings.settings.onChangeVolume += changeValume;
            Helpers.AudioSettings.settings.onChangePlaySpeed += changePlaySpeed;
        }

        void Update()
        {
            if (!source.isPlaying && !isLoop && destroyOnFinish)
            {
                DestroyMe();
            }
        }
        public void DestroyMe()
        {
            Helpers.AudioSettings.settings.onChangeVolume -= changeValume;
            Helpers.AudioSettings.settings.onChangePlaySpeed -= changePlaySpeed;
            Destroy(gameObject);
        }
        void changePlaySpeed()
        {
            //  Debug.Log("chande speed efect");
            if (!ignoreVolumeEffect) source.pitch = Helpers.AudioSettings.settings.playSpeedEffect;
        }
        void changeValume()
        {
            float effectFactor = 1;
            if (!ignoreVolumeEffect)
            {

                effectFactor = Helpers.AudioSettings.settings.volumeEffect;
            }
            if (type == SoundType.shoot)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.shootVolume * effectFactor;
            }
            else if (type == SoundType.ui)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.uiVolume * effectFactor;
            }
            else if (type == SoundType.spell)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.spellVolume * effectFactor;
            }
            else if (type == SoundType.hit)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.hit * effectFactor;
            }
            else if (type == SoundType.weather)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.weather * effectFactor;
            }
            else if (type == SoundType.areaFon)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.area * effectFactor;
            }
            else if (type == SoundType.music)
            {
             
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.music * effectFactor;
            }
            else if (type == SoundType.voice)
            {
                source.volume = currentClipVolume * Helpers.AudioSettings.settings.soundVolume * Helpers.AudioSettings.settings.voice * effectFactor;
            }
            // Debug.Log("Громеость стала " + source.volume);
            //  Debug.Log("currentClipVolume " + currentClipVolume+ "AudioSettings.settings.soundVolume " + AudioSettings.settings.soundVolume + "AudioSettings.settings.uiVolume " + AudioSettings.settings.uiVolume);
        }

        /// <summary>
        /// если ренж 0 то звук 2д
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="Range"></param>
        /// <returns></returns>
        public GameObject SetParams(AudioClip clip, SoundType type, float Range = 0, bool loop = false, float volume = 0.3f, bool ignore = false, bool destroy = true)
        {
            ignoreVolumeEffect = ignore;
            currentClipVolume = volume;
            destroyOnFinish = destroy;
            source.clip = clip;
            source.loop = loop;
            this.isLoop = loop;
            this.type = type;

            if (Range == 0) source.spatialBlend = 0;
            else source.maxDistance = Range;
            source.Play();
            return gameObject;
        }
        private void OnDestroy()
        {
            try
            {
                DestroyMe();
            }
            catch (System.Exception)
            {


            }
        }
    }
}