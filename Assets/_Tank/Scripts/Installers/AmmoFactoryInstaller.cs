using UnityEngine;
using Zenject;

namespace _Tank.Scripts.Installers
{
    public class AmmoFactoryInstaller : MonoInstaller
    {
        #region attributes

        [SerializeField] private AmmoFactory m_Factory;

        #endregion

        #region service methods

        public override void InstallBindings()
        {
            Container.BindInstance(m_Factory).NonLazy();
        }

        #endregion
    }
}