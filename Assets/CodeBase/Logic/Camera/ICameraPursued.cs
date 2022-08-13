using Services;
using UnityEngine;

namespace Logic.Camera
{
    public interface ICameraPursued : IService
    {
        Transform TransformForFollowing { get; }
    }
}