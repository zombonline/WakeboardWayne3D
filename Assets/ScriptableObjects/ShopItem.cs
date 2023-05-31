using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Costume,
    Board,
    Pet
}

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]

public class ShopItem : ScriptableObject
{
    public ItemType itemType;
    public Sprite icon;
    public string itemName;
    [TextArea(2, 5)]
    public string itemDesc;
    public bool storePlacePurchase = false, unlocked = false;
    public int cost;
    public GameObject model;
    public int ItemID;


}
