using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzesFim : MonoBehaviour
{
    private void Update()
    {
        if (DoorCards.EnergyRestored)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

    }
}
