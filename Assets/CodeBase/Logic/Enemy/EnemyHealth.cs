using Data;
using System;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private Health _health;

        public float CurrentHP => _health.CurrentHP;
        public float MaxHP => _health.MaxHP;

        public event Action<float> OnHealthChanged;

        public void TakeDamage(float damage)
        {
            _health.CurrentHP -= damage;
            OnHealthChanged?.Invoke(_health.CurrentHP);
        }
    }
}
