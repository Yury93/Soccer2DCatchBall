using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance;
        [Header("���������")]
        [SerializeField] bool dontDestroyOnLoad;
        [Tooltip("������ �� ������ �� ��������� ��� �������� ����� �����")]
        [SerializeField] bool changeClipOnLevelLoaded = false;
        [Tooltip("������ ����� ������ ��� ����� �����")]
        [SerializeField] bool slowlySwapClipBetweenScenes = true;
        [Tooltip("����������� ������� ������, ���� ����������� �� �������")]
        [SerializeField] bool randomizeClips = true;

        [Header("���������")]
        [Tooltip("�� ��������� ����������� ������ ���� ������, ���� ������ ���� ������")]
        [SerializeField] List<Clip> clipsForMenu;
        [Tooltip("���� �� ������, �� �� ���� ������ ����� ������, ����� ������������� ���� ������")]
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
            yield return new WaitForEndOfFrame();//����� ������������� �� �������� ���������� �� ����� ����� ��������� �����������
           
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
         //   Debug.Log("������������ ����� ���������� ������ ������ ��������� ����� ������ "+ Helpers.AudioSettings.settings.music+ " ���������� ���� "+ normalMusic);
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
              //  Debug.Log("��������� �������� ���� �� ���� "+curClipId);
                c = curClips[curClipId];
                if (curClipId+1 < curClips.Count)
                {
           //         Debug.Log("��� ��� ������� ����  " + curClipId +" ������ ������� ������� "+ clips.Count+" ������� ++" );
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
               // Debug.Log("������ ������ ��� ��� ������ ������ �� �������������");
                PlayNextClip();
            }
        }
    }
}