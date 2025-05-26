using UnityEngine;

namespace Data.ValueObjects
{
    [System.Serializable]
    public struct CmpVisualData
    {
        public Material EyeMaterial;
        public GameObject Mesh; //This value will change based on the level
    }
}