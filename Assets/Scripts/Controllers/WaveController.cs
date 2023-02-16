using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    enum Levels
    {
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }

    [SerializeField]
    private GameObject Light;
    [SerializeField]
    private GameObject Armed;

    [SerializeField]
    public int InitialLevel = 1;
    
    [SerializeField]
    private EnemySpawnPoint spawnPoint;

    private bool startWave = false;
    [SerializeField]
    private Levels level;

    private List<Wave> waves;
    public int timeRemains = 0;
    public int timeout = 0;
    
    
    [SerializeField]
    public int WaveCounter { get; private set; }

    private bool canSpawn; //todo false if game just started
    public bool isStarted = false;

    [SerializeField]
    private float timeToSpawn = 2f; //seconds

    [SerializeField]
    private int skipCost;

    [SerializeField]
    private bool loopWaves;

    void Start()
    {
        if (level == Levels.MainMenu)
        {
            waves = SceneVars.current.MainMenuWaves;
        }
        if (level == Levels.Level1)
        {
            waves = SceneVars.current.Level1; //SceneVars.current.Level1;
        }
        if (level == Levels.Level2)
        {
            waves = SceneVars.current.Level2;
        }

        if (level == Levels.Level3)
        {
            waves = SceneVars.current.Level3;
        }
        if (level == Levels.Level4)
        {
            waves = SceneVars.current.Level4;
        }

        WaveCounter = 0;

        if (isStarted) StartWaves();


    }

    private void Awake()
    {
        
    }

    IEnumerator SpawnEnemies(Wave wave, int enemiesCount, float waitTime)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            var enemy = wave.Enemies[i];
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void StartWaves()
    {
        isStarted = true;
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        if (WaveCounter >= waves.Count)
        {
            if (loopWaves)
                WaveCounter = 0;
            else
            {
                
                StopAllCoroutines();
                StartCoroutine(CheckWin());
                yield break;
            }
        }
        var wave = waves[WaveCounter];
        int enemiesCount = wave.Enemies.Length;
        
        //float waitTime = (enemiesCount / 2) * timeToSpawn;
        float waitTime = timeToSpawn / enemiesCount;
        timeout = wave.WaveTimeoutSeconds;

        StartCoroutine(SpawnEnemies(wave, enemiesCount, waitTime));
        StartCoroutine(WaitSeconds(wave.WaveTimeoutSeconds, () => startWave = true));
        
        yield return new WaitUntil(() => startWave == true);
        StopAllCoroutines();
        WaveCounter++;
        startWave = false;
        timeRemains = WaveCounter + 1 <= waves.Count - 1 ? waves[WaveCounter + 1].WaveTimeoutSeconds : 0;
        CalculateSkipCost();
        StartCoroutine(StartSpawn());
    }
    
    public GameObject[] FindGameObjectsWithLayer (int layer) {
        var goArray = FindObjectsOfType(typeof(GameObject));
        var goList = new List<GameObject>();
        for (var i = 0; i < goArray.Length; i++) {
            if (goArray[i].GameObject().layer == layer) {
                goList.Add(goArray[i].GameObject());
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }

    private IEnumerator CheckWin()
    {
        while (true)
        {
            var enemies = FindGameObjectsWithLayer(LayerMask.NameToLayer("Enemy"));
            if (enemies.Length < 2)
            {
                break;
            }

            yield return new WaitForSeconds(.5f);
        }

        GameEvents.current.Win();
    }

    private void CalculateSkipCost()
    {
        skipCost = rootCalc(skipCost, WaveCounter + 1);
    }

    private int rootCalc(int x, int y)
    {
        return x + (int) (Math.Sqrt(y) * y);
    }

    private IEnumerator WaitSeconds(int timeout, Action clb)
    {
        timeRemains = timeout;
        for (int i = 0; i < timeout; i++)
        {
            timeRemains--;
            yield return new WaitForSeconds(1);
        }
        clb.Invoke();
    }

    public int GetSkipReward()
    {
        if (skipCost == 0 || timeout == 0 || timeRemains == 0) return 0;
        
        return (int)Math.Round(((float)skipCost * ((float)timeRemains / (float)timeout)));
    }
    
    public void SkipWave()
    {
        GlobalVars.current.AddMoney(GetSkipReward());
        startWave = true;
    }

    private void SpawnEnemy(Enemy enemy)
    {
        GameObject enemyPrefab;
        if (enemy.Type == EnemyType.Light) enemyPrefab = this.Light;
        else if (enemy.Type == EnemyType.Armed) enemyPrefab = this.Armed;
        else enemyPrefab = new GameObject();

        var o = spawnPoint.gameObject;
        var newEnemy = Instantiate(enemyPrefab, o.transform.position,
            o.transform.rotation);
        var sc = newEnemy.GetComponent<CreepController>();

        sc.creepCost = enemy.Cost + (int)Math.Sqrt(WaveCounter+1);
        // sc.creepCost = logCalc(WaveCounter+1, enemy.Cost);
        sc.creepLevel = (int)Math.Round((WaveCounter+1) * .15f) + InitialLevel;

        // sc.Health = logCalc(WaveCounter+1, (int)enemy.Health / 10, 1.2f);
        sc.health = enemy.Health + (int) (Math.Sqrt(WaveCounter + 1) * WaveCounter);
        sc.currentSpeed = enemy.Speed;
        var scale = 0.5f * enemy.Size;
        sc.gameObject.transform.localScale = new Vector3(scale, scale, 1f);

        sc.creepPathController = spawnPoint.SelectedPath;
    }

    private static int logCalc(int wave, int cost)
    {
        return (int)Math.Round(cost * (float)Math.Log(wave, 1.8f) + cost);
    }
    private static int logCalc(int wave, int cost, float newBase)
    {
        return (int)Math.Round(cost * (float)Math.Log(wave, newBase) + cost);
    }
    void Update()
    {
        
    }
}
