using Logic.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Checkpoints
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private List<EnemyDeath> _enemy;

        private int _remainder;

        public bool AlreadyKilled => _remainder <= 0;

        public event Action OnDestruction;

        public void Activate()
        {
            foreach (var item in _enemy)
                item.Activate();
        }

        private void OnEnable()
        {
            _remainder = _enemy.Count;
            foreach (var item in _enemy)
                item.OnDeath += CheckDestruction;
        }

        private void CheckDestruction()
        {
            _remainder--;
            if (AlreadyKilled)
                OnDestruction?.Invoke();
        }

        private void OnDisable()
        {
            foreach (var item in _enemy)
                if (item != null)
                    item.OnDeath += CheckDestruction;
        }
    }
}
