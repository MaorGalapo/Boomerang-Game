using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBounds : MonoBehaviour
{
    private Transform playerT_;
    private bool stopGame = false;
    private GameMaster gameMaster;


    private BoundAnim boundImage_L;
    private BoundAnim boundImage_R;
    void Start()
    {
        boundImage_L = GameObject.Find("Bound_L").GetComponent<BoundAnim>();
        boundImage_R = GameObject.Find("Bound_R").GetComponent<BoundAnim>();
        PU_setBoundsState("Normal");
        gameMaster = GetComponent<GameMaster>();
        gameMaster.OnStop += GameMaster_OnStop;
        playerT_ = GameObject.Find("PlayerObject").GetComponent<Transform>();
    }
    private void GameMaster_OnStop(object sender, GameMaster.OnStopUpdateArgs e)
    {
        stopGame = e.stopGame;
    }
    private Action boundsFunction;
    private void normalBounds()
    {
        if (playerT_.position.x > 5.2 || playerT_.position.x < -5.2)
        {

            stopGame = true;
            boundImage_R.SetBound(false);
            boundImage_L.SetBound(false);
            gameMaster.loseReason_GM = "Bounds";
            gameMaster.stopGame_GM = stopGame;
        }
        else
        {
            if (playerT_.position.x > 3.6 && playerT_.position.x < 5.2)
            {
                if (!boundImage_R.getBoundActive())
                    boundImage_R.SetBound(true);
            }
            else if (playerT_.position.x < -3.6 && playerT_.position.x > -5.2)
            {
                if (!boundImage_L.getBoundActive())
                    boundImage_L.SetBound(true);
            }
            else
            {
                boundImage_R.SetBound(false);
                boundImage_L.SetBound(false);
            }
        }
    }
    private void portalBounds()
    {
        if (playerT_.position.x < -3.6)
        {
            playerT_.position = new Vector3(3.6f, playerT_.position.y, playerT_.position.z);
        }
        else if (playerT_.position.x > 3.6 )
        {
            playerT_.position = new Vector3(-3.6f, playerT_.position.y, playerT_.position.z);
        }
    }
    private void setBoundsState(bool active, Color color,Action method)
    {
        boundImage_L.SetBound(active);
        boundImage_R.SetBound(active);
        boundImage_L.setColor(color);
        boundImage_R.setColor(color);
        boundsFunction = method;
    }
    public void PU_setBoundsState(string state)
    {
        if(state.ToLower().Equals("normal"))
            setBoundsState(false, Color.red, normalBounds);
        if (state.ToLower().Equals("portal"))
            setBoundsState(true, new Color32(159, 52, 255, 255), portalBounds);
    }
    void Update()
    {
        if (!stopGame)
        {
            boundsFunction();
        }
    }
}
