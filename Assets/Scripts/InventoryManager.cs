using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager Instance;
    public static List<Items> Items = new List<Items>();

    public Transform ItemContainer;
    public GameObject InventoryItem;

    public Toggle EnableRemove; 

    public InventoryItemController[] InventoryItems;
    public void Awake()
    {
        Instance = this;
    }

    public void ClearItems()
    {
        Items.Clear();
    }

    public void Add(Items item)
    {
        Items.Add(item);
    }

    public void Remove(Items item) 
    {
        Items.Remove(item);
    }

    public void ListItems()
    {

        foreach (var item in Items) {
            GameObject obj = Instantiate(InventoryItem, ItemContainer);
            var ItemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();   

            ItemName.text = item.ItemName;
            ItemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);
        }
        SetInventoryItems();
    }

   public void EnableItemsRemove()
    {
        if(EnableRemove.isOn)
        {
            foreach (Transform item in ItemContainer)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContainer)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }

    }

  public void SetInventoryItems()
    {
        InventoryItems = ItemContainer.GetComponentsInChildren<InventoryItemController>();
        for ( int i = 0; i < Items.Count; i++ )
        {
           
            InventoryItems[i].AddItem(Items[i]);
        }
    }

    public void Clean()
    {
        foreach (Transform item in ItemContainer)
        {
            Destroy(item.gameObject);
        }
    }
   
}
