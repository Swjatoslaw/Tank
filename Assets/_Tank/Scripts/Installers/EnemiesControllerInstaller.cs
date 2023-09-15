using _Tank.Scripts.Factory;
using UnityEngine;
using Zenject;

namespace _Tank.Scripts.Installers
{
    public class EnemiesControllerInstaller : MonoInstaller
    {
        #region attributes

        [SerializeField] private EnemiesController m_EnemiesController;

        #endregion

        #region service methods

        public override void InstallBindings()
        {
            Container.BindInstance(m_EnemiesController).NonLazy();
        }

        #endregion
    }
}