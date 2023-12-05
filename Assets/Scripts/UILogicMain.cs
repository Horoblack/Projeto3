using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogicMain : MonoBehaviour
{
    static public UILogicMain Instance;

    public int switchCount;
    private int onCount = 0;

    private void Awake()
    {
        Instance = this;
    }
    public void SwitchChange(int points)
    {
        onCount = onCount + points;
        if(onCount == switchCount)
        {
            Debug.Log("brabo");
            DoorCards.EnergyRestored = true;
        }
    }

}
