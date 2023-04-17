using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTest : MonoBehaviour
{
    public void spawnLaser(GameObject laserPrefab)
    { 
        Instantiate(laserPrefab, new Vector3(4,11,-1),Quaternion.identity);
    }


}
