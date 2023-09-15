using _Tank.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace _Tank.Scripts.Installers
{
    public class TankHealthInstaller : MonoInstaller
    {
        #region attributes

        [SerializeField] private TankHealth m_TankHealth;

        #endregion

        #region service methods

        public override void InstallBindings()
        {
            Container.BindInstance(m_TankHealth);
        }

        #endregion
    }
}
