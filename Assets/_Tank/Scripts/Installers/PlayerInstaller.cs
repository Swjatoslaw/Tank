using UnityEngine;
using Zenject;

namespace _Tank.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        #region attributes

        [SerializeField] private PlayerMovement m_Player;

        #endregion

        #region service methods

        public override void InstallBindings()
        {
            Container.BindInstance(m_Player);
        }

        #endregion
    }
}
