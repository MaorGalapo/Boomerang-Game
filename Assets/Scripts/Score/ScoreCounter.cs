using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreCounter : MonoBehaviour
{

    public float distance;

    public int gold;

    public bool stopGame;

    private GameMaster gameMaster;

    private SphereCollider playerCollider;
    void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        gameMaster.OnStop += GameMaster_OnStop; ;
        stopGame = false;
        distance = 0;
        gold = 0;
        playerCollider = GetComponent<SphereCollider>();
    }

    private void GameMaster_OnStop(object sender, GameMaster.OnStopUpdateArgs e)
    {
        stopGame = e.stopGame;
    }

    public event EventHandler<UpdateScoreEventArgs> UpdateScore;
    public class UpdateScoreEventArgs : EventArgs
    {
        public float distance;
        public int gold;
    }
    void Update()
    {
        if (!stopGame)
        {
            distance = distance + Time.deltaTime * 10;
            UpdateScore?.Invoke(this, new UpdateScoreEventArgs { distance = distance, gold = gold});
        }
        else
            playerCollider.enabled = false;
    }
    void OnTriggerEnter(Collider HitInfo)
    {

        if (HitInfo.gameObject.CompareTag("Pick Up"))
        {
            HitInfo.gameObject.SetActive(false);
            gold+=100;
        }
        if (HitInfo.CompareTag("Obsticale"))
        {
            stopGame = true;
            CustomTag newTag = HitInfo.GetComponent<CustomTag>();
            if (newTag != null)
                gameMaster.loseReason_GM = newTag.customeTag_;
            else
                gameMaster.loseReason_GM = "error";
            gameMaster.stopGame_GM = stopGame;
        }

    }

}
