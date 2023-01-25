using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyScript : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] TimerScript timerScript;
    [Header("Boss Spawn")]
    [SerializeField] int TimeToSpawnBoss;
    [SerializeField] GameObject BossPref;
    [SerializeField] bool BossSpawning;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject SpiderPref;
    [SerializeField] GameObject BatPref;
    [SerializeField] GameObject SkeletonPref;
    [SerializeField] GameObject PlayerEnemyPref;

    [Header("Spawn Settings")]
    [SerializeField] int StartEnemyCount; //��������� ����������� ����������� ������� �������� �� ���������� ������ ������
    [SerializeField] float EnemyPercent; // ����������� ����������� � ��������� ������� ����� ���������� � ������� ������ ������
    [SerializeField] int SecondsForNextMinCountEnemy; // ������� ����� ��� ������������ ������, ������������� ��� �� ���������� �� ���� ������
    [SerializeField] int PercentToNextMintSpawn; // ������� ����������� ������� ����������� �� ���� ������
    [SerializeField] int SecStepSpawn; // ��� ����� ������� ����� ���������� ����������
    [SerializeField] int NowSecIntoSpawn; // ��� �� ������ ������ ������ ������� �������, 
    [SerializeField] int NextSpawnToSec; // ������� ���������� ������
    [SerializeField] int EnemyFor10Sec;  // ����������� ����������� ������� ������������ � ������� ���� ������ ������
    void Start()
    {
        timerScript = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        EnemyPercent += StartEnemyCount;
    }
    void Update()
    {
        SpawnForSec();
        EnemyCountSecAndMinute();
        SpawnBossInDieTimePlayer();
    }

    void SpawnForSec()
    {
        NowSecIntoSpawn = timerScript.ULTseconds;
        if (NextSpawnToSec == NowSecIntoSpawn)
        {
            NextSpawnToSec += SecStepSpawn;
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < EnemyFor10Sec; i++)
        {
            int randMob = Random.Range(0, 100);
            if (randMob <= 33)
            {
                SpawnTypeMob(SpiderPref);
            }
            else if(randMob >= 34 && randMob <= 66)
            {
                SpawnTypeMob(BatPref);
            }
            else if (randMob >= 67)
            {
                SpawnTypeMob(SkeletonPref);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SpawnTypeMob(GameObject enemyType)
    {
        int randPoint = Random.Range(0, 7);
        Instantiate(enemyType, SpawnPoints[randPoint].transform.position, SpawnPoints[randPoint].transform.rotation);
    }

    void EnemyCountSecAndMinute()
    {

        EnemyFor10Sec =  (int)EnemyPercent / 6;
        if(SecondsForNextMinCountEnemy == NowSecIntoSpawn)
        {
            SecondsForNextMinCountEnemy += 60;
            EnemyPercent = EnemyPercent + (EnemyPercent / 100 * PercentToNextMintSpawn);
            if (EnemyPercent >= 204)
            {
                EnemyPercent = 204;
            }
        }
    }
    void SpawnBossInDieTimePlayer()
    {
        int GetTimeInPrefs = PlayerPrefs.GetInt("TimeDieInRun");
        TimeToSpawnBoss = GetTimeInPrefs;
        if (TimeToSpawnBoss == timerScript.ULTseconds && BossSpawning == false)
        {
            SpawnTypeMob(BossPref);
            BossSpawning = true;
        }
    }
}
