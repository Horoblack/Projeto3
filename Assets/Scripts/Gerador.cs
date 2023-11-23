using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);

        if(distanceToPlayer < distancia)
        {
            if (DoorCards.FuelFull && Input.GetKeyDown(KeyCode.E))
            {
               DoorCards.EnergyRestored = true;
                Debug.Log("A energia voltou!");
            }
            else
            {
                Debug.Log("O computador não possui energia!");
            }
        }

    }
}
