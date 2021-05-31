using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    private enum Options { Endless, Waves }

    [Header("Settings")]
    [SerializeField] private Options Option = Options.Endless;
    public int Seed = 0;
    public bool SetRandomSeed = true;

    [Header("Object Pool")]
    public ObjectPool ObjectPool = null;

    [Header("Enemies")]
    public EnemySpawnHandler_Enemy[] Enemies = null;

    [Header("Settings - Endless")]
    public float SpawnRate = 5; // Seconds between spawning
    public float SpawnRateEncrease = 0.05f; // Decrease time between spawning per sec
    public bool RandomEnemy = true;

    [Header("Settings - Waves")]
    public EnemySpawnHandler_WaveSettings Waves = null;
    public bool WaitForAllEnemiesKilled = true;

    private float Timer = 0;
    private int CurrentWave = 0;
    private int CheckWave = 999;
    private float TimerBetweenWaves = 0;
    private float SpawnSpeed = 0;

    private int _EnemiesAlive = 0;

    public float spawnRange;

    private void Start()
    {
        if (SetRandomSeed)
            Random.InitState(Random.Range(0, 10000));
        else
            Random.InitState(Seed);

        if (Waves.WaveOption == EnemySpawnHandler_WaveSettings.WaveOptions.Generate)
            StartCoroutine(GenerateWaves());
        if (Waves.WaveOption == EnemySpawnHandler_WaveSettings.WaveOptions.Endless)
        {
            Waves.WaveAmount = 1;
            StartCoroutine(GenerateWaves());
            StartCoroutine(GenerateWaves(1));
        }


    }

    void Update()
    {
        Timer += 1 * Time.deltaTime;

        switch (Option)
        {
            case Options.Endless:
                Update_Endless();
                break;
            case Options.Waves:
                StartCoroutine(Update_Waves());
                break;
        }
    }

    //Update
    private void Update_Endless()
    {
        if (Timer >= SpawnRate)
        {
            int randomenemyid = 0;
            if (RandomEnemy)
                randomenemyid = Random.Range(0, Enemies.Length);
            Spawn(randomenemyid);
            Timer = 0;
        }
        SpawnRate -= SpawnRateEncrease * Time.deltaTime;
    }
    private IEnumerator Update_Waves()
    {
        if (CurrentWave < Waves.Waves.Count)
        {
            if (CheckWave != CurrentWave)
            {
                if (WaitForAllEnemiesKilled)
                {
                    EnemiesAlive();

                    if (_EnemiesAlive == 0)
                        TimerBetweenWaves += 1 * Time.deltaTime;
                }
                else
                    TimerBetweenWaves += 1 * Time.deltaTime;

                if (TimerBetweenWaves >= Waves.TimeBetweenWaves)
                {
                    TimerBetweenWaves = 0;
                    CheckWave = CurrentWave;
                    SpawnSpeed = Waves.Waves[CurrentWave].SpawnDuration / Waves.Waves[CurrentWave].TotalEnemies;
                    if (Waves.WaveOption == EnemySpawnHandler_WaveSettings.WaveOptions.Endless)
                        StartCoroutine(GenerateWaves(CurrentWave + 2));
                }
            }
            else
            {
                //Spawn
                if (Waves.Waves[CurrentWave].TotalEnemies > 0)
                {
                    if (Timer > SpawnSpeed)
                    {
                        bool spawncheck = false;
                        while (!spawncheck)
                        {
                            int spawnid = Random.Range(0, Enemies.Length);
                            if (Waves.Waves[CurrentWave].EnemyID[spawnid] > 0)
                            {
                                Spawn(spawnid);
                                Waves.Waves[CheckWave].EnemyID[spawnid]--;
                                Waves.Waves[CurrentWave].TotalEnemies--;
                                spawncheck = true;
                            }

                            yield return null;
                        }
                        Timer = 0;
                    }
                }
                else
                {
                    CurrentWave++;
                }
            }
        }
    }

    //Generate Waves
    private IEnumerator GenerateWaves(int waveid = 0)
    {
        int enemytypes = Enemies.Length;
        for (int i = 0; i < Waves.WaveAmount; i++)
        {
            EnemySpawnHandler_Wave newwave = new EnemySpawnHandler_Wave();
            int enemyamount = 0;

            if (waveid == 0)
                enemyamount = Mathf.RoundToInt(Waves.EnemyAmount * ((Waves.EnemyIncreaseAmount * i) + 1));
            else
                enemyamount = Mathf.RoundToInt(Waves.EnemyAmount * ((Waves.EnemyIncreaseAmount * waveid) + 1));

            //Set enemy amount
            newwave.EnemyID = new int[enemytypes];
            int checkenemyamount = 0;
            newwave.TotalEnemies = enemyamount;

            while (checkenemyamount < enemyamount)
            {
                for (int j = 0; j < enemytypes; j++)
                {
                    if (Enemies[j].StartWave <= i)
                    {
                        int addamount = 0;
                        if (enemyamount < 2)
                            addamount = Random.Range(0, enemyamount);
                        else
                            addamount = Random.Range(0, Mathf.RoundToInt(enemyamount * 0.5f));

                        if (enemyamount > checkenemyamount + addamount)
                        {
                            newwave.EnemyID[j] += addamount;
                            checkenemyamount += addamount;
                        }
                        else
                        {
                            newwave.EnemyID[j] += enemyamount - checkenemyamount;
                            checkenemyamount = enemyamount;
                            continue;
                        }
                    }
                }

                yield return null;
            }
            Waves.Waves.Add(newwave);
        }
    }

    public void Spawn(int enemyid)
    {
        GameObject obj = ObjectPool.GetObjectPrefabName(Enemies[enemyid].EnemyPrefab.name, false);
    //    obj.transform.position = SpawnLocations[spawnid].position;
      obj.transform.position = transform.position + Random.insideUnitSphere * spawnRange;
        obj.SetActive(true);
    }
    private void EnemiesAlive()
    {
        _EnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}

[System.Serializable]
public class EnemySpawnHandler_Enemy
{
    public string EnemyName;
    public GameObject EnemyPrefab;

    [Header("Settings")]
    public int StartWave;
}

[System.Serializable]
public class EnemySpawnHandler_WaveSettings
{
    public enum WaveOptions { Endless, Manually, Generate }
    public WaveOptions WaveOption;

    [Header("Endless")]
    public float EnemyIncreaseAmount;

    [Header("Manual")]
    public List<EnemySpawnHandler_Wave> Waves;

    [Header("Generate")]
    public int WaveAmount;
    public int EnemyAmount;

    [Header("Other")]
    public float TimeBetweenWaves;
}

[System.Serializable]
public class EnemySpawnHandler_Wave
{
    public int[] EnemyID;
    public float SpawnDuration = 5;

    public int TotalEnemies;
}