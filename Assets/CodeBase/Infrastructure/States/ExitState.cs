using UnityEngine;

namespace Infrastructure.States
{
    public class ExitState : IState
    {
        public void Enter() => Application.Quit();
        public void Exit() => throw new System.NotImplementedException();

    }
}
