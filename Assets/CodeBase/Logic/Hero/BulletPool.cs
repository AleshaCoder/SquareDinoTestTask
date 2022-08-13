using Infrastructure.AssetManagement;
using Services;
using System.Collections.Generic;

namespace Logic.Hero
{
    public class BulletPool : IBulletPool
    {
        private IAssetProvider _assetProvider;
        private List<Bullet> _bullets;
        private int _maxCount = 10;
        private int _index = 0;

        public void Instantiate()
        {
            _assetProvider = AllServices.Container.Single<IAssetProvider>();
            _bullets = new List<Bullet>();
            for (int i = 0; i < _maxCount; i++)
            {
                Bullet bullet = _assetProvider.Instantiate<Bullet>(AssetPath.Bullet);
                bullet.Hide();
                _bullets.Add(bullet);
            }
        }

        public Bullet GetFreeBullet()
        {
            if (_bullets == null)
                Instantiate();
            if (_index >= _maxCount) _index = 0;
            return _bullets[_index++];
        }
    }
}