using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    private float time1; // states the time input was recived
    private bool time2 = false; // states if a touch is inbound

    [SerializeField]
    private float defaultMaxSpeed;
    private float maxSpeed; //max speed that player can move
    private float currSpeed;// current speed of player

    private int movingDir;//current miving direction
    private int Dir; // 1 = right , -1 = left

    //private float lastSpeed; // last speed player was before input recived
    private Transform transform_;
    [SerializeField]
    private float tapTime; // time the player taps on screen
    [SerializeField]
    private bool ispress; // is the input a press or a hold on the screen

    private GameMaster gameMaster;
    private bool stopGame;
    //public bool stop; // should the game stop
    private void Start() // sets vars
    {
        stopGame = false;
        Dir = 1;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        gameMaster.OnStop += GameMaster_OnStop;
        transform_ = GetComponent<Transform>();
        //stop = false;
        currSpeed = 0;
        ispress = true;
        DifficultyScript.OnDifficultyUpdate += GameMasterDiff_OnDifficultyUpdate;

    }

    private void GameMasterDiff_OnDifficultyUpdate(object sender, DifficultyScript.OnDifficultyUpdateArgs e)
    {
        //if (e.diffLevel == 0)
        //{
        //    defaultMaxSpeed = 14;
        //}
        //else
        //{
        //    if (e.diffLevel < 6)
        //        defaultMaxSpeed += 1f;
        //    else
        //        defaultMaxSpeed += .5f;
        //}
        //if (e.diffLevel == 4)
        //    defaultMaxSpeed = 19;
    }

    private void GameMaster_OnStop(object sender, GameMaster.OnStopUpdateArgs e)
    {
        stopGame = e.stopGame;
    }

    void Update()
    {
        maxSpeed = defaultMaxSpeed;
        if (!stopGame)
        {
            getInput();
            changeDir();
            move();
        }
        //else
        //    Debug.Log("<color=white>Game Lost</color>");
    }

    private void getInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(0); //detects and receives last touch
            if (touch.position.x / Screen.width > .5) // checks if touch is in movement side of screen
            {
                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        if (!time2)
                        {
                            Dir *= -1; // changes direction of player
                            acceleration = accelarationVals[0];    
                        }
                        time1 = Time.time; // sets time the touvh has started
                        time2 = true; // states that a touch is inbound
                        break;


                    case TouchPhase.Ended:
                        // Report that the touch has ended when it ends
                        time2 = false; // states that a touch has eneded
                        break;
                }
            }

        }
        //if(Input.touchCount<0)
        //{
        //    time2 = false;
        //}
    }
    [SerializeField]
    private float acceleration;
    [SerializeField]
    public float[] accelarationVals = { 1.5f, 1f };
    private void changeDir()// Track a single touch as a direction control.
    {
        
        if (currSpeed >= -0.1f&&currSpeed<=.1f||movingDir==Dir) // add acceleration to turn to make controls more responsive
            acceleration = accelarationVals[1];
        if (time2) // if a touch is inbound
        {
            tapTime = Time.time - time1; // how long is the tap
            if (tapTime > 0.2) // if the tap is above 0.2 seconds it is considerd a hold
            {
                ispress = false; // a hold
            }
            else // if a touch is less than 0.2 seconds it is a press
            {
                ispress = true; // apress and not a hold
            }
        }
        if (ispress) // if player just pressed change the curspeed to the new (-maxspeed) maxspeed (changes diraction with curve (lerp))
        {
            currSpeed = Mathf.Lerp(currSpeed, maxSpeed * Dir, Time.deltaTime*acceleration);
        }
        else // if it is a hold
        {
            if (time2) // if the touch hasnt ended yet
            {
                if (tapTime < 1.5f) // if time of touch is less than 2.5 seconds (player didnt fail)
                {
                    currSpeed = Mathf.Lerp(currSpeed, 0, Time.deltaTime*acceleration); // change gradualy the speed to 0 so it will create a longer curve
                }
                else //player did fail
                {
                    // stop = true;
                    currSpeed = Mathf.Lerp(currSpeed, maxSpeed * Dir, Time.deltaTime*acceleration);
                    //fail the player
                }
            }
            else // if a touch has ended
            {
                //if (Mathf.Abs(currspeed) < Mathf.Abs(lastspeed))
                //    currspeed = Mathf.Lerp(currspeed, maxspeed, Time.deltaTime);
                //else
                ispress = true; // return to original value (later currspeed will change dir to -maxspeed in line 75)
            }
        }
    }
    private void move() // moves the player
    {
        movingDir = ((int)Mathf.Sign(currSpeed));
        Vector3 movment = new Vector3(transform_.position.x + (currSpeed * Time.deltaTime), transform_.position.y, transform_.position.z);
        transform_.position = movment;
    }

}
