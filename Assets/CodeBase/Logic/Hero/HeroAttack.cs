using UnityEngine;
using Services;
using Data;
using UnityEngine.Events;
using Infrastructure;

namespace Logic.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private Transform _originForFire;
        [SerializeField] private BulletData _bulletData;
        private GameStarter _gameStarter;
        private IBulletPool _bulletPool;
        private UnityEngine.Camera _camera;
        private bool _activated = false;

        public UnityEvent OnAttack;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;
            _bulletPool = AllServices.Container.Single<IBulletPool>();
            _gameStarter = AllServices.Container.Single<GameStarter>();
            _gameStarter.OnStart += Activate;
        }

        private void OnDisable() => _gameStarter.OnStart -= Activate;

        private void Activate() => _activated = true;

        private void Update()
        {
            if (_activated == false)
                return;
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    var bullet = _bulletPool.GetFreeBullet();
                    bullet.BulletMovement.StopMove();
                    bullet.transform.position = _originForFire.position;
                    bullet.Show();
                    _bulletData.Direction = hit.point - _originForFire.position;
                    bullet.BulletMovement.StartMove();
                    OnAttack?.Invoke();
                }
            }
        }
    }
}
