using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeReference]
    private float speed; // gameobject speed


    private Camera cameraMain;
    private Transform transform_; // the gameobject

    private BackgroundController Background_Controller;

    public Transform endOfPlain;
    private Transform followPoint;

    public void setFollowDot(Transform point)
    {
        followPoint = point;
    }

    public virtual void Start()
    {
        cameraMain = Camera.main;
        Background_Controller = GameObject.Find("Background").GetComponent<BackgroundController>();
        transform_ = GetComponent<Transform>();
        //if (transform_.rotation.x == 0)
        //    transform_.Rotate(-90, 0, 0);
        speed = Background_Controller.speed_BG;
        endOfPlain = transform_.GetChild(0).transform;
        Background_Controller.checkTopX(transform_.localPosition.y, gameObject);
    }


    private void move()
    {
        Vector3 movment = new Vector3(transform_.position.x, transform_.position.y - (speed * Time.deltaTime), transform_.position.z);
        transform_.transform.position = movment;
        if (cameraMain.WorldToViewportPoint(transform_.position).y < -0.5f)
            Destroy(gameObject);
    }
    private void followPointMove()
    {
        transform_.transform.position =   new Vector3(transform_.position.x, 5.4f+ followPoint.transform.position.y, transform_.position.z);
    }
    void Update()
    {
        //if (followPoint != null)
        //    followPointMove();
        //else
        //{
            speed = Background_Controller.speed_BG;
            move();
        //}
    }
}
