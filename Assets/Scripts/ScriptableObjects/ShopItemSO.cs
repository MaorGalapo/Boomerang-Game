using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName ="Scriptable Objects/New shop item", order =1)]
public class ShopItemSO : ScriptableObject
{
    [SerializeField]
    private string ID;
    public string getID() { return ID; }
    public string itemName;
    public string itenDescription;
    public int[] itemPrice;
    public Sprite itemImage;
    public bool consumable;
}
