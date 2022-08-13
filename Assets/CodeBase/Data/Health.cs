using System;

namespace Data
{
    [Serializable]
    public class Health
    {
        public float MaxHP;
        public float CurrentHP;

        public void Reset() => CurrentHP = MaxHP;
    }
}