using Data;
using UnityEngine;

namespace Logic.Hero
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private BulletData _bulletData;

        private Vector3 _direction;
        private bool _moving = false;

        public void StartMove()
        {
            _direction = _bulletData.Direction;
            _moving = true;
        }

        public void StopMove() => _moving = false;

        private void Update()
        {
            if (_moving)
                transform.Translate(_direction.normalized * Time.deltaTime * _bulletData.Speed);
        }
    }
}
