using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Extensions
{
    public class EnemyHolder
    {
        public List<GameObject> EnemyList;
        public GameObject peekEnemy;
        public EnemyHolder()
        {
            EnemyList = new List<GameObject>();
        }
        public void CalculateClosestEnemy(Vector3 playerPosition)
        {
            if (EnemyList.Count == 0) return;
            if (EnemyList.Count == 1)
            {
                peekEnemy = EnemyList[0];
            }
            else
            {
                for (int i = 0; i < EnemyList.Count; i++)
                {
                    if (Vector3.Distance(peekEnemy.transform.position, playerPosition) > Vector3.Distance(EnemyList[i].transform.position, playerPosition))
                    {
                        peekEnemy = EnemyList[i];
                    }
                }
            }
        }

        public void SetPeekedEnemy()
        {
            peekEnemy = EnemyList[0];
        }
    }
}
