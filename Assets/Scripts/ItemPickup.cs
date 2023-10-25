using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Items item;
    public static ItemPickup InstancePickup;
    public GameObject playerGO;
    public bool Picked;
    [Range(1f,200f)] public float distancia = 20f;

    public void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        Picked = false;
        InstancePickup = this;
    }

    public void Pickup()
    {
        
        InventoryManager.Instance.Add(item);   
        Destroy(gameObject);

      
    }

    private void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if (Input.GetKey(KeyCode.E))
                Pickup();
        }
    }
}
