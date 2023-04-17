using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMoveTest : GeneralMove
{
    
    //[SerializeField]
    //private GameObject scoreCollider;
    public override void Start() // mainly setting up all of the variables and objects
    {
base.Start();
        int x = getNewRotation();
        transform_.transform.Rotate(x, -90, 90);
        //Instantiate(scoreCollider,new Vector3(0,transform_.position.y,-5f),Quaternion.identity);
    }
    public override void Update()
    {
        moveGeneral(transform_);
    }
    int getNewRotation()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                return 90;
            case 1:
                return 45;
            case 2:
                return -45;
            case 3:
                return 0;
        }
        return 90;
    }
}
