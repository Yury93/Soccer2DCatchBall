using UnityEngine;

namespace CatchTheSphere {
    /// <summary>
    /// Спавнер сущностей
    /// </summary>
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Sphere m_SpherePrefabs;
        [SerializeField] private SphereProperties sphereProperties;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private SpawnMode m_SpawnMode;
        [SerializeField] private int m_NumSpawns;
        private float m_RespawnTime = 0;
        private float m_Timer;
        /// <summary>
        /// Минимальное значение таймера
        /// </summary>
        [SerializeField] private float m_minTimer;
        /// <summary>
        /// Максимальное значение таймера
        /// </summary>
        [SerializeField] private float m_maxTimer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }
            m_Timer = Random.Range(m_minTimer,m_maxTimer);
        }
        private void Update()
        {
           
            if (m_Timer > m_RespawnTime)
            {
                m_Timer -=  Time.deltaTime;
            }
            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= m_RespawnTime)
            {
                SpawnEntities();
                m_Timer = Random.Range(m_minTimer, m_maxTimer);
            }
        }
        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                GameObject e = Instantiate(m_SpherePrefabs.gameObject);
                e.GetComponent<Sphere>().SphereEditor(sphereProperties);
                e.transform.position = m_Area.GetRandomInsideZone();
                

                //Рандомная позиция точки спавна по оси Y
                m_Area.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
