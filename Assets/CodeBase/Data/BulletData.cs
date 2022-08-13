using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletData", order = 1)]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private float _damage = 100;
        [SerializeField] private float _force = 100;
        [SerializeField] private float _speed = 100;
        public Vector3 Direction;

        public float Damage => _damage;
        public float Force => _force;
        public float Speed => _speed;
    }
}