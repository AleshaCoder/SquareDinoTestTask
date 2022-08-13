using UnityEngine;

namespace Logic.Hero
{
    public class Bullet : MonoBehaviour
    {
        public BulletMovement BulletMovement;
        public BulletTrigger BulletTrigger;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}