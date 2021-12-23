using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helpers
{

    public class SoundCreator : MonoBehaviour
    {
        [SerializeField] GameObject soundPrefab;
        public static SoundCreator creator;


        Vector3 position2d = Vector3.zero;
        private void Awake()
        {
            creator = this;
        }
        /// <summary>
        /// не уничтожать на прямую
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <param name="range"></param>
        /// <param name="loop"></param>
        /// <returns></returns>
        public GameObject CreateSound(AudioClip clip, Sound.SoundType type, Vector3 position, float range = 5, bool loop = false, float volume = 0.3f, bool ignoreEffects = false)
        {

            GameObject go = Instantiate(soundPrefab, position, Quaternion.identity);
            go.GetComponent<Sound>().SetParams(clip, type, range, loop, volume, ignore: ignoreEffects);
            return go;
        }
        /// <summary>
        /// 2д звук
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="type"></param>
        /// <param name="loop"></param>
        /// <param name="volume"></param>
        /// <param name="ignoreEffects"></param>
        /// <returns></returns>
        public GameObject CreateSound(AudioClip clip, Sound.SoundType type, bool loop = false, float volume = 0.3f, bool ignoreEffects = false)
        {

            GameObject go = Instantiate(soundPrefab, position2d, Quaternion.identity);
            go.GetComponent<Sound>().SetParams(clip, type, loop: loop, volume: volume, ignore: ignoreEffects);
            return go;
        }
        public GameObject CreateSound(Helpers.Clip clip, Sound.SoundType type, bool loop = false)
        {

            GameObject go = Instantiate(soundPrefab, position2d, Quaternion.identity);
            go.GetComponent<Sound>().SetParams(clip.clip, type, loop: loop, volume: clip.volume, ignore: clip.ignoreEffects);
            return go;
        }
    }

}