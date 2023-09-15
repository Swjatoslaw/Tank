using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Tank.Scripts
{
    public class AmmoFactory : MonoBehaviour
    {
        #region attributes
        
        protected readonly Dictionary<AmmoType, Stack<AmmoBase>> m_Registry = new();
        
        [SerializeField] protected AmmoBase[] m_Types;
        [SerializeField] protected int m_DefaultSize = 4;

        #endregion

        #region public methods

        public AmmoBase GetObjectOfType(AmmoType _Type)
        {
            if (m_Registry[_Type].Count > 0)
                return m_Registry[_Type].Pop();

            AmmoBase ammo = InstantiateAmmo(m_Types.FirstOrDefault(_Ammo => _Ammo.Type == _Type));
            return ammo;
        }

        public void ReturnToPool(AmmoBase _Ammo)
        {
            var obj = gameObject;
            _Ammo.gameObject.transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);

            m_Registry[_Ammo.Type].Push(_Ammo);
        }

        #endregion

        #region engine methods

        protected void Awake()
        {
            foreach (AmmoBase ammo in m_Types)
            {
                m_Registry[ammo.Type] = new Stack<AmmoBase>();

                for (int i = 0; i < m_DefaultSize; i++)
                {
                    AmmoBase ammoBase = InstantiateAmmo(ammo);
                    ammoBase.gameObject.SetActive(false);
                    m_Registry[ammo.Type].Push(ammoBase);
                } 
            }
        }
        
        protected virtual AmmoBase InstantiateAmmo(AmmoBase _Ammo)
        {
            AmmoBase ammo = Instantiate(_Ammo);
            ammo.Init(this);
            return ammo;
        }
        
        #endregion
    }
}