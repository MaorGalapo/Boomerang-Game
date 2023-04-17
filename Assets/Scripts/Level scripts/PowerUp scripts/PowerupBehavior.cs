using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    #region Component References

    public PowerupController controller;

    [SerializeField]
    private Powerup powerup;

    private Transform transform_;


    #endregion
    #region Monobehaviour API
    private void Awake()
    {
        transform_ = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivatePowerup();
            Destroy(gameObject);
        }
    }

    #endregion

    private void ActivatePowerup()
    {
        controller.ActivatePowerup(powerup);
    }
    
    public void SetPowerup(Powerup powerup, int level)
    {
        this.powerup = powerup;
        this.powerup.level = level;     
        gameObject.name = powerup.name;
    }
}
