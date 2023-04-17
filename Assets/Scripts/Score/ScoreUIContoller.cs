using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIContoller : MonoBehaviour
{

    [SerializeField]
    private Text distanceText;

    [SerializeField]
    private Text goldText;

    [SerializeField]
    ScoreCounter scoreCounter;
    private void ScoreCounter_UpdateScore(object sender, ScoreCounter.UpdateScoreEventArgs e)
    {
        distanceText.text = "distance :\n" + e.distance.ToString("F2") + "m";
        goldText.text = "gold : " + e.gold.ToString();
    }
    void Start()
    {
        scoreCounter = GameObject.Find("player").GetComponent<ScoreCounter>();
        scoreCounter.UpdateScore += ScoreCounter_UpdateScore;
    }
}
