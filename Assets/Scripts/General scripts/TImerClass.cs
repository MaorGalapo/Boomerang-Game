using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TImerClass : MonoBehaviour
{
    private Action timerCallBack;
    private float timer;
    public void SetTimer(float timer,Action timerCallBack)
    {
        this.timer = timer;
        this.timerCallBack = timerCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f){
            timer -= Time.deltaTime;
            if(timer < 0f) // timer complete
            {
                timerCallBack();
            }
        }
    }
}
