using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DifficultyScript : MonoBehaviour
{
    public static event EventHandler<OnDifficultyUpdateArgs> OnDifficultyUpdate;
    [SerializeField]
    private int diffLevel = 0;

    private int MaxDiffLevel = 10;

                    //  level :         0   1   2   3   4    5    6    7    8     9    10
    private float[] timeToNextLevel = { 10, 30, 55, 85, 120, 140, 165, 190, 225, 255, 300 };

    public class OnDifficultyUpdateArgs : EventArgs
    {
        public int diffLevel;
    }
    void setDifficulty()
    {
        if (diffLevel < MaxDiffLevel && Time.timeSinceLevelLoad >= timeToNextLevel[diffLevel])
        {
            diffLevel++;
            OnDifficultyUpdate?.Invoke(this, new OnDifficultyUpdateArgs { diffLevel = diffLevel });
        }
    }

    void Update()
    {
        if(diffLevel == 0)
            OnDifficultyUpdate?.Invoke(this, new OnDifficultyUpdateArgs { diffLevel = 0 });
        setDifficulty();
    }
}
