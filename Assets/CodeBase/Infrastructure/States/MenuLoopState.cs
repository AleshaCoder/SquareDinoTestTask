using Infrastructure.States;
using Services;

public class MenuLoopState : IState
{
    private GameStateMachine _gameStateMachine;
    private readonly AllServices _services;

    public MenuLoopState(GameStateMachine gameStateMachine, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _services = services;
    }

    public void Enter()
    {
    }

    private void ExitGame() => _gameStateMachine.Enter<ExitState>();

    private void EnterGame() => _gameStateMachine.Enter<LoadLevelState, int>(0);

    public void Exit()
    {
    }
}
