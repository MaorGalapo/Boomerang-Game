using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public event EventHandler <OnStopUpdateArgs> OnStop;

    public class OnStopUpdateArgs : EventArgs
    {
        public bool stopGame;
    }
    [SerializeField]
    private ScoreCounter player;
    [SerializeField]
    private GameObject EndScreen;
    private RectTransform MainUI;

    private bool stoppedGame = true; // if stooped then do...
    public bool stopGame_GM;
    public string loseReason_GM { get; set; }
    void Start()
    {
        Time.timeScale = 1;
        MainUI = GameObject.Find("UI").GetComponent<RectTransform>();
        player = GameObject.Find("player").GetComponent<ScoreCounter>();
        stopGame_GM = false;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 80;
    }
    void Update()
    {
        //stopGame_GM = player.stopGame;
        if (stopGame_GM)
        {
            if (stoppedGame)
            {
                OnStop?.Invoke(this, new OnStopUpdateArgs { stopGame = stopGame_GM });
                Instantiate(EndScreen, MainUI);
                stoppedGame = false;
            }
        }
    }
}
