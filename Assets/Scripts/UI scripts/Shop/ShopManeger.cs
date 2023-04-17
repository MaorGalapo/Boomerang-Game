using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class ShopManeger : MonoBehaviour
{
    private int coins;

    public static event Action<ItemOnDisplay> OnBuyItem;

    private ShopTemplate DisplayItem_UI; // current display item
    private ItemOnDisplay DisplayItem_;

    private ItemOnDisplay[] shopItems;
    [SerializeField]
    private Transform contents_transform;

    void Start()
    {
        DisplayItem_UI = GameObject.Find("Item_desplay").GetComponent<ShopTemplate>();
        coins = PlayerPrefs.GetInt("PlayerCoins", 0); // temp save
        shopItems = new ItemOnDisplay[contents_transform.childCount];
        for (int i = 0; i < contents_transform.childCount; i++) // generates array of all items in shop from the items that exist
        {
            ItemOnDisplay shopItem = contents_transform.GetChild(i).GetComponent<ItemOnDisplay>();
            if (shopItem != null)
                shopItems[i] = shopItem;
            //else
            //    throw new System.Exception($"Item number -{i} has no ScriptbleObject");
        }
        shopItems[0].displayItem();
        OnBuyItem += ShopManeger_OnBuyItem;
    }

    private void ShopManeger_OnBuyItem(ItemOnDisplay item)
    {
        LoadItem(item.itemSO, DisplayItem_);
        CheckPurchaseable(item);
    }

    public void PurchaseItem(ItemOnDisplay item)
    {
        if (item != null && CheckPurchaseable(item))
        {
            coins -= item.getPrice();
            PlayerPrefs.SetInt("PlayerCoins", coins);
            OnBuyItem?.Invoke(item);
        }
    }

    public bool CheckPurchaseable(ItemOnDisplay item)
    {
        int price;
        if (item.IsPurchaseable()) // if item is not maxed out sed price
        {
            price = item.getPrice();
            DisplayItem_UI.SetPurchaseButton("Purchase", true);
        }
        else // item maxed out
        {
            DisplayItem_UI.SetPurchaseButton("Maxed", false);
            return false;
        }
        if (coins >= price)
        {
            DisplayItem_UI.SetPurchaseButton("Purchase", true);
            return true;
        }
        else
        {
            DisplayItem_UI.SetPurchaseButton("Purchase", false);
            return false;
        }
    }

    public void LoadItem(ShopItemSO item, ItemOnDisplay currItem) // used to load item on display (sign)
    {
        DisplayItem_ = currItem;
        DisplayItem_UI.ShopItemSetup(item, currItem);
        CheckPurchaseable(currItem);
    }
    //public void LoadItems() // used to load all items from list to display (table)
    //{
    //    for (int i = 0; i < itemsArray.Length; i++)
    //    {
    //        shopItemsOnDisplay[i].itemName_Txt.text = itemsArray[i].itemName;
    //        shopItemsOnDisplay[i].itemMesh.mesh = itemsArray[i].item_mesh;
    //        shopItemsOnDisplay[i].itemMeshRenderer.material=itemsArray[i].item_material;           
    //    }
    //}
    private void OnDestroy()
    {
        OnBuyItem -= ShopManeger_OnBuyItem;
    }
}
