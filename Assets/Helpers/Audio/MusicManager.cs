using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance;
        [Header("Поведение")]
        [SerializeField] bool dontDestroyOnLoad;
        [Tooltip("Менять ли музыку на следующую при загрузке новой сцены")]
        [SerializeField] bool changeClipOnLevelLoaded = false;
        [Tooltip("Плавая смена музыки при смене сцены")]
        [SerializeField] bool slowlySwapClipBetweenScenes = true;
        [Tooltip("Перемешиват ьсписки музыки, либо проигрывать по порядку")]
        [SerializeField] bool randomizeClips = true;

        [Header("Настройки")]
        [Tooltip("По умолчанию проигрывает только этот список, если список ниже пустой")]
        [SerializeField] List<Clip> clipsForMenu;
        [Tooltip("Если не пустой, то во всех сценах кроме первой, будеб проигрываться этот список")]
        [SerializeField] List<Clip> clipsForGameplay;
        [SerializeField] AudioSource source;

        Coroutine slowSwapCor=null;

        int curClipId=0;

        private void Awake()
        {
            if (dontDestroyOnLoad)
            {
                if (instance != null)
                {
                    if (instance != this)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    DontDestroyOnLoad(this.gameObject);
                    instance = this;
                }
            }
        }
        private void OnLevelWasLoaded(int level)
        {
            if (changeClipOnLevelLoaded)
            {
                if (slowlySwapClipBetweenScenes)
                {
                  //  Debug.Log("slowlySwapClipBetweenScenes");
                    if (slowSwapCor == null)
                    {
                        Debug.Log("slow cor == null");
                        StartCoroutine(slowSwapClip());
                    }
                    else
                    {
                        curClipId = 0;
                        PlayNextClip();
                    }
                }
                else
                {
                    curClipId = 0;
                    PlayNextClip();
                }
               
            }
        }
        IEnumerator slowSwapClip()
        {
            yield return new WaitForEndOfFrame();//иначе аудиоСеттингс не успевает подргузить из плеер префс громкость сохраненную
           
            float normalMusic = Helpers.AudioSettings.settings.music;
            
            while (Helpers.AudioSettings.settings.music > 0)
            {
                Helpers.AudioSettings.settings.music = Mathf.MoveTowards(Helpers.AudioSettings.settings.music, 0, 1* Time.unscaledDeltaTime);
                yield return null;
            }
            curClipId = 0;
            PlayNextClip();
            while (Helpers.AudioSettings.settings.music < normalMusic)
            {
                Helpers.AudioSettings.settings.music = Mathf.MoveTowards(Helpers.AudioSettings.settings.music, normalMusic, 1 * Time.unscaledDeltaTime);
                yield return null;
            }
         //   Debug.Log("Восстановили после замедления музыки теперь громкость нашей музыки "+ Helpers.AudioSettings.settings.music+ " Должнабыла быть "+ normalMusic);
            slowSwapCor = null;

        }
       public void PlayNextClip()
        {
            List<Clip> curClips=clipsForMenu;
            if (clipsForGameplay.Count>0)
            {
                if (Application.loadedLevel != 0)
                {
                    curClips = clipsForGameplay;
                }
            }
            
            Clip c;
            if (randomizeClips)
            {
                int r = Random.Range(0, curClips.Count);
                c = curClips[r];
             
            }
            else
            {
              //  Debug.Log("запускаем следующи клип по айди "+curClipId);
                c = curClips[curClipId];
                if (curClipId+1 < curClips.Count)
                {
           //         Debug.Log("так как текущий айди  " + curClipId +" меньше размера массива "+ clips.Count+" плюсуем ++" );
                    curClipId++;
                }
                else curClipId = 0;
               
            }
            source.GetComponent<Sound>().SetParams(c.clip, Sound.SoundType.music, volume: c.volume, destroy: false, ignore:c.ignoreEffects) ;
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!source.isPlaying)
            {
               // Debug.Log("Меняем музыку так как сейчас ничего не проигрывается");
                PlayNextClip();
            }
        }
    }
}