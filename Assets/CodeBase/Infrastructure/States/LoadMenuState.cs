using Infrastructure;
using Infrastructure.States;
using Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }

    public void Enter(string sceneName)
    {
        _loadingCurtain.Show();
        _sceneLoader.Load(sceneName, onLoaded: EnterMenu);
    }

    private void EnterMenu()
    {
        _loadingCurtain.Hide();
        _gameStateMachine.Enter<MenuLoopState>();
    }

    public void Exit()
    {
    }
}
