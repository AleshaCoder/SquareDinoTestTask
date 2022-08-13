using Infrastructure.AssetManagement;
using Logic;
using Services;
using System;
using System.Collections.Generic;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, loadingCurtain),
                [typeof(MenuLoopState)] = new MenuLoopState(this, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IAssetProvider>()),
                [typeof(GameLoopState)] = new GameLoopState(this, services),
                [typeof(ExitState)] = new ExitState()
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;
    }
}
