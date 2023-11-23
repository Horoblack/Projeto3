using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motores : MonoBehaviour
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

        if (distanceToPlayer < distancia)
        {
            if (DoorCards.HasBucketFull && Input.GetKeyDown(KeyCode.E))
            {

                var balde = InventoryManager.Items.Find(x => x.name == "BaldeCheio");
                InventoryManager.Items.Remove(balde);
                DoorCards.FuelFull = true;
            }
        }
    }
}
