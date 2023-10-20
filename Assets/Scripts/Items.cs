using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Items : ScriptableObject 
{
    public int id;
    public string ItemName;
    public int value;
    public Sprite icon;
    public itemType _itemType;

    public enum itemType
    {
        Bandagem,
        Munição,
        Cartão1,
        Cartao2,
        Cartao3,
        Cartao4
    }



}
