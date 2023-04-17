using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    private Rigidbody rb_;
    private Transform transform_;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float currSpeed;
    [SerializeField]
    private float timer;
    private bool haveShot;
    [SerializeField]
    private EnemySpawner spawner;

    private Vector3 lastPos;

    public float projectileSpeed { get; set; } = 15;
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<Transform>();
        spawner = GameObject.Find("LevelGenerator").GetComponent<EnemySpawner>();
        maxSpeed = spawner.maxSpeed_E;
        timer = 4;
        transform_ = transform.GetComponent<Transform>();
        rb_ = transform_.GetComponent<Rigidbody>();
    }
    private void tickTimer()
    {
        timer -= Time.deltaTime;
    }
    private void Move()
    {
        float x = transform_.position.x;
        currSpeed = Mathf.Lerp(currSpeed, maxSpeed, Time.deltaTime);
        Vector3 newPosition;
        if (x < Player.position.x)
        {
            newPosition = new Vector3(x + currSpeed * Time.deltaTime, transform.position.y, transform_.position.z);
        }
        else
        {
            newPosition = new Vector3(x - currSpeed * Time.deltaTime, transform.position.y, transform_.position.z);
        }
        if (Mathf.Abs(x - Player.position.x) > .1)
        {
            rb_.MovePosition(newPosition);
        }
    }
    private void shoot()
    {
        Instantiate(arrow, transform_.position +new Vector3(0,0,-.5f), Quaternion.identity,transform_);
        spawner.currEnemies--;
        Destroy(gameObject, .8f);
        //GetComponent<Collider>().enabled = false;
        //rb_.detectCollisions = false;
    }
    void Update()
    {
        if (timer >= 0)
        {
            tickTimer();
            if (timer <= .8)
            {
                transform_.position = lastPos;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                lastPos = transform_.position;
                Move();
            }

        }
        else
        {
            transform_.position = lastPos;
            if (!haveShot)
            {
                shoot();
                haveShot = true;
            }
        }
    }
}
