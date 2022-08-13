using System;
using System.Threading.Tasks;
using UI.HealthBar;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private MonoBehaviour _health;
        [SerializeField] private Animator _animator;
        [SerializeField] private RagdollController _ragdoll;

        private bool _activated = false;

        private IHealth Health => _health as IHealth;

        public event Action OnDeath;

        public void Activate()
        {
            _healthBar.Show();
            _animator.enabled = true;
            _activated = true;
        }

        private async void Start()
        {
            if (_activated == true)
                return;
            _animator.enabled = true; //set default pose

            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime * 5));
            _healthBar.Hide();
            _animator.enabled = false;
        }

        private void OnEnable() => Health.OnHealthChanged += CheckDeath;
        private void OnDisable() => Health.OnHealthChanged -= CheckDeath;

        private void CheckDeath(float hp)
        {
            if (hp <= 0) Death();
        }

        private void Death()
        {
            OnDeath?.Invoke();
            _ragdoll.Activate();
            _healthBar.Hide();
            _animator.enabled = false;
            Health.OnHealthChanged -= CheckDeath;
            Destroy(gameObject, 5);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_health == null)
                return;
            if (_health is IHealth)
                return;

            Debug.LogError(_health.name + " needs to implement " + nameof(IHealth));
            _health = null;
        }
#endif
    }
}
