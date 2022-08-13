using System;

namespace Logic.Enemy
{
    public interface IHealth
    {
        float CurrentHP { get; }
        float MaxHP { get; }

        event Action<float> OnHealthChanged;

        void TakeDamage(float damage);
    }
}