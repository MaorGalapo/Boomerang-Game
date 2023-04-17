using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ShopMainUi : MonoBehaviour
{
    private Button gameButton;
    private Button resetButton;
    private TMP_Text goldText;
    void Start()
    {
        gameButton = GameObject.Find("Game_btn").GetComponent<Button>();
        resetButton = GameObject.Find("Reset_btn").GetComponent<Button>();
        gameButton.onClick.AddListener(OpenGame);
        resetButton.onClick.AddListener(ResetGame);
        goldText = GameObject.Find("Gold_txt").GetComponent<TMP_Text>();
        goldText.text = $"Gold :{PlayerPrefs.GetInt("PlayerCoins", 0)}";
        ShopManeger.OnBuyItem += ShopManeger_OnBuyItem;
    }

    private void ShopManeger_OnBuyItem(ItemOnDisplay obj)
    {
        UpdateGold();
    }

    public void UpdateGold()
    {
        goldText.text = $"Gold :{PlayerPrefs.GetInt("PlayerCoins", 0)}";
    }
    private void OpenGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    private void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnDestroy()
    {
        ShopManeger.OnBuyItem -= ShopManeger_OnBuyItem;
    }
}
