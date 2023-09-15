using System;
using _Tank.Scripts.Interfaces;
using UnityEngine;

namespace _Tank.Scripts
{
    public class Health : MonoBehaviour, IDamageable
    {
        #region properties

        public event Action Killed; 

        #endregion
    
        #region attributes

        [SerializeField, Range(0, 1)] protected float m_Armor;
        [SerializeField] protected float m_Health;

        protected float m_HealthInternal;

        #endregion

        #region engine methods

        protected virtual void OnEnable()
        {
            ResetHealth();
        }

        #endregion

        #region public methods

        public void ResetHealth()
        {
            m_HealthInternal = m_Health;
        }

        public void ApplyDamage(float damage)
        {
            m_HealthInternal -= damage * (1 - m_Armor);
            m_HealthInternal = Mathf.Clamp(m_HealthInternal, 0, float.MaxValue);
            if (Mathf.Approximately(m_HealthInternal, 0))
            {
                Killed?.Invoke();
            }
        }
    
        #endregion
    }
}