using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemOnDisplay : MonoBehaviour
{
    public ShopItemSO itemSO;
    private Button itemButton;
    private ShopManeger shopManeger;
    private TMP_Text itemName;

    private bool consumable;
    public int itemLevel;
    void Start()
    {
        shopManeger = GameObject.Find("Shop_Maneger").GetComponent<ShopManeger>();
        itemButton = GetComponentInChildren<Button>();
        itemButton.onClick.AddListener(displayItem);
        itemName = GetComponentInChildren<TMP_Text>();
        if (itemSO != null)
        {
            itemLevel = PlayerPrefs.GetInt(itemSO.getID(), 0); // temp save
            itemName.text = itemSO.name;
            consumable = itemSO.consumable;
        }
        ShopManeger.OnBuyItem += ShopManeger_OnBuyItem;
    }

    private void ShopManeger_OnBuyItem(ItemOnDisplay obj)
    {
        if (obj != null &&itemSO!=null&& obj.itemSO.itemName.Equals(itemSO.itemName))
            LevelUp();
    }

    public bool IsPurchaseable()
    {
        return itemLevel < itemSO.itemPrice.Length;
    }
    public int getPrice()
    {
        if (consumable)
            return itemSO.itemPrice[0];
        return itemSO.itemPrice[itemLevel];
    }
    public void LevelUp()
    {
        itemLevel++;
        PlayerPrefs.SetInt(itemSO.getID(), itemLevel); // temp save
    }
    public void displayItem()
    {
        shopManeger.LoadItem(itemSO, GetComponent<ItemOnDisplay>());
    }
    private void OnDestroy()
    {
        ShopManeger.OnBuyItem -= ShopManeger_OnBuyItem;
    }
}
