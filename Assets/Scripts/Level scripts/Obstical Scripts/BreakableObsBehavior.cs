using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObsBehavior : MonoBehaviour
{
    Rigidbody topPart;
    Transform transform_;
    GeneralMove generalMove;
    Collider tpcollider;
    void Start()
    {
        topPart = GetComponent<Rigidbody>();
        transform_ = transform;
        generalMove = GetComponent<GeneralMove>();
        tpcollider = GetComponent<Collider>();
    }
    void breakObs()
    {

        tpcollider.enabled = false;
        generalMove.enabled = false;
        transform_.Rotate(new Vector3(transform_.rotation.x, transform_.rotation.y, transform_.rotation.z + Random.Range(5, 16)));
        topPart.useGravity = true;
        topPart.velocity = Vector3.back *Random.Range(1.8f,2.5f) + Vector3.up*Random.Range(1.8f, 2.5f) + Vector3.left * Random.Range(-.8f, .8f);
        Destroy(gameObject, 1f);
    }
    void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.tag);
        if (other.CompareTag("Weapon"))
            breakObs();
    }
}
