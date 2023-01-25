using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    [SerializeField] Vector2 OldPos;
    public bool EnemyMove;
    [Header("Boss Vars")]
    [SerializeField] bool ThisIsBoss;
    [SerializeField] bool GetDataForBoss;

    public float EnemyHP; // HP ���������� � ������� ������������ ��� ��� �������������(��� ��, ���������� ���� � �.�)
    [SerializeField] float StartEnemyHP; // HP ���������� ������� � ���� �������� ���������� ��� ���� ��������������
    [SerializeField] float EnemySpeed;
    [SerializeField] float stopDistance;
    [SerializeField] GameObject PlayerObject;
    [SerializeField] PlayerMechanics PlayerScript;

    [Header("Enemy Damage")]
    [SerializeField] GameObject DamageZone;
    [SerializeField] float DamageDelay;
    float DamageDelayULT;
    public float DamageForPlayer;

    [Header("Enemy Color")]
    [SerializeField] bool RedColorEnemyOn;
    [SerializeField] bool BlueColorEnemyOn;
    [SerializeField] bool GreenColorEnemyOn;
    public string EnemyColorNow;
    Rigidbody2D thisRB;

    [Header("Enemy Loot")]
    [SerializeField] GameObject Exp;
    [SerializeField] GameObject SmallPoision;
    [SerializeField] GameObject BigPoision;
    [SerializeField] int GetMoney;

    [Header("Chance Drop Loot Enemy")]
    [SerializeField] int ChanceExpDrop;
    [SerializeField] int ChanceDropPoision;
    [SerializeField] int ChanceBigPoisionDrop;

    [SerializeField] TimerScript TimerScript;
    [SerializeField] int NextStep; // ��� ������� ����� ������� ���������� ������� ������������� ����������
    [SerializeField] int TimeIntoEnemy; // ������ ������ ����������
    [SerializeField] float PercentPlusHpEnemy; // ������� ������� ����� ���������� �� 
    [SerializeField] float PercentPlusDamageEnemy; // ������� ������� ����� ���������� ����
    [SerializeField] float EnemyHPplusProcentUP;
    [SerializeField] float FinalDamageEnemy;
    [SerializeField] float Lucky;

    [SerializeField] Animator RedAnimator;
    [SerializeField] Animator BlueAnimator;
    [SerializeField] Animator GreenAnimator;

    [SerializeField] GameObject RedEnemy;
    [SerializeField] GameObject BlueEnemy;
    [SerializeField] GameObject GreenEnemy;

    // ��������� ������
    void Start()
    {
        ChangeRandomColorEnemy();
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = PlayerObject.GetComponent<PlayerMechanics>();
        DamageZone.SetActive(false);
        thisRB = GetComponent<Rigidbody2D>();
        TimerScript = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        EnemyHPplusProcentUP = StartEnemyHP;
        GetLuckyPlayer();
        RedColorEnemyOn = false;
        BlueColorEnemyOn = false;
        GreenColorEnemyOn = false;
    }
    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        EnemyDie();
        UpSpecsFromTime();
        IfBoss();
        OldPos = new Vector2(transform.position.x, transform.position.y);
    }
    void FixedUpdate()
    {
        DelayDamageTimer();
    }
    // ��������� ������

    // ����� ������ ����� ��� ����������
    void ChangeRandomColorEnemy()
    {
        int randonINT = Random.Range(0, 100);
        if (randonINT >= 0 && randonINT <= 33)
        {
            RedColorEnemyOn = true;
            RedEnemy.SetActive(true);
            BlueEnemy.SetActive(false);
            GreenEnemy.SetActive(false);
           // GetComponent<SpriteRenderer>().color = Color.red;
            EnemyColorNow = "red";
        }
        else if (randonINT >= 34 && randonINT <= 66)
        {
            BlueColorEnemyOn = true;
            RedEnemy.SetActive(false);
            BlueEnemy.SetActive(true);
            GreenEnemy.SetActive(false);
           // GetComponent<SpriteRenderer>().color = Color.blue;
            EnemyColorNow = "blue";
        }
        else if (randonINT >= 67 && randonINT <= 100)
        {
            GreenColorEnemyOn = true;
            RedEnemy.SetActive(false);
            BlueEnemy.SetActive(false);
            GreenEnemy.SetActive(true);
           // GetComponent<SpriteRenderer>().color = Color.green;
            EnemyColorNow = "green";
        }
    }
    // ����� ������ ����� ��� ����������

    // ����� ������������ ����������
    void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, PlayerObject.transform.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerObject.transform.position, EnemySpeed * Time.deltaTime);
        }
        else
        {
            transform.position = this.transform.position;
        }
    }
    // ����� ������������ ����������

    // ����� ����� ��� ��������� ����� �� ������
    void DelayDamageTimer()
    {
        if (DamageDelayULT <= 0)
        {
            DamageZone.SetActive(true);
            transform.Find("DamageZone").GetComponent<DamageZoneScript>().DamageForPlayer = DamageForPlayer;
            DamageDelayULT = DamageDelay;
        }
        else
        {
            DamageZone.SetActive(false);
            DamageDelayULT -= Time.deltaTime;
        }
    }

    // ����� ����� ��� ��������� ����� �� ������

    // ������������ ���������� ��� ��������� �����
    public void PushAway(Vector2 PushFrom ,float pushPower)
    {
        if (thisRB == null || pushPower == 0)
        {
            return;
        }

        var pushDirection = (PushFrom = transform.position).normalized;
        thisRB.AddForce(pushDirection * pushPower, ForceMode2D.Impulse);
        StartCoroutine(StopPushAway());
    }
    IEnumerator StopPushAway()
    {
        yield return new WaitForSeconds(0.3f);
        thisRB.Sleep();
        thisRB.WakeUp();
    }
    // ������������ ���������� ��� ��������� �����

    // ������ ���������� � ���������� ����
    void EnemyDie()
    {
        if (EnemyHP <= 0)
        {
            SpawnExpDie();
            SpawnPoisionDie();
            Destroy(this.gameObject);
            PlayerScript.DieEnemyCount += 1;
            if (ThisIsBoss == true)
            {
                int randChace = Random.Range(1, 1000);
                if (randChace <= 5)
                {
                    PlayerScript.MoneyCount += GetMoney;
                }
            }
        }
        else
        {
            return;
        }
    }
    void SpawnExpDie()
    {
        int RandomInt = Random.Range(0, 100);
        if (RandomInt <= ChanceExpDrop)
        {
            Instantiate(Exp, transform.position, transform.rotation);
        }
        else
        {
            return;
        }
    }

    void SpawnPoisionDie()
    {
        int randomIntPoisionDrop = Random.Range(0, 100);
        if (randomIntPoisionDrop <= ChanceDropPoision)
        {
            int RandomInt = Random.Range(0, 100);
            if (RandomInt >= ChanceBigPoisionDrop) // ������ ���� ��������� �������� ����� ������ ��� ��������� ����� ������� � ����� ������ ���� ���� ��������� ������� �� �� ������� �����
            {
                Instantiate(SmallPoision, transform.position, transform.rotation);
            }
            else if (RandomInt < ChanceBigPoisionDrop)
            {
                Instantiate(BigPoision, transform.position, transform.rotation);
            }
        }
        else
        {
            return;
        }
        
    }
    // ������ ���������� � ���������� ����

    // �������� ���������� �� �������
    void UpSpecsFromTime()
    {
        TimeIntoEnemy = TimerScript.ULTseconds;
        if (TimeIntoEnemy == NextStep)
        {
            NextStep += 60;
            EnemyHPplusProcentUP = EnemyHPplusProcentUP + (EnemyHPplusProcentUP / 100 * PercentPlusHpEnemy);
            EnemyHP += (EnemyHPplusProcentUP - StartEnemyHP);
            DamageForPlayer = DamageForPlayer + (DamageForPlayer / 100 * PercentPlusDamageEnemy);
        }
    }
    // �������� ���������� �� �������

    // ��������� ����� �� ������(�������� ���� ��������)
    void GetLuckyPlayer()
    {
        Lucky = PlayerScript.LuckyPrefs;
        ChanceExpDrop += (int)Lucky;
        ChanceBigPoisionDrop += (int)Lucky;
        ChanceDropPoision += (int)Lucky;
    }
    // ��������� ����� �� ������(�������� ���� ��������)

    void IfBoss()
    {
        if (ThisIsBoss == true)
        {
            
            if (GetDataForBoss == false)
            {
                GetDataForBoss = true;
                EnemyHP = PlayerPrefs.GetFloat("HpEndRun");
                DamageForPlayer = PlayerPrefs.GetFloat("DamageEndRun");
                EnemySpeed = PlayerPrefs.GetFloat("SpeedInRun") - 1;
                
            }
            else
            {
                return;
            }
        }
    }

    void Animations()
    {
        //RedAnimator.SetBool("Walk", EnemyMove);
        //BlueAnimator.SetBool("Walk", EnemyMove);
        //GreenAnimator.SetBool("Walk", EnemyMove);
    }

    void CheckWAalk()
    {

        if (OldPos != new Vector2(transform.position.x, transform.position.y))
        {
            EnemyMove = true;
        }
        else
        {
            EnemyMove = false;
        }
    }
}
