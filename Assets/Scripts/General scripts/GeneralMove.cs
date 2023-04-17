using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMove : MonoBehaviour
{
    [SerializeReference]
    protected float speed; // gameobject speed


    protected Camera cameraMain;
    protected Transform transform_; // the gameobject

    protected IntervalGenerator intervalGenerator_;


    public virtual void Start()
    {
        cameraMain = Camera.main;
        intervalGenerator_ = GameObject.Find("LevelGenerator").GetComponent<IntervalGenerator>();
        transform_ = GetComponent<Transform>();
        speed = intervalGenerator_.obsSpwnSpeed_;
    }

   protected void moveGeneral(Transform transform_)
    {
        Vector3 movment = new Vector3(transform_.position.x, transform_.position.y - (speed*Time.deltaTime), transform_.position.z);
        transform_.transform.position = movment;
        if (cameraMain.WorldToViewportPoint(transform_.position).y < -0.5f)
            Destroy(gameObject);
    }
    public virtual void Update()
    {
        moveGeneral(transform_);
    }
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}
