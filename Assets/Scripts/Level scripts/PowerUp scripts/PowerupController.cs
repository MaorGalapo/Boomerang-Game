using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField]
    private List<Powerup> powerups;

    [SerializeField]
    private Dictionary<Powerup, float> activePowerups = new Dictionary<Powerup, float>();
    private List<Powerup> keys = new List<Powerup>();

    private PowerupActions Actions;

    #region Global Powerup Functions

    // Handles the beginning and ending of activated Powerups.
    // Inactive Powerups are removed automatically.
    private void HandleGlobalPowerups()
    {
        bool changed = false;

        if (activePowerups.Count > 0)
        {
            foreach (Powerup powerup in keys)
            {
                if (activePowerups[powerup] > 0)
                {
                    activePowerups[powerup] -= Time.deltaTime;
                }
                else
                {
                    changed = true;

                    activePowerups.Remove(powerup);

                    //if (powerup.endAction != null)
                    powerup.End();
                }
            }
        }

        if (changed)
        {
            keys = new List<Powerup>(activePowerups.Keys);
        }
    }


    // Adds a global Powerup to the activePowerups list.
    public void ActivatePowerup(Powerup powerup)
    {
        if (!activePowerups.ContainsKey(powerup))
        {
            Actions.currLvl = powerup.level;
            powerup.Start();
            activePowerups.Add(powerup, powerup.getDuration());
            //Debug.Log($"<color=green>Powerup lvl { powerup.level}, Powerup Duration {powerup.getDuration()}</color>");
        }
        else
        {
            activePowerups[powerup] += powerup.getDuration();
        }

        keys = new List<Powerup>(activePowerups.Keys);
    }

    // Calls the end action of each powerup and clears them from the activePowerups
    public void ClearActivePowerups()
    {
        foreach (KeyValuePair<Powerup, float> Powerup in activePowerups)
        {
            Powerup.Key.End();
        }

        activePowerups.Clear();
    }

    #endregion

    private void Start()
    {
        Actions = GetComponent<PowerupActions>();
    }
    void Update()
    {
        HandleGlobalPowerups();
    }
    public void SpawnRandomepowerup(Vector3 position)
    {
        SpawnPowerup(powerups[Random.Range(0, powerups.Count)], position);
    }


    // Spawns a powerup by given name at given position.
    public GameObject SpawnPowerup(Powerup powerup, Vector3 position)
    {
        GameObject powerupGameObject = Instantiate(powerup.powerupPrefab);

        var powerupBehaviour = powerupGameObject.GetComponent<PowerupBehavior>();

        powerupBehaviour.controller = this;

        powerupBehaviour.SetPowerup(powerup, powerup.getPowerupLevel(powerup.getID()));

        powerupGameObject.transform.position = position;

        powerupGameObject.transform.SetParent(transform);

        return powerupGameObject;
    }

    //public GameObject SpawnRandomPowerup(Vector3 position)
    //{
    //    return SpawnPowerup(powerups[Random.Range(0, powerups.Count - 1)], position);
    //}
}
