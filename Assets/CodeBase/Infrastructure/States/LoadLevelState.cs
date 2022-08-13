using Infrastructure.AssetManagement;
using Logic.Camera;
using Logic.Hero;
using Services;
using SWS;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<int>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;

        private List<GameObject> _lastLevel = new List<GameObject>();

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IAssetProvider assetProvider)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
        }

        public void Enter(int levelID)
        {
            foreach (var item in _lastLevel)
                UnityEngine.Object.DestroyImmediate(item);

            _lastLevel.Add(_assetProvider.Instantiate($"Levels/Level{levelID}/" + AssetPath.Enemy));
            _lastLevel.Add(_assetProvider.Instantiate($"Levels/Level{levelID}/" + AssetPath.Map));
            _lastLevel.Add(_assetProvider.Instantiate($"Levels/Level{levelID}/" + AssetPath.Enviroment));
            var path = _assetProvider.Instantiate<PathManager>($"Levels/Level{levelID}/" + AssetPath.Waypoints);
            var hero = _assetProvider.Instantiate<navMove>(AssetPath.Hero, path.waypoints[0].position);
            hero.pathContainer = path;
            _lastLevel.Add(path.gameObject);
            _lastLevel.Add(hero.gameObject);

            AllServices.Container.RegisterSingle<PathManager>(path);
            AllServices.Container.RegisterSingle<navMove>(hero);
            AllServices.Container.RegisterSingle<ICameraPursued>(hero.GetComponent<Hero>());
            AllServices.Container.RegisterSingle<CameraFollower>(Camera.main.GetComponent<CameraFollower>());
            AllServices.Container.Single<CameraFollower>().Init();

            EnterGame();
        }

        private void EnterGame() => _stateMachine.Enter<GameLoopState>();

        public void Exit() { }
    }
}
