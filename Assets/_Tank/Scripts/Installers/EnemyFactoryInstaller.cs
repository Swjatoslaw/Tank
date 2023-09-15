using _Tank.Scripts.Factory;
using UnityEngine;
using Zenject;

namespace _Tank.Scripts.Installers
{
    public class EnemyFactoryInstaller : MonoInstaller
    {
        #region attributes

        [SerializeField] private EnemyFactory m_Factory;

        #endregion

        #region service methods

        public override void InstallBindings()
        {
            Container.BindInstance(m_Factory).NonLazy();
        }

        #endregion
    }
}