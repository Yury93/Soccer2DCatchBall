using UnityEngine;
using UnityEngine.Events;

namespace CatchTheSphere
{
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField]
        private bool m_Indestructible;
        public bool Indestructible => m_Indestructible;
        /// <summary>
        /// Начисление очков
        /// </summary>
        [SerializeField] private int m_ScoreValue;
         public int ScoreValue => m_ScoreValue;
        /// <summary>
        /// Стартовое Количество хитпоинтов.
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int startHp => m_HitPoints;
        
        /// <summary>
        /// Текущие хитпоинты.
        /// </summary>
        private int m_CurrentHitPoints;
        public int currentHp => m_CurrentHitPoints;
        
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;
        #endregion

        #region Unity Events    
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region  Public API

        /// <summary>
        /// 
        /// </summary>Приминение дамага к объекту
        /// <param name="damage">Урон наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }
        #endregion

        /// <summary>
        /// Уничтожение объекта когда хитпоинты ниже нуля
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        public virtual int Use(SphereProperties sphereProperties)
        {
            m_ScoreValue = sphereProperties.scoreProp;

            return m_ScoreValue;
        }
    }

}
