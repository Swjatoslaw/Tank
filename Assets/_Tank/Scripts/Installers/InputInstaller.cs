using Zenject;

namespace _Tank.Scripts
{
    public class InputInstaller : MonoInstaller
    {
        #region service methods

        public override void InstallBindings()
        {
            Container.Bind<InputSource>().AsSingle().NonLazy();
        }

        #endregion
    }
}