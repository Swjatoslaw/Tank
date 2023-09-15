using System;
using System.Runtime.InteropServices;
using _Tank.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace _Tank.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class AmmoBase : MonoBehaviour
    {
        #region properties

        public AmmoType Type => m_Type;

        #endregion
        
        #region attributes

        [SerializeField] protected AmmoType m_Type;
        [SerializeField] protected float m_Damage;
        [SerializeField] protected float m_Speed = 1;
        
        protected AmmoFactory m_Factory;
        protected Rigidbody2D m_Rigidbody;
        protected bool m_Destroying;
        
        #endregion

        #region public methods

        public abstract void Fire(Transform _Transform);

        public void Init(AmmoFactory _Factory)
        {
            m_Factory = _Factory;
        }
        
        #endregion
        
        #region engine methods

        protected void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        #endregion

        #region service methods

        protected abstract void DestroySelf();
        
        protected void OnCollisionEnter2D(Collision2D col)
        {
            if(m_Destroying)
                return;
            
            if (col.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.ApplyDamage(m_Damage);
            
            DestroySelf();
        }

        #endregion
    }
}
