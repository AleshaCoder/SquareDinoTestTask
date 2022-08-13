using Infrastructure.AssetManagement;
using Logic.Hero;
using Services;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Demo";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter() =>
          _sceneLoader.Load(Initial, onLoaded: EnterLoadMenu);

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IBulletPool>(new BulletPool());
        }

        private void EnterLoadMenu()
        {
            _stateMachine.Enter<LoadLevelState, int>(0);
            //_stateMachine.Enter<LoadMenuState, string>(MainMenu); нет меню, нет перехода в стейт меню
        }
    }
}
