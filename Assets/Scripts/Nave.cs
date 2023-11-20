using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    public int entregues;
    public static bool BucketUsed;
    public string _index;
    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        BucketUsed = false;
        
       

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if (DoorCards.HasBucket && Input.GetKeyDown(KeyCode.E))
            {

              var balde = InventoryManager.Items.Find(x => x.name == "Balde");
              InventoryManager.Items.Remove(balde);
                if (!BucketUsed)
                {
                    entregues++;
                    BucketUsed = true;
                }
               
            }
            else Debug.Log("Não tem!");


        }


    }
}
