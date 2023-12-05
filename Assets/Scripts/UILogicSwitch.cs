using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UILogicSwitch : MonoBehaviour, IPointerClickHandler
{
    public GameObject up;
    public GameObject on;
    public bool isUp;
    public bool isOn;

    private void Awake()
    {
        up.SetActive(isUp);
        on.SetActive(isOn);
        if(isOn)
            UILogicMain.Instance.SwitchChange(1);
    }

    public void OnPointerClick (PointerEventData eventData)
    {
       
        isUp = !isUp;
        isOn = !isOn;
        up.SetActive(isUp);
        on.SetActive(isOn);
        if (isOn)
        {
            isUp = true; 
            UILogicMain.Instance.SwitchChange(1);
        }

        else
            UILogicMain.Instance.SwitchChange(-1);

    }
}
