using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Items : ScriptableObject 
{
    public int id;
    public string ItemName;
    public int value;
    public bool isRemovable;
    public Sprite icon;
    public itemType _itemType;
    public cardType _cardType;

    public enum itemType
    {
        Bandagem,
        Medkit,
        Munição,
        Cartão1,
        Cartao2,
        Cartao3,
        Balde,
        ChaveFenda
    }

    public enum cardType
    {
        Blue,
        Red,
        Yellow
    }

}
