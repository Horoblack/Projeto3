using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryItemController : MonoBehaviour
{
    public Items item;

    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Items newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item._itemType)
        {
            case Items.itemType.Munição:
                Shooting.instance.maxAmmo += 10;
                break;
            case Items.itemType.Bandagem:
                PlayerMove.instance.IncreaseHealth(item.value);
                break;
            case Items.itemType.Medkit:
                PlayerMove.instance.IncreaseHealth(item.value);
                break;
       
            default:
                break;
        }


        if (item.isRemovable)
            RemoveItem();
    }
    
    public void HasItens()
    {
        switch(item.id)
        {
            case 3:
                DoorCards.HasblueCard = true;
                Debug.Log("tem o cartão azul");
                break;
            case 4:
                DoorCards.HasredCard = true;
                break;
            case 5:
                DoorCards.HasYellowCard = true;
                break;
            case 8:
                DoorCards.HasBucket = true;
                break;
            case 6:
                DoorCards.HasWrench = true;
                break;
            case 9:
                DoorCards.HasBucketFull = true; 
                break;
        }
    }

    private void Update()
    {
        HasItens();
    }
}