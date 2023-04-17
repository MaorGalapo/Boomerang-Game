using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    private TMP_Text itemName_Txt;
    private TMP_Text itemDescription_Txt;
    private TMP_Text itemPrice_Txt;
    private Image itemIcon_Img;
    public Button itemIcon_Btn;

    public ItemOnDisplay itemSo_OnDisplay { get; set; }
    private Button itemButton;
    private ShopManeger shopManeger;

    private Transform progressBar;


    public void ShopItemSetup(ShopItemSO item, ItemOnDisplay currItem)
    {
        itemSo_OnDisplay = currItem;
        ItemPriceSetup(item);
        ProgressBarSetup(item);
        ItemNameAndImageSetup(item, currItem); ;
    }
    private void ProgressBarSetup(ShopItemSO item)
    {
        int lvl = itemSo_OnDisplay.itemLevel;
        int maxLvl = item.itemPrice.Length;
        for (int i = 0; i < progressBar.childCount; i++)
        {
            if (i < maxLvl)
            {
                if (i < lvl)
                    progressBar.GetChild(i).GetComponent<Image>().color = Color.green;
                else
                    progressBar.GetChild(i).GetComponent<Image>().color = Color.gray;
            }
            else
                progressBar.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
    private void ItemPriceSetup(ShopItemSO item)
    {
        if (itemSo_OnDisplay.itemLevel < item.itemPrice.Length)
            itemPrice_Txt.text = item.itemPrice[itemSo_OnDisplay.itemLevel].ToString();
        else
            itemPrice_Txt.text = "";
    }
    private void ItemNameAndImageSetup(ShopItemSO item, ItemOnDisplay currItem)
    {
        itemIcon_Img.sprite = item.itemImage;
        itemSo_OnDisplay = currItem;
        itemName_Txt.text = item.itemName;
        itemDescription_Txt.text = item.itenDescription;

    }
    private void Start()
    {
        shopManeger = GameObject.Find("Shop_Maneger").GetComponent<ShopManeger>();
        itemButton = GetComponentInChildren<Button>();
        itemButton.onClick.AddListener(PurchaseItem);
        progressBar = GameObject.Find("ProgressBar").GetComponent<Transform>();
        itemIcon_Img = GameObject.Find("Item_Img").GetComponent<Image>();
        itemName_Txt = GameObject.Find("Item_Name_Txt").GetComponent<TMP_Text>();
        itemDescription_Txt = GameObject.Find("Item_Description_Txt").GetComponent<TMP_Text>();
        itemPrice_Txt = GameObject.Find("Item_Price_Txt").GetComponent<TMP_Text>();
    }

    private void PurchaseItem()
    {
        shopManeger.PurchaseItem(itemSo_OnDisplay);
    }
    public void SetPurchaseButton(string text, bool interactable)
    {
        itemIcon_Btn.interactable = interactable;
        itemButton.GetComponentInChildren<TMP_Text>().text = text;
    }
}
