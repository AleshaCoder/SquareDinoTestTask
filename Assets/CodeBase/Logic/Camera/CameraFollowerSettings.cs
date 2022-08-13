using System;
using UnityEngine;

namespace Logic.Camera
{
    public sealed partial class CameraFollower
    {
        [Serializable]
        public class CameraFollowerSettings : ICloneable
        {
            public bool Physical = true;
            public float DistanceToTarget = 15.0f;
            public float FollowSpeed = 3.0f;
            public Vector3 EulerAngle = new Vector3(70, 0, 0);
            public Vector3 Offset = Vector3.zero;

            [Space]
            public bool FollowX;
            public bool FollowY;
            public bool FollowZ;

            [Space]
            public bool FollowRotationX;
            public bool FollowRotationY;
            public bool FollowRotationZ;

            [Space]
            public bool Follow = false;

            [Space]
            public bool UseRotation = false;
            public bool AutoDistance = false;

            public object Clone() => MemberwiseClone();
        }
    }
}