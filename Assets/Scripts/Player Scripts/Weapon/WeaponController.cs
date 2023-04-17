using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    #region Generic attack variables
    private float lastAttackTime; // time of last attack

    private float attackRate; // attacks per second

    private float DefaultAttackRate; // attacks per second

    private Animator animator;

    private bool touchInbound; // if a touch is inbound

    private GameObject currShot;
    #endregion

    #region Specific attack variables

    private Transform swordTransform_;

    private Transform FirePoint;

    [SerializeField]
    private GameObject SB_Prefab;
    private float SBFirerate_mult = 1.2f;

    [SerializeField]
    private GameObject WS_Prefab;
    private float WSFIrerate_mult = 2.2f;


    #endregion
    private GameMaster gameMaster;
    private bool stopGame;

    private Action attackMethod;
    public void multAttackRate(float mult)
    {
        attackRate *= mult;
    }
    public void defaultAttackRate()
    {
        attackRate = DefaultAttackRate;
    }
    public void changeSwordScale(float scale = .4f)
    {
        swordTransform_.localScale = new Vector3(scale, scale, scale);
    }
    private void Start()
    {

        swordTransform_ = GetComponent<Transform>();

        FirePoint = GameObject.Find("FirePoint").GetComponent<Transform>();

        animator = GetComponent<Animator>();

        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();


        DefaultAttackRate = 2f;

        attackRate = DefaultAttackRate;

        lastAttackTime = -1;


        stopGame = false;

        gameMaster.OnStop += GameMaster_OnStop;


        SetAtaackMethod("bfs");


    }

    private void GameMaster_OnStop(object sender, GameMaster.OnStopUpdateArgs e)
    {
        stopGame = e.stopGame;
    }

    public void SetAtaackMethod(string attack)
    {
        switch (attack.ToLower())
        {
            case "normal":
                defaultAttackRate();
                changeSwordScale(.4f);
                attackMethod = SwordAttack;
                break;
            case "bfs":
                changeSwordScale(.7f);
                multAttackRate(2);
                attackMethod = SwordAttack;
                break;
            case "swordbeam":
                multAttackRate(SBFirerate_mult);
                currShot = SB_Prefab;
                attackMethod = BeamAttack;
                break;
            case "wizardshot":
                multAttackRate(WSFIrerate_mult);
                currShot = WS_Prefab;
                attackMethod = FireShot;
                break;
            default:
                throw new ArgumentException("unknown attack");

        }
    }

    private void Update()
    {
        if (!stopGame)
        {
            checkForTap();
            attackMethod();
        }
        else
        {
            animator.Play("Sword_Idle");
        }

    }
    void SwordAttack() // shoots by fire rate
    {

        if (touchInbound)
        {
            if (Time.time - lastAttackTime > 1 / attackRate)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
                touchInbound = false;
            }
        }
    }
    void BeamAttack() // shoots by fire rate
    {
        if (touchInbound)
        {
            if (Time.time - lastAttackTime > 1 / attackRate)
            {
                animator.SetTrigger("Attack");
                Instantiate(currShot, FirePoint.position, FirePoint.rotation);
                lastAttackTime = Time.time;
                touchInbound = false;
            }
        }
    }
    
    void FireShot() // shoots by fire rate
        {
            if (touchInbound)
            {
                if (Time.time - lastAttackTime > 1 / attackRate)
                {
                    Instantiate(currShot, FirePoint.position, FirePoint.rotation);
                    lastAttackTime = Time.time;
                }
            }
        }

        void checkForTap()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i); //detects and receives last touch
                if (touch.position.x / Screen.width < .5) // checks if touch is in movement side of screen
                {
                    switch (touch.phase)
                    {

                        case TouchPhase.Began:
                            touchInbound = true; // states that a touch is inbound
                            break;


                        case TouchPhase.Ended:
                            // Report that the touch has ended when it ends
                            touchInbound = false; // states that a touch has eneded
                            break;

                    }
                }
            }
        }
    }
