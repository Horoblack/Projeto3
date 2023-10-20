using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public  void UseItem()
    {
        switch(item._itemType)
        {
            case Items.itemType.Muni��o:
                Shooting.instance.maxAmmo += 10;
                break;
            case Items.itemType.Bandagem:
              PlayerMove.instance.IncreaseHealth(item.value);
                break;
            case Items.itemType.Cart�o1:
                Debug.Log("A porta de n�mero 1 se abriu!");
                break;
            case Items.itemType.Cartao2:
                Debug.Log("A porta de n�mero 2 se abriu!");
                break;
            case Items.itemType.Cartao3:
                Debug.Log("A porta de n�mero 3 se abriu!");
                break;
            case Items.itemType.Cartao4:
                Debug.Log("A porta de n�mero 4 se abriu!");
                break;
            default:
                break; 
        }

        RemoveItem();
    }

}
