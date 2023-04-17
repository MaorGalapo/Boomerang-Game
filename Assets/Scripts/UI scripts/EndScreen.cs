using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    private Button endButton;
    private Button shopButton;
    private TMP_Text distanceText;
    private TMP_Text goldText;
    private TMP_Text endText;

    private ScoreCounter scoreCounter;
    private GameMaster gameMaster;
    //public string loseReason { get; set; } // reason player lost game , obs = hit obsticale||breakobs = hit breakable obsticale||bounds = got out of bounds|| laser = laser || shot = shot
    void Start()
    {
        SetUpVars();
        SetUpTexts();
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins", 0) + scoreCounter.gold);
        endButton.onClick.AddListener(Restart);
        shopButton.onClick.AddListener(OpenShop);
    }
    private void SetUpVars()
    {
        scoreCounter = GameObject.Find("player").GetComponent<ScoreCounter>();
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        endText = GameObject.Find("EndText").GetComponent<TMP_Text>();
        goldText = GameObject.Find("EndText_gold_num").GetComponent<TMP_Text>();
        distanceText = GameObject.Find("EndText_distance_num").GetComponent<TMP_Text>();

        endButton = GameObject.Find("EndButton").GetComponent<Button>();
        shopButton = GameObject.Find("ShopButton").GetComponent<Button>();
    }

    private void SetUpTexts()
    {
        goldText.text = scoreCounter.gold.ToString();
        distanceText.text = scoreCounter.distance.ToString("F2");
        endText.text = gameMaster.loseReason_GM;

    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }
    private void OpenShop()
    {
        SceneManager.LoadScene("GameShop");
        Destroy(gameObject);
    }
}
