using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public bool spawnLaser = true;
    [SerializeField]
    private ShootBeam laserPrefab;
    private void Update()
    {
        spawnLaser = laserPrefab.alive;
        if(!spawnLaser)
            Destroy(gameObject, 1f);
    }

}
