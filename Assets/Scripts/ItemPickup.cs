using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Items item;
    public static ItemPickup InstancePickup;
    public bool Picked;

    public void Awake()
    {
        Picked = false;
        InstancePickup = this;
    }

    public void Pickup()
    {
        
        InventoryManager.Instance.Add(item);   
        Destroy(gameObject);
      
    }


    private void OnMouseDown()
    {
        Pickup();
    }
}
