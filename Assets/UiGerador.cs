using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGerador : MonoBehaviour
{
    [SerializeField] private float maxSlider = 100f;
    [SerializeField] public float target = 10;

   public void SlideChange(float valor)
    {
        float localValue = valor * maxSlider;

        if(localValue == target)
        {
            Debug.Log("achou");
        }
    }
}
