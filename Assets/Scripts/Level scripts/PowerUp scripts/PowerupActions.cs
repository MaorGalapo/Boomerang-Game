using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerupActions : MonoBehaviour
{
    //[SerializeField]
    //private PlayerController playerController;

    [SerializeField]
    private WeaponController weaponController;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject shieldEffect_Prefab;

    [SerializeField]
    private PlayerBounds boundsController;

    public int currLvl { get;  set; } // current used power up level

    #region player effecting powerups
    #region slow motion
    // slow motion power up - makes every thing slower
    public void SlowMotionStartAction()
    {
        SlowMotionTimeAction(.5f);
    }
    public void SlowMotionEndAction()
    {
        SlowMotionTimeAction(1f);
    }
    private void SlowMotionTimeAction(float time)
    {
        Time.timeScale = time;
    }
    #endregion
    public void BoostStartAction()
    {
        player.GetComponentInChildren<Collider>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        Time.timeScale = 5f;

    }

    public void BoostEndAction()
    {
        player.GetComponentInChildren<Collider>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        Time.timeScale = 1f;
    }
    public void portalBoundsStartAction()
    {
        boundsController.PU_setBoundsState("portal");
    }
    public void portalBoundsEndAction()
    {
        boundsController.PU_setBoundsState("normal");
    }
    public void ShieldStartAction()
    {
        //spawn a shield that will have own script
    }
    // end of slow motion powerup
    #endregion

    #region shot effecting powerups
    public void AttackSpeedStartAction()
    {
        weaponController.multAttackRate(2);
    }

    public void AttackSpeedEndAction()
    {
        weaponController.defaultAttackRate();
    }
    #endregion

}
