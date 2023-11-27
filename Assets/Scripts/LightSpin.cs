using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpin : MonoBehaviour
{
    public Transform fatherOrbit;
    [Range(1, 200)]
    public float velRotacao;

    private void Awake()
    {

        velRotacao = 100;
    }
    void Orbitar()
    {
        transform.Rotate(Vector3.up * velRotacao * Time.deltaTime);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 relativePosition = child.position - transform.position;
            Vector3 rotatedPosition = Quaternion.Euler(0, velRotacao * Time.deltaTime, 0) * relativePosition;
            child.position = transform.position + rotatedPosition;
        }
    }

  
    void Update()
    {
       Orbitar();
    }
}
