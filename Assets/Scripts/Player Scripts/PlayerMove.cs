using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // testing stuff
    public Text speedtext; // to see player currspeed
    public Text failtext; // to see if player failed

    // -----------------------------------------------------
    public float RotateSpeed = 5f; // speed in which the player will rotate

    public float speed; // speed in which the player shuld move
    public float currspeed; // speed in shich the player moves
    public float tspeed; // speed helper
    public float time; // time it should take the object to move to its other value
    public bool var; // check is speed is above or equal to 100%
    public bool var2; // check is speed is returned by tspeed

    private Vector2 startlocation;
    public bool stop; // should the game stop
    public float q; // time since the player held the space button
    public bool b; // does the player holds space

    private Rigidbody rb; // the player
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        stop = false;
        currspeed = 0;
        time = 2f;
        startlocation = rb.transform.position;
        var = false;
        var2 = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F)) // reseting the game
        {
            stop = false;
            rb.transform.position = startlocation;
            failtext.text = "";
        }
        if (rb.transform.position.x > 11 || rb.transform.position.x < 1 && !stop)
        {
            failtext.text = "out of bounds - You Failed"; // testing
            q = Time.time; // for not fucking up time
        }
        if (!stop) // calling main functions
        {
            changedir();
            //checkspace();
        }
        else // if the player fails the object should still lerp the the same value
        {
            if (rb.transform.position.x < 20 && rb.transform.position.x > -10)
            {
                currspeed = Mathf.Lerp(currspeed, 100 * speed, Time.deltaTime);
                check();
                move();
                speedtext.text = currspeed.ToString(); // testing
                q = Time.time; // for not fucking up time
            }
        }
    }
    private void checkspace()
    {
        float timeheld = 0;
        if (Input.GetKey(KeyCode.Space)) // if the player holds or taps space
        {
            if (!b) // to start counting time since the player tap or held the button
            {
                q = Time.time;
                b = true;
            }
            timeheld = Time.time - q;
            failtext.text = string.Format("{0:0.0000}", (Time.time - q).ToString()); // testing
        }
        if (Input.GetKeyUp(KeyCode.Space) || !Input.GetKey(KeyCode.Space)) // for reseting the counter when the player relesed the space bar
            b = false;
        if (timeheld > 1.2f) // to fail the player
        {
            speed = tspeed;
            stop = true;
            failtext.text = string.Format("{0:0.0000}", (Time.time - q).ToString()) + " - You Failed"; // testing
            time = 100f;
        }
    }
    private void move() // moves the player
    {
        Vector2 movment = new Vector2(transform.position.x + currspeed, transform.position.y);
        rb.transform.position = movment;
    }
    private void changedir() //to change dir (TODO - fix holding mechanic)
    {
        if (Input.GetKeyDown(KeyCode.Space)) // if the player tap the space button (the "if" statements are just to set the time and destenation
        {
            tspeed = 0;
            speed = -speed;
            time = 2f;

        }
        //if (Input.GetKey(KeyCode.Space) && Mathf.Abs(currspeed) >= 0.85f * Mathf.Abs(speed))// if the player is still holding the button
        //{
        //    Debug.Log("hey");
        //    if (!var)
        //    {
        //        var2 = true;
        //        currspeed = 0;
        //        var = true;
        //    }
        //    tspeed = currspeed;
        //    currspeed = Mathf.Lerp(currspeed, 0.35f * speed, Time.deltaTime * time); // changes the number
        //}
        //else
        //{
        //    var = false;
        //}
        //if (tspeed != 0 && !Input.GetKey(KeyCode.Space))
        //{
        //    Debug.Log("hey2");
        //    if (var2)
        //    {
        //        currspeed = 0;
        //        var2 = false;
        //        speed = -speed;
        //    }
        //    currspeed = Mathf.Lerp(currspeed, tspeed, Time.deltaTime * time);
        //}
        //if (!var)
            currspeed = Mathf.Lerp(currspeed, speed, Time.deltaTime * time); // changes the number
        check(); // floores the number
        move(); // moves player
        speedtext.text = currspeed.ToString(); // testing
    }
    void check() // to floor the number
    {
        if (speed == 0.2f && currspeed > 0.195 || currspeed > 0.2)
            currspeed = 0.2f;
        if (speed == -0.2f && currspeed < -0.195 || currspeed < -0.2)
            currspeed = -0.2f;
    }
}
