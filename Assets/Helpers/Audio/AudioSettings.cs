using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    [System.Serializable]
    public struct Clip
    {
        public AudioClip clip;
        [Range(0, 1)]
        public float volume;
        public bool ignoreEffects;
    }

    public class AudioSettings : MonoBehaviour
    {
        public static AudioSettings settings;
        public delegate void OnChangeVolume();
        public OnChangeVolume onChangeVolume;
        public OnChangeVolume onChangePlaySpeed;



        [SerializeField] float _soundVolume;
        public float soundVolume
        {
            get { return _soundVolume; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _soundVolume = value;
            }
        }
        float _shootVolume;
        public float shootVolume
        {
            get { return _shootVolume; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _shootVolume = value;
            }
        }

        float _uiVolume;
        public float uiVolume
        {
            get { return _uiVolume; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _uiVolume = value;
            }
        }
        float _spellVplume;
        public float spellVolume
        {
            get { return _spellVplume; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _spellVplume = value;
            }
        }
        float _hit;
        public float hit
        {
            get { return _hit; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _hit = value;
            }
        }
        float _weather;
        public float weather
        {
            get { return _weather; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _weather = value;
            }
        }
        float _area;
        public float area
        {
            get { return _area; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _area = value;
            }
        }
        [SerializeField] float _music;

        public float music
        {
            get { return _music; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _music = value;
            }
        }
        float _voice;
        public float voice
        {
            get { return _voice; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                _voice = value;
            }
        }
        [Tooltip("используется если нужно приглушить определенные звуки")]
        public float volumeEffect = 1;
        public float playSpeedEffect = 1;
        bool slowProcess;
        [SerializeField] bool _ignorePitchEffects = false;
        public bool ignorePitchEffects
        {
            get { return _ignorePitchEffects; }
            set
            {
                if (onChangeVolume != null) onChangeVolume.Invoke();
                Debug.Log("ignore =" + value);
                _ignorePitchEffects = value;
            }
        }
        [SerializeField] bool saveCurrentSettings;


        Coroutine pitchCor;
        private void Awake()
        {
            settings = this;
            volumeEffect = 1;
            playSpeedEffect = 1;
        }
        private void Update()
        {
            if (saveCurrentSettings)
            {
                SaveSettings();
                saveCurrentSettings = false;
            }
        }
        void Start()
        {
            LoadSettings();
            StartCoroutine(delay());

        }
        IEnumerator delay()
        {
            if (onChangeVolume != null) onChangeVolume.Invoke();
            yield return new WaitForEndOfFrame();
            if (onChangeVolume != null) onChangeVolume.Invoke();
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.05f);
            if (onChangeVolume != null) onChangeVolume.Invoke();
            yield return new WaitForSeconds(0.1f);
            if (onChangeVolume != null) onChangeVolume.Invoke();

        }
        /// <summary>
        /// меняет фактор снижения громкости , раздавая события всем.
        /// </summary>
        /// <param name="v"></param>
        public void ChangeVolumeEffect(float v)
        {
            volumeEffect = v;
            onChangeVolume.Invoke();
        }
        public void SaveSettings()
        {
            Debug.Log("SaveAudioSatinngs started");
            PlayerPrefs.SetFloat("AudioSettings.soundVolume", soundVolume);
            PlayerPrefs.SetFloat("AudioSettings.uiVolume", uiVolume);
            PlayerPrefs.SetFloat("AudioSettings.shootVolume", shootVolume);
            PlayerPrefs.SetFloat("AudioSettings.hit", hit);
            PlayerPrefs.SetFloat("AudioSettings.weather", weather);
            PlayerPrefs.SetFloat("AudioSettings.area", area);
            PlayerPrefs.SetFloat("AudioSettings.spellVolume", spellVolume);
            PlayerPrefs.SetFloat("AudioSettings.music", music);
            PlayerPrefs.SetFloat("AudioSettings.voice", voice);

            PlayerPrefs.SetString("AudioSettings.ignorePitchEffects", _ignorePitchEffects.ToString());
            Debug.Log("SaveAudioSatinngs End");
        }
        void LoadSettings()
        {
            if (!PlayerPrefs.HasKey("AudioSettings.soundVolume"))//если ни разу не сохраняли
            {
                settings.soundVolume = 1f;
                settings.shootVolume = 0.3f;
                settings.spellVolume = 0.3f;
                settings.hit = 0.3f;
                settings.area = 0.3f;
                settings.music = 1f;
                settings.weather = 0.3f;
                settings.uiVolume = 0.3f;
                settings.voice = 0.3f;

                SaveSettings();

            }
            else
            {
                Debug.Log("Аудио загрузило настройки из префсов");
                soundVolume = PlayerPrefs.GetFloat("AudioSettings.soundVolume");
                uiVolume = PlayerPrefs.GetFloat("AudioSettings.uiVolume");
                shootVolume = PlayerPrefs.GetFloat("AudioSettings.shootVolume");
                hit = PlayerPrefs.GetFloat("AudioSettings.hit");
                weather = PlayerPrefs.GetFloat("AudioSettings.weather");
                area = PlayerPrefs.GetFloat("AudioSettings.area");
                spellVolume = PlayerPrefs.GetFloat("AudioSettings.spellVolume");
                music = PlayerPrefs.GetFloat("AudioSettings.music");
                voice = PlayerPrefs.GetFloat("AudioSettings.voice");

                string bol = PlayerPrefs.GetString("AudioSettings.ignorePitchEffects");
                Debug.Log("igrore loaded from pref = "+bol);
                _ignorePitchEffects = bol == "True" ? true : false;

            }
        }

        /// <summary>
        /// Создает эффект замедления проигрывания, на определенное время, самостоятельно возвращая в норму скорость. (защищает от дублирования эффета)
        /// </summary>
        /// <param name="duration">длительность нахожденяи на минимальнйо сорости</param>
        /// <param name="minSpeed"></param>
        public void CreateSlowSpeedEffect(float duration, float minSpeed)
        {
            if (_ignorePitchEffects) return;
            if (!slowProcess) StartCoroutine(slow(duration, minSpeed));
        }
        IEnumerator slow(float duration, float minSpeed)
        {
            slowProcess = true;
            while (playSpeedEffect > minSpeed)
            {

                playSpeedEffect -= Time.unscaledDeltaTime;
                if (onChangePlaySpeed != null) onChangePlaySpeed.Invoke();
                yield return null;
            }

            yield return new WaitForSecondsRealtime(duration);

            while (playSpeedEffect < 1)
            {

                playSpeedEffect += Time.unscaledDeltaTime;
                if (onChangePlaySpeed != null) onChangePlaySpeed.Invoke();
                yield return null;
            }
            slowProcess = false;

        }
        /// <summary>
        /// меняет скорость воспроизведения к Target. при дублировании перекрывает старую задачу
        /// </summary>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public void SetPitchEffect(float target, float speed)
        {
            if (_ignorePitchEffects) return;
            
            if (!slowProcess)
            {

                if (pitchCor != null) StopCoroutine(pitchCor);
                pitchCor = StartCoroutine(pitchEffect(target, speed));
            }
        }
        IEnumerator pitchEffect(float target, float minSpeed)
        {
            while (playSpeedEffect != target)
            {

                playSpeedEffect = Mathf.MoveTowards(playSpeedEffect, target, minSpeed * Time.unscaledDeltaTime);
                if (onChangePlaySpeed != null) onChangePlaySpeed.Invoke();
                yield return null;
            }

            pitchCor = null;
        }

    }
}