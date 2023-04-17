using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeam : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float laserAttackTime; // time of laser attack

    [SerializeField]
    private float laserChargeTime; // time of laser charge

    private bool shot = false;

    public bool alive = true; 

    public float timeIdle;
    void Start()
    {
        alive = true;
        animator = GetComponentInChildren<Animator>();
    }
    private float tickTimer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIdle > 0)
        {
            timeIdle =tickTimer(timeIdle);
        }
        else
        {
            animator.SetTrigger("StartCharge");
            if (laserChargeTime >= 0)
            {
                laserChargeTime = tickTimer(laserChargeTime);
            }
            else if (!shot)
            {
                animator.SetTrigger("FireLaser");
                shot = true;
            }
            if (laserAttackTime >= 0 & shot)
            {
                laserAttackTime = tickTimer(laserAttackTime);
            }
            else if (shot)
            {
                animator.SetTrigger("EndLaser");
                alive = false;
                Destroy(gameObject, .5f);
            }
        }
    }
}
