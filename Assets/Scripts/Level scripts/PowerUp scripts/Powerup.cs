using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Powerup
{
    [SerializeField]
    private ShopItemSO itemSO;

    public string getID() { return itemSO.getID(); }

    [SerializeField]
    public string name;

    [SerializeField]
    public float[] duration;

    public int level = 0;

    // used to apply the Powerup of the Powerup
    [SerializeField]
    public UnityEvent startAction;

    // used to remove the Powerup of the Powerup
    [SerializeField]
    public UnityEvent endAction;

    [SerializeField]
    public GameObject powerupPrefab;

    public void End()
    {
        if (endAction != null)
        {
            endAction.Invoke();
        }
    }

    public void Start()
    {
        if (startAction != null)
        {
            startAction.Invoke();
        }
    }
    public float getDuration()
    {
        if (duration.Length - level>0)
            return duration[level];
        return duration[duration.Length - 1];
    }
    public int getPowerupLevel(string powerup)
    {
        return PlayerPrefs.GetInt(powerup, 0);
    }
}