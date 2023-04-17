using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalGenerator : MonoBehaviour
{
    #region interval delay variables
    private float maxDelay;

    private float minDelay;

    private float currDelay;

    private float timeSpawned;

    // diff levels {maxDelay, MinDelay}
    [SerializeField]
    private List<float[]> diffIntervalList = new List<float[]>() {
    new float[]{5.2f,2.2f},     //lvl 0 (00:10)
    new float[]{4.6f,1.9f},     //lvl 1 (00:30)
    new float[]{4.2f,1.7f},     //lvl 2 (00:55)
    new float[]{4f,1.6f},       //lvl 3 (1:25)
    new float[]{3.4f,1.4f},    //lvl 4 (2:00)
    new float[]{3.1f,1.2f},     //lvl 5 (2:20)
    new float[]{2.6f,1f},      //lvl 6 (2:45)
    new float[]{2.2f,.85f},     //lvl 7 (3:10)
    new float[]{2f,.65f},     //lvl 8 (3:45)
    new float[]{1.85f,.55f},     //lvl 9 (4:15)
    new float[]{1.7f,.55f}     //lvl 10 (5:00)
    };
    #endregion


    #region obs presantage variables

    private bool spawnSecond = false;

    private float breakPresantage = .45f;

    private float secondPresantage = .15f;

    private List<float[]> obsTypePrsntgDiffList = new List<float[]>
    {
    new float[]{.3f,.6f,.9f,1f},
    new float[]{.3f,.6f,.85f,1f},
    new float[]{.35f,.7f,.8f,.1f},
    };
    private float[] obsPresantage = new float[] { // type 1(small), type 2(mid), type 3(big) (sum is 100)
        40, 75, 100
    };
    private float[] obsTypePresantage = new float[]{ // logs, lines, gold, laser (sum is 100)
        .3f, .6f ,1f, 1f
    };

    #endregion

    #region obs prefabs 
    //public Obsticle[] obsticlesArray; // create new claas that has obsticle and liklyhood to spawn in it
    [SerializeField]
    private List<Spawnable> balls;
    [SerializeField]
    private List<Spawnable> breakableBalls;
    [SerializeField]
    private List<Spawnable> lines;
    [SerializeField]
    private List<Spawnable> breakableLines;
    [SerializeField]
    private List<Spawnable> gold;
    [SerializeField]
    private List<Spawnable> lasers;

    [SerializeField]
    public float obsSpwnSpeed_;

    #endregion

    #region laser spawn variables
    private bool spawningLaser = false;
    private LaserSpawner currLaser;
    private bool spawnScndLaser = false;
    private float laserCoolDown;
    #endregion

    private Camera mainCamera;

    #region Randomize prefab
    float getDelay()
    {
        return Random.Range(minDelay, maxDelay);
    }

    float getPosition(float min, float max)
    {
        return Random.Range(min, max); // screen width size
    }

    List<Spawnable> getBreakable(List<Spawnable> normals, List<Spawnable> breakables, float mult = 1)
    {
        spawnSecond = Random.value < secondPresantage;
        if (Random.value > breakPresantage * mult)
            return normals;
        return breakables;
    }

    List<Spawnable> getObsType() // chooses obs type by randome
    {
        if (spawnScndLaser)
        {
            spawnScndLaser = false;
            spawningLaser = true;
            return lasers;
        }
        float x = Random.value;
        if (x < obsTypePresantage[0]) // balls (normal or breakable)
            return getBreakable(balls, breakableBalls);
        if (x < obsTypePresantage[1]) // lines (normal or breakable)
            return getBreakable(lines, breakableLines);
        if (x < obsTypePresantage[2]) // gold
            return gold;
        if (x < obsTypePresantage[3] && laserCoolDown <= 0) // lasers
        {
            spawnScndLaser = (Random.value > 0.75f);
            spawningLaser = true;
            return lasers;
        }
        return gold;
    }
    List<Spawnable> getRandomeObsType(float mult = 1.4f, float num1 = .4f, float num2 = .8f, float num3 = 1f) // get randome obs exept lasers
    {
        float x = Random.value;
        if (x < num1)
            return getBreakable(balls, breakableBalls, mult);
        if (x < num2)
            return getBreakable(lines, breakableLines, mult);
        if (x < num3)
            return gold;
        return gold;
    }

    Spawnable getObs(List<Spawnable> prefabs) // chooses obs size by presantage (need to fix for more obs like in gold prefabs)
    {
        if (prefabs.Count > 3)
            return prefabs[Random.Range(0, prefabs.Count)];
        int prob = Random.Range(0, 100); // presantage base spawn
        Spawnable prefab = prefabs[0];
        if (prob < obsPresantage[0])
            prefab = prefabs[0];
        else if (prob < obsPresantage[1])
            prefab = prefabs[1];
        else if (prob < obsPresantage[2] && laserCoolDown <= 0)
            prefab = prefabs[2];
        return prefab;
    }
    #endregion

    void spawnObsticle()
    {
        Spawnable currSpwn = getObs(getObsType());
        if (spawningLaser)
        {
            currLaser = Instantiate(currSpwn.prefab, new Vector3(0, 11.7f, currSpwn.prefab.transform.position.z), Quaternion.identity).GetComponent<LaserSpawner>();
            return;
        }
        float x = getPosition(currSpwn.minPos, currSpwn.maxPos);
        Vector3 position = new Vector3(x, (mainCamera.orthographicSize * 2 + 7), 0f);
        Instantiate(currSpwn.prefab, position, Quaternion.identity);
        if (spawnSecond) // spawn second Obs
        {
            spawnSecond = false;
            Spawnable secondSpwn = getObs(getRandomeObsType(1.6f));
            float x2 = getPosition(secondSpwn.minPos, secondSpwn.maxPos);
            Vector3 position2 = new Vector3(x2, (mainCamera.orthographicSize * 2 + 7 + Random.Range(2.5f, 3.8f)), 0f);
            Instantiate(secondSpwn.prefab, position2, Quaternion.identity);

        }
    }

    private void diffSet(float obsSpwnSpeed = -1, float breakPresantage = -1, float secondPresantage = -1, int num = -1)
    {
        if (obsSpwnSpeed_ != -1)
            this.obsSpwnSpeed_ = obsSpwnSpeed;
        if (breakPresantage != -1)
            this.breakPresantage = breakPresantage;
        if (obsSpwnSpeed_ != -1)
            this.secondPresantage = secondPresantage;
        if (num != -1)
            this.obsTypePresantage = obsTypePrsntgDiffList[num];

    }
    private void GameMasterDiff_OnDifficultyUpdate(object sender, DifficultyScript.OnDifficultyUpdateArgs e) // interface that changes delay time for difficulty
    {
        maxDelay = diffIntervalList[e.diffLevel][0];
        minDelay = diffIntervalList[e.diffLevel][1];
        if (e.diffLevel < 4)
            spawnScndLaser = false;
        switch (e.diffLevel)
        {
            case 0: // until 10 sec mark
                break;
            case 1: // until 30 sec mark
                diffSet(6.5f);
                break;
            case 2: // until 55 sec mark
                //obsSpwnSpeed_ = 7f;
                //breakPresantage = .4f;
                //secondPresantage = .20f;
                //obsTypePresantage = obsTypePrsntgDiffList[0];
                diffSet(7f, .4f, .2f, 0);
                break;
            case 3: // until 1 min and 25 sec mark
                diffSet(8);
                break;
            case 4: // until 2 min mark
                secondPresantage = .25f;
                //diffSet(-1,-1, .25f);
                break;
            case 5: // until 2 min and 20 sec mark
                diffSet(8.5f, -1, -1, 1);
                //obsSpwnSpeed_ = 8.5f;
                //obsTypePresantage = obsTypePrsntgDiffList[1];
                break;
            case 6: // until 2 min and 45 sec mark
                diffSet(9, .35f);
                //obsSpwnSpeed_ = 9f;
                //breakPresantage = .35f;
                break;
            case 7: // until 3 min and 10 sec mark
                diffSet(9.5f, .3f, .3f, 2);
                //obsSpwnSpeed_ = 9.5f;
                //secondPresantage = .3f;
                //breakPresantage = .30f;
                //obsTypePresantage = obsTypePrsntgDiffList[2];
                break;
            case 8:// until 3 min and 45 sec mark
                diffSet(10, -1, .35f);
                //secondPresantage = .35f;
                //obsSpwnSpeed_ = 10f;
                break;
            case 9: // until 4 min and 15 sec mark
                diffSet(10.7f);
                //obsSpwnSpeed_ = 10.7f;
                break;
            case 10: // until 5 min mark
                diffSet(11.5f, -1, .4f);
                //secondPresantage = .4f;
                //obsSpwnSpeed_ = 11.5f;
                break;


        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        DifficultyScript.OnDifficultyUpdate += GameMasterDiff_OnDifficultyUpdate;
        StartCoroutine(gameStartCorutine());
    }
    private bool startGame = false;
    private IEnumerator gameStartCorutine()
    {
        yield return new WaitForSeconds(3.5f);
        startGame = true;
    }
    [SerializeField]
    private PowerupController powerupController;

    void Update()
    {
        if (startGame)
            SpawnObsitcleBehavior();
    }
    private void SpawnObsitcleBehavior()
    {
        if (laserCoolDown > 0 && currLaser == null)
            laserCoolDown = tickTimer(laserCoolDown);
        if (Time.time - timeSpawned > currDelay && !spawningLaser)
        {
            timeSpawned = Time.time;
            currDelay = getDelay();
            float x = Random.value;
            if (x < .8f)
                spawnObsticle();
            else
                powerupController.SpawnRandomepowerup(new Vector3(Random.Range(-3, 3), (mainCamera.orthographicSize * 2 + 7), -.52f));
        }
        if (currLaser != null)
        {
            laserCoolDown = Random.Range(8, 16);
            spawningLaser = currLaser.spawnLaser;
        }
    }
    private float tickTimer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
