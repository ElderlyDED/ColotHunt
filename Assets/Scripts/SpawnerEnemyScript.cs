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
    [SerializeField] int StartEnemyCount; //Ќачальное колличесвто противников которое по€витс€ на прот€жении первой минуты
    [SerializeField] float EnemyPercent; //  олличество противников с процентом которые будут по€вл€етс€ в течении данной минуты
    [SerializeField] int SecondsForNextMinCountEnemy; // —четчик минут дл€ высчитывани€ нового, увеличенового кол во противника на след минуте
    [SerializeField] int PercentToNextMintSpawn; // ѕроцент противников который прибавитьс€ на след минуте
    [SerializeField] int SecStepSpawn; // шаг через который будут спавнитьс€ противники
    [SerializeField] int NowSecIntoSpawn; //  ол во секунд прошло внутри данного скрипта, 
    [SerializeField] int NextSpawnToSec; // —четчик следующего спавна
    [SerializeField] int EnemyFor10Sec;  //  олличество противников которые заспавн€тьс€ в течении этих дес€ти секунд
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
