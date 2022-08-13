using Services;
using System;
using UnityEngine;

namespace Infrastructure
{
    public class GameStarter : MonoBehaviour, IService
    {
        private bool _gameStarted = false;

        public event Action OnStart;

        private void Awake() => AllServices.Container.RegisterSingle(this);

        public void Reset() => _gameStarted = false;

        private void Update()
        {
            if (_gameStarted)
                return;

            if (Input.GetMouseButtonUp(0))
            {
                _gameStarted = true;
                OnStart?.Invoke();
            }
        }
    }
}