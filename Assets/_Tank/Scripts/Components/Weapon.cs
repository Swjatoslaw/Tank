using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Tank.Scripts
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        #region attributes

        private readonly List<WeaponType> m_Registry = new();
        
        [Inject] private AmmoFactory m_Factory;
        
        [SerializeField] private WeaponType[] m_WeaponTypes;

        private int m_WeaponId;
        
        #endregion

        #region public methods

        public void Fire()
        {
            WeaponType weaponType = m_Registry[m_WeaponId];
            AmmoType ammoType = weaponType.Ammo.Type;
            AmmoBase ammo = m_Factory.GetObjectOfType(ammoType);
            ammo.Fire(weaponType.WeaponPoint.transform);
        }

        public void ChangeWeapon(int _Delta)
        {
            if(m_Registry.Count > 0)
                m_Registry[m_WeaponId].gameObject.SetActive(false);
            
            m_WeaponId = Repeat(m_WeaponId + _Delta, m_WeaponTypes.Length);
        
            SetWeapon();
            
            int Repeat(int _Value, int _Length)
            {
                if (_Length == 0)
                    return 0;

                return _Value > 0 ? _Value % _Length : (_Length + _Value % _Length) % _Length;
            }
        }

        #endregion

        #region engine methods

        private void Awake()
        {
            Init();
        }

        #endregion

        #region service methods

        private void Init()
        {
            foreach (WeaponType weaponType in m_WeaponTypes)
            {
                WeaponType type = Instantiate(weaponType, transform);
                type.gameObject.SetActive(false);
                m_Registry.Add(type);
            }
            
            SetWeapon();
        }

        private void SetWeapon()
        {
            if(m_Registry.Count > 0)
                m_Registry[m_WeaponId]?.gameObject.SetActive(true);
        }
        
        #endregion
    }
}