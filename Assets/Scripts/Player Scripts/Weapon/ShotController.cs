using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShotController : MonoBehaviour
{
    public float shotSpeed;
    public float shotSize;
    public GameObject shotPrefab { get; private set; }

    protected Transform transform_;
    protected Camera cameraMain;
    protected Rigidbody rb;
    protected virtual void Start()
    {
        cameraMain = Camera.main;
        transform_ = GetComponent<Transform>();
        shotPrefab = gameObject;
        ChangeSize();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * shotSpeed;
    }
    private void ChangeSize()
    {
        transform_.localScale = new Vector3(shotSize, shotSize, shotSize);
    }
    protected virtual void Update()
    {
        checkOutOfScreen();
    }
    protected void checkOutOfScreen()
    {
        if (cameraMain.WorldToViewportPoint(transform_.position).y > 1.5f)
            Destroy(gameObject);
    }

}
