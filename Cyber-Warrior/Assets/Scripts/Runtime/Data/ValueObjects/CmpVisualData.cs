using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [System.Serializable]
    public struct CmpVisualData
    {
        public Material eyeMaterial;
        public GameObject mesh; //This value will change based on the level
    }
}