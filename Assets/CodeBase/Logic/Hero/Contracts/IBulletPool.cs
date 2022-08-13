using Services;

namespace Logic.Hero
{
    public interface IBulletPool : IService
    {
        Bullet GetFreeBullet();
        void Instantiate();
    }
}