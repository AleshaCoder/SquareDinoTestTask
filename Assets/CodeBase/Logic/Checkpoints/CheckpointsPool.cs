using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Checkpoints
{
    public class CheckpointsPool : MonoBehaviour, IService
    {
        [SerializeField] private List<Checkpoint> _checkpoints;
        private int _index;

        public bool AlreadyKilled(int index)
        {
            if (index > _checkpoints.Count - 1)
                return false;
            if (index < 0)
                return false;
            return _checkpoints[index].AlreadyKilled;
        }

        public event Action OnNext;

        private void OnEnable()
        {
            AllServices.Container.RegisterSingle<CheckpointsPool>(this);
            _index = 0;
            foreach (var item in _checkpoints)
                item.OnDestruction += OpenNextCheckpoint;
        }

        private void OnDisable()
        {
            _index = 0;
            foreach (var item in _checkpoints)
                item.OnDestruction -= OpenNextCheckpoint;
        }

        private void Start()
        {
            _checkpoints[0].Activate();
        }

        private void OpenNextCheckpoint()
        {
            _index++;
            if (_index < _checkpoints.Count)
                _checkpoints[_index].Activate();
            OnNext?.Invoke();
        }
    }
}
