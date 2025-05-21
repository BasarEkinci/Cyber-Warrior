using Data.UnityObjects;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ScarpAmountManager.Instance.AddScarp(300);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ScarpAmountManager.Instance.TrySpendScarp(120);
        }
    }
}
