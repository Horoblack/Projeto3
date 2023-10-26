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
            case Items.itemType.Cartão1:
                DoorCards.HasblueCard = true;             
                break;
            case Items.itemType.Cartao2:
                DoorCards.HasredCard = true; 
                break;
            case Items.itemType.Cartao3:
                Debug.Log("A porta de número 3 se abriu!");
                break;
            case Items.itemType.Cartao4:
                Debug.Log("A porta de número 4 se abriu!");
                break;
            default:
                break;
        }
        if (item.isRemovable)
            RemoveItem();
        else
            Debug.Log("nao removi");
    }
    
}