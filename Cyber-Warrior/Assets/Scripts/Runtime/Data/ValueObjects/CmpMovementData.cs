using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [System.Serializable]
    public struct CmpMovementData
    {
        public Vector3 followOffset;
        public Vector3 attackOffset;
        public float moveSpeed;
        public float rotationSpeed;
    }
}