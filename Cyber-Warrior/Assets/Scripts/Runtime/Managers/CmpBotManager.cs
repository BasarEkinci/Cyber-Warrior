using Runtime.Data.UnityObjects.ObjectData;
using UnityEngine;

namespace Runtime.Managers
{
    public class CmpBotManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private CmpBotDataSo botDataSo;

        private void Start()
        {
            InitializeBotData();
        }

        private void InitializeBotData()
        {
            Instantiate(botDataSo.statDataList[levelManager.CurrentLevel].visualData.mesh, transform.position,
                Quaternion.identity, transform);
        }
    }
}