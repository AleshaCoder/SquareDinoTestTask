using UnityEngine;

namespace Logic.Enemy
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;

        public void Activate()
        {
            foreach (var item in _rigidbodies)
                item.isKinematic = false;
        }

        public void Deactivate()
        {
            foreach (var item in _rigidbodies)
                item.isKinematic = true;
        }

        private void Awake()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            Deactivate();
        }
    }
}