using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Tank.Scripts.Factory
{
     public class EnemyFactory : MonoBehaviour
    {
        #region attributes

        protected readonly Dictionary<EnemyType, Stack<EnemyBase>> m_Registry = new();
        
        [SerializeField] protected EnemyBase[] m_Types;
        [SerializeField] protected int m_DefaultSize = 4;

        #endregion

        #region public methods

        public EnemyBase GetObjectOfType(EnemyType _Type)
        {
            if (m_Registry[_Type].Count > 0)
                return m_Registry[_Type].Pop();
            
            return Instantiate(m_Types.FirstOrDefault(_Enemy => _Enemy.Type == _Type));
        }

        public void ReturnToPool(EnemyBase _Enemy)
        {
            var obj = gameObject;
            _Enemy.gameObject.transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);
            
            m_Registry[_Enemy.Type].Push(_Enemy);
        }

        #endregion
        
        #region engine methods

        protected void Awake()
        {
            foreach (EnemyBase ammo in m_Types)
            {
                m_Registry[ammo.Type] = new Stack<EnemyBase>();
            
                for(int i = 0; i < m_DefaultSize; i++)
                {
                    EnemyBase ammoBase = Instantiate(ammo);
                    ammoBase.gameObject.SetActive(false);
                    m_Registry[ammo.Type].Push(ammoBase);
                }
            }
        }
        
        #endregion
    }
}
