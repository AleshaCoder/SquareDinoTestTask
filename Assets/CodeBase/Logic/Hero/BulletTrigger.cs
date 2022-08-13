using Data;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletTrigger : MonoBehaviour
    {
        [SerializeField] private BulletData _bulletData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_bulletData.Damage);
                other.attachedRigidbody?.AddExplosionForce(_bulletData.Force, transform.position, _bulletData.Force);
            }
            gameObject.SetActive(false);
        }
    }
}