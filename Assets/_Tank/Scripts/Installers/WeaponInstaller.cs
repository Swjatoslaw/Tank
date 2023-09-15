using Zenject;

namespace _Tank.Scripts.Installers
{
    public class WeaponInstaller : MonoInstaller
    {
        #region service methods

        public override void InstallBindings()
        {
            Container.Bind<IWeapon>().FromComponentInChildren().AsSingle();
        }

        #endregion
    }
}