using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public class EnemyHolder
    {
        public readonly GameObject referanceObject;
        public List<GameObject> EnemyList;
        public GameObject peekEnemy;
        public EnemyHolder()
        {
            EnemyList = new List<GameObject>();
            referanceObject = GameObject.FindWithTag("Crosshair");
            EnemyList.Add(referanceObject);
        }
        public void CalculateClosestEnemy(Vector3 currentPosition)
        {
            if (EnemyList.Count == 0) return;
            if (EnemyList.Count == 1)
            {
                peekEnemy = EnemyList[0];
            }
            else
            {
                for (int i = 1; i <= EnemyList.Count; i++)
                {
                    if (Vector3.Distance(peekEnemy.transform.position, currentPosition) > Vector3.Distance(EnemyList[i].transform.position, currentPosition))
                    {
                        peekEnemy = EnemyList[i];
                    }
                }
            }
        }
        public void SetPeekedEnemy()
        {
            if (EnemyList.Count > 1)
            {
                peekEnemy = EnemyList[1];
            }
            else
            {
                peekEnemy = EnemyList[0];
            }
        }
    }
}
