using Logic.Camera;
using UnityEngine;

namespace Logic.Hero
{
    public class Hero : MonoBehaviour, ICameraPursued
    {
        public Transform TransformForFollowing => transform;
    }
}
