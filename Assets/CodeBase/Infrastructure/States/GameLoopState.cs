using Logic.Checkpoints;
using Services;
using SWS;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly AllServices _services;
        private navMove _movement;
        private CheckpointsPool _checkPointsPool;
        private GameStarter _gameStarter;
        private PathManager _path;
        private GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine stateMachine, AllServices services)
        {
            _services = services;
            _gameStateMachine = stateMachine;
        }

        public void Exit()
        {
            _checkPointsPool.OnNext -= StartMove;
            _movement.movementChangeEvent -= StartShot;
            _gameStarter.OnStart -= _movement.StartMove;
            _gameStarter.Reset();
        }

        public void Enter()
        {
            _movement = _services.Single<navMove>();
            _checkPointsPool = _services.Single<CheckpointsPool>();
            _gameStarter = _services.Single<GameStarter>();
            _path = _services.Single<PathManager>();

            _checkPointsPool.OnNext += StartMove;
            _gameStarter.OnStart += _movement.StartMove;
            _movement.movementChangeEvent += StartShot;
            _movement.movementEndEvent += ReloadLevel;
        }

        private void ReloadLevel() => _gameStateMachine.Enter<LoadLevelState, int>(0);

        private void StartMove() => _movement.Resume();

        private void StartShot(int point)
        {
            if (point == 0)
                return;
            if (_checkPointsPool.AlreadyKilled(point - 1) == false)
                _movement.Pause();
        }
    }
}
