using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableMove : MonoBehaviour
{
    private Transform transform_;
    private EnemyMove Parent;
    private Camera cameraMain;
    private float speed;
    void Start()
    {
        cameraMain = Camera.main;
        Parent = GetComponentInParent<EnemyMove>();
        transform_ = GetComponent<Transform>();
        speed = Parent.projectileSpeed;
    }
    protected void move(Transform transform_)
    {
        Vector3 movment = new Vector3(transform_.position.x, transform_.position.y - (speed * Time.deltaTime), transform_.position.z);
        transform_.transform.position = movment;
        if (cameraMain.WorldToViewportPoint(transform_.position).y < -0.5f)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        move(transform_);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
            Destroy(gameObject);
    }
}
