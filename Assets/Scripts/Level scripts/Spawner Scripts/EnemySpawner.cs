using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    private Transform playerTransform_;

    private IntervalGenerator intervalGenerator_;

    private int maxEnemies; // number of max enemies
    public int currEnemies; // number of current enemies
    private float enemyDelay; // time between each enemy spawn
    private float currDelay; // current enemy spanwer timer
    private bool spawnEnemies; // true = can spawn enemies, false = cant spawn enemies
    private float spawnChance; // chance an enem

    public float maxSpeed_E;

    private GameMaster gameMaster;
    private bool stopGame;
    private void GameMasterDiff_OnDifficultyUpdate(object sender, DifficultyScript.OnDifficultyUpdateArgs e) // changes spawn chance, enemy spawn delay and max enemies
    {
        switch (e.diffLevel)
        {
            case 0:
                maxSpeed_E = 6;
                spawnEnemies = false;
                break;
            case 1:      
                maxEnemies = 1;
                enemyDelay = 10f;
                spawnChance = .25f;
                spawnEnemies = true;
                break;
            case 2:
                maxSpeed_E = 7;
                maxEnemies = 2;
                spawnChance = .3f;
                break;
            case 4:
                maxSpeed_E = 8;
                enemyDelay = 8f;
                spawnChance = .35f;
                break;
            case 5:
                maxSpeed_E = 9;
                maxEnemies = 3;
                spawnChance = .4f;
                break;
            case 6:
                maxSpeed_E = 10;
                enemyDelay = 7.5f;
                break;
            case 7:
                maxEnemies = 4;
                spawnChance = .45f;
                break;
            case 8:
                maxSpeed_E = 10;
                break;
            case 10:
                maxSpeed_E = 11;
                break;
        }
    }

    private void spawnEnemy()
    {
        Vector3 spawnPos = new Vector3(playerTransform_.position.x, 16, -.5f);
        if (Random.value > .7)
            spawnPos = new Vector3(Random.Range(-3.3f, 3.3f), 16, -.5f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    private bool checkSpawnConditions(float spawnChance)
    {
        return (spawnEnemies & currEnemies < maxEnemies && Random.value <= spawnChance);
    }
    private float tickTimer(ref float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
    void Start()
    {
        maxSpeed_E = 6;
        stopGame = false;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        gameMaster.OnStop += GameMaster_OnStop;
        intervalGenerator_ = GetComponent<IntervalGenerator>();
        DifficultyScript.OnDifficultyUpdate += GameMasterDiff_OnDifficultyUpdate;

    }

    private void GameMaster_OnStop(object sender, GameMaster.OnStopUpdateArgs e)
    {
        stopGame = e.stopGame;
    }

    private float elapsed = 0;
    void Update()
    {
        if (!stopGame)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                if (currDelay <= 0 && spawnEnemies)
                {
                    if (checkSpawnConditions(spawnChance))
                    {
                        spawnEnemy();
                        currEnemies++;
                        currDelay = enemyDelay;
                    }
                }
                else if (currEnemies < maxEnemies && currEnemies >= 1)
                {
                    if (checkSpawnConditions(spawnChance * 1.2f))
                    {
                        spawnEnemy();
                        currEnemies++;
                        currDelay = enemyDelay;
                    }
                }
            }
            if (currDelay >= 0 && spawnEnemies)
                tickTimer(ref currDelay);
        }
        //else
        //    Debug.Log("<color=Red> Game Losr </color>");
    }
}
