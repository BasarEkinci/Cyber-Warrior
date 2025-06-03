using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.UnityObjects.ObjectData
{
    [CreateAssetMenu(fileName = "ScarpTypes", menuName = "Scriptable Objects/ScarpTypes", order = 0)]
    public class ScrapTypesSO : ScriptableObject
    {
        public List<GameObject> scarpList;
        public GameObject GetRandomScrap()
        {
            if (scarpList.Count == 0) return null;
            int randomIndex = Random.Range(0, scarpList.Count);
            return scarpList[randomIndex];
        }
    }
}