using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMechanics : MonoBehaviour
{
    public bool PlayerWalk;
    public bool PlayerAttack;
    [SerializeField] Vector2 OldPos;

    [Header("Money")]
    public int MoneyCount;

    [Header("Stats For Boss")]
    [SerializeField] float MaxDamageInWeapon;
    [SerializeField] float MaxHpInRun;
    [SerializeField] float MaxSpeedInRun;
    [SerializeField] int DieTimePlayer;

    [Header("Movment")]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSpeedPercent;
    Rigidbody2D thisRB;
    public Vector2 PlayerDirection; // Переменная которая запоминает направления игрока по нажатиям кнопок
    public Vector2 LastPlayerDirect;
    Vector2 moveVelocity;

    [Header("Player HP")]
    [SerializeField] TextMeshProUGUI HPtext;
    public float PlayerHP;

    [Header("Sword Punch")]
    [SerializeField] GameObject SwordPuncObject;
    [SerializeField] bool ReadySwordPunch;

    [Header("Sword Around Punch")]
    [SerializeField] GameObject SwordAroundPunchObject;
    [SerializeField] bool ReadySwordAroundPunch;

    [Header("Sword Wave Punch")]
    [SerializeField] GameObject SwordWavePunchObject;
    [SerializeField] bool ReadySwordWavePunch;

    [Header("Sword Damage")]
    [SerializeField] SwordAroundPunchScript AroundPunchScript;
    [SerializeField] SwordPunchAttack SwordPunchScript;

    public float SwordPunchDamageOnColor;
    public float SwordPunchDamageOffColor;

    public float SwordAroundPunchDamageOnColor;
    public float SwordAroundPunchDamageOffColor;

    public float SwordWavePunchDamageOnColor;
    public float SwordWavePunchDamageOffColor;
    public float SwordWavePunchDamegePercentOnColor;
    public float SwordWavePunchDamegePercentOffColor;

    [Header("Sword Delays")]
    [SerializeField] float DelaySwordPunch;
    [SerializeField] float DelaySwordAroundPunch;
    [SerializeField] float DelaySwordWavePunch;
    [SerializeField] float DelaySPpercent;
    [SerializeField] float DelaySAPpercent;
    [SerializeField] float DelaySWPpercent;

    [Header("Exp Plaayer")]
    public float AllExpPlayer;
    public float LimitLvl;
    [SerializeField] float NextLimitLvl;
    [SerializeField] float PercentForNextLimitLvl;
    [SerializeField] int PlayerLvl;

    [Header("Score for LVL UP Menu")]
    public int ScoreCount;

    [Header("Percent's specs LVL")]
    public float damagePercentSpecs;
    public float moveSpeedPercentSpecs;
    public float attackSpeedPercentSpecs;
    public float SwitchColorSpeedPercentSpecs;

    [Header("Color Mechanics Var")]
    [SerializeField] float DelayColorSwitch;
    [SerializeField] float DelayColorSwitchPercent;
    [SerializeField] bool RedColorOn;
    [SerializeField] bool BlueColorOn;
    [SerializeField] bool GreenColorOn;
    public string ColorNowPlayer;

    [Header("PlayerPrefs Specs")]
    [SerializeField] int DamagePrefs;
    [SerializeField] int MoveSpeedPrefs;
    public int LuckyPrefs;
    [SerializeField] int AttackSpeedPrefs;
    public int ExpPrefs;
    public int HealPrefs;

    [Header("animations")]
    [SerializeField] Animator animator;
    [SerializeField] Animator AnimatorGreen;
    [SerializeField] Animator AnimatorRed;

    [SerializeField] GameObject PlayerBlue;
    [SerializeField] GameObject PlayerGreen;
    [SerializeField] GameObject PlayerRed;

    [Header("ColorSwitcherShard")]
    [SerializeField] GameObject ShardRed;
    [SerializeField] GameObject ShardBlue;
    [SerializeField] GameObject ShardGreen;

    //[Header("Player Prefs Specs Percent")]
    //[SerializeField] int DamagePrefsPercent;
    //[SerializeField] int MoveSpeedPrefsPercent;
    //[SerializeField] int LuckyPrefsPlus;
    //[SerializeField] int AttackSpeedPrefsPercent;
    //[SerializeField] int ExpPrefsPlus;
    //[SerializeField] int HealPrefsPlus;
    [SerializeField] GameObject DieMenu;

    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI LvlText;
    [SerializeField] TextMeshProUGUI DieEnemy;
    public int DieEnemyCount;
    [SerializeField] Slider ExpSlide;
    
    // Системные методы
    void Start()
    {
        thisRB = GetComponent<Rigidbody2D>();
        HPtext = GameObject.FindGameObjectWithTag("UIHP").GetComponent<TextMeshProUGUI>();
        ReadySwordPunch = true;
        SwordPuncObject.SetActive(false);
        ReadySwordAroundPunch = true;
        SwordAroundPunchObject.SetActive(false);
        AroundPunchScript = SwordAroundPunchObject.GetComponent<SwordAroundPunchScript>();
        SwordPunchScript = SwordPuncObject.GetComponent<SwordPunchAttack>();
        RedColorOn = true;
        BlueColorOn = false;
        GreenColorOn = false;
        
    }
    void Update()
    {
        MovePlayer();
        HpNow();
        AttackDirection();
        SwordPunch();
        SwordAroundPunch();
        SwordWavePunch();
        DamageManager();
        LVLmanager();
        SpeedMoveManager();
        AttackSpeedManager();
        ColorSwitchSpeedManager();
        ColorSwircher();
        GetPrefsOnMenu();
        MaxHpInThisRun();
        DiePlayer();
        CheckWAalk();
        //Animations();
        OldPos = new Vector2(transform.position.x, transform.position.y);
        MoneyText.text = MoneyCount.ToString();
        LvlText.text = PlayerLvl.ToString();
        DieEnemy.text = DieEnemyCount.ToString();

 
    }
    private void FixedUpdate()
    {
        FixedMovePlayer();
    }
    // Системные методы

    // Механика передвижения
    void MovePlayer()
    {
        PlayerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Считываем нажатие на кнопки, для передвижения и заданем в Vector2
        moveVelocity = PlayerDirection.normalized * moveSpeedPercent; // Скорость передвижения 
    }
    void FixedMovePlayer()
    {
        thisRB.MovePosition(thisRB.position + moveVelocity * Time.fixedDeltaTime); // Передвижения персонажа с задаными выше параметрами
    }
    // Механика передвижения

    // Механика HP
    void HpNow()
    {
        PlayerHP = (int)PlayerHP;
        HPtext.text = PlayerHP.ToString();
    }
    // Механика HP

    // Механика направления удара
    void AttackDirection()
    {
        float offsetZoneX;
        float offsetZoneY;
        if (PlayerDirection.x > 0)
        {
            offsetZoneX = 1;
            offsetZoneY = 0;
            OffsetZoneDamage(offsetZoneX, offsetZoneY);
            LastPlayerDirect = new Vector2(offsetZoneX, offsetZoneY);
        }
        else if (PlayerDirection.x < 0)
        {
            offsetZoneX = -1;
            offsetZoneY = 0;
            OffsetZoneDamage(offsetZoneX, offsetZoneY);
            LastPlayerDirect = new Vector2(offsetZoneX, offsetZoneY);
        }
        else if (PlayerDirection.y > 0)
        {
            offsetZoneX = 0;
            offsetZoneY = 1;
            OffsetZoneDamage(offsetZoneX, offsetZoneY);
            LastPlayerDirect = new Vector2(offsetZoneX, offsetZoneY);
        }
        else if (PlayerDirection.y < 0)
        {
            offsetZoneX = 0;
            offsetZoneY = -1;
            OffsetZoneDamage(offsetZoneX, offsetZoneY);
            LastPlayerDirect = new Vector2(offsetZoneX, offsetZoneY);
        }

        void OffsetZoneDamage(float offsetX, float offsetY)
        {
            SwordPuncObject.transform.position = new Vector2(this.transform.position.x + offsetX, this.transform.position.y + offsetY) + PlayerDirection;
        }
    }
    // Механика направления удара

    // Механика удара мечем
    void SwordPunch()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           
            if (ReadySwordPunch == true)
            {
                
                PlayerAttack = true;
                ReadySwordPunch = false;
                SwordPuncObject.SetActive(true);
                StartCoroutine(OffSwordPunchZone());
                StartCoroutine(SwordPunchDelay());
            }
        }
    }
    IEnumerator SwordPunchDelay()
    {
        yield return new WaitForSeconds(DelaySPpercent);
        ReadySwordPunch = true;
    }
    IEnumerator OffSwordPunchZone()
    {
        yield return new WaitForSeconds(0.4f);
        SwordPuncObject.SetActive(false);
    }
    // Механика удара мечем

    // Механика кругового удара
    void SwordAroundPunch()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(ReadySwordAroundPunch == true)
            {
                
                ReadySwordAroundPunch = false;
                SwordAroundPunchObject.SetActive(true);
                StartCoroutine(OffSwordAroundPunchZone());
                StartCoroutine(SwordAroundPunchDelay());
            }
        }
    }
    IEnumerator SwordAroundPunchDelay()
    {
        yield return new WaitForSeconds(DelaySAPpercent);
        ReadySwordAroundPunch = true;
    }
    IEnumerator OffSwordAroundPunchZone()
    {
        yield return new WaitForSeconds(0.4f);
        SwordAroundPunchObject.SetActive(false);
    }
    // Механика кругового удара

    // Механика волнового удара
    void SwordWavePunch()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (ReadySwordWavePunch == true)
            {
                ReadySwordWavePunch = false;
                StartCoroutine(SwordWavePunchDelay());
                float offsetZoneX;
                float offsetZoneY;
                if (PlayerDirection.x > 0)
                {
                    offsetZoneX = 1;
                    offsetZoneY = 0;
                    spawnWavePunch(offsetZoneX, offsetZoneY);
                }
                else if (PlayerDirection.x < 0)
                {
                    offsetZoneX = -1;
                    offsetZoneY = 0;
                    spawnWavePunch(offsetZoneX, offsetZoneY);
                }
                else if (PlayerDirection.y > 0)
                {
                    offsetZoneX = 0;
                    offsetZoneY = 1;
                    spawnWavePunch(offsetZoneX, offsetZoneY);
                }
                else if (PlayerDirection.y < 0)
                {
                    offsetZoneX = 0;
                    offsetZoneY = -1;
                    spawnWavePunch(offsetZoneX, offsetZoneY);
                }
                else if (PlayerDirection.x == 0 && PlayerDirection.y == 0)
                {
                    SwordWavePunchObject.GetComponent<SwordWavePunchScript>().SWPonColorDamage = SwordWavePunchDamegePercentOnColor;
                    SwordWavePunchObject.GetComponent<SwordWavePunchScript>().SWPoffColorDamage = SwordWavePunchDamegePercentOffColor;
                    Instantiate(SwordWavePunchObject, new Vector2(transform.position.x, transform.position.y) + LastPlayerDirect, transform.rotation);
                }
            }
        }
    }
    void spawnWavePunch(float offsetX, float offsetY)
    {
        Vector2 WaveDirect = new Vector2(offsetX, offsetY);
        SwordWavePunchObject.GetComponent<SwordWavePunchScript>().SWPonColorDamage = SwordWavePunchDamegePercentOnColor;
        SwordWavePunchObject.GetComponent<SwordWavePunchScript>().SWPoffColorDamage = SwordWavePunchDamegePercentOffColor;
        Instantiate(SwordWavePunchObject, new Vector2(transform.position.x, transform.position.y) + WaveDirect, transform.rotation);
    }
    IEnumerator SwordWavePunchDelay()
    {
        yield return new WaitForSeconds(DelaySWPpercent);
        ReadySwordWavePunch = true;
    }
    // Механика волнового удара

    // ЛВЛ менеджер в забеге для персонажа
    void LVLmanager()
    {
        ExpSlide.maxValue = NextLimitLvl;
        ExpSlide.value = LimitLvl;
        if (LimitLvl >= NextLimitLvl)
        {
            LimitLvl = 0;
            NextLimitLvl = NextLimitLvl + (NextLimitLvl / 100 * PercentForNextLimitLvl);
            PlayerLvl += 1;
            ScoreCount += 1;
        }
    }
    // ЛВЛ менеджер в забеге для персонажа

    // Менеджер урона для способностей персонажа
    void DamageManager()
    {
        float DamagePrefPercentSPonColor = SwordAroundPunchDamageOnColor / 100 * (float)DamagePrefs;
        float DamagePrefPercentSPoffColor = SwordAroundPunchDamageOffColor / 100 * (float)DamagePrefs;

        float DamagePrefsPercentSAPonColor = SwordAroundPunchDamageOnColor / 100 * (float)DamagePrefs;
        float DamagePrefsPercentSAPoffColor = SwordAroundPunchDamageOffColor / 100 * (float)DamagePrefs;

        float DamagePrefsPercentSWPonColor = SwordWavePunchDamageOnColor / 100 * (float)DamagePrefs;
        float DamagePrefsPercentSWPoffColor = SwordWavePunchDamageOffColor / 100 * (float)DamagePrefs;
        if (damagePercentSpecs != 0)
        {
            SwordPunchScript.OnColorDamage = DamagePrefPercentSPonColor + SwordPunchDamageOnColor + (SwordPunchDamageOnColor / 100 * damagePercentSpecs);
            SwordPunchScript.OffColorDamage = DamagePrefPercentSPoffColor + SwordPunchDamageOffColor + (SwordPunchDamageOffColor / 100 * damagePercentSpecs);

            AroundPunchScript.SAPonColorDamage = DamagePrefsPercentSAPonColor + SwordAroundPunchDamageOnColor + (SwordAroundPunchDamageOnColor / 100 * damagePercentSpecs);
            AroundPunchScript.SAPoffColorDamage = DamagePrefsPercentSAPonColor + SwordAroundPunchDamageOffColor + (SwordAroundPunchDamageOffColor / 100 * damagePercentSpecs);

            SwordWavePunchDamegePercentOnColor = DamagePrefsPercentSWPonColor + SwordWavePunchDamageOnColor + (SwordWavePunchDamageOnColor / 100 * damagePercentSpecs);
            SwordWavePunchDamegePercentOffColor = DamagePrefsPercentSWPoffColor + SwordWavePunchDamageOffColor + (SwordWavePunchDamageOffColor / 100 * damagePercentSpecs);
            MaxDamageInWeapon = DamagePrefsPercentSAPonColor + SwordAroundPunchDamageOnColor + (SwordAroundPunchDamageOnColor / 100 * damagePercentSpecs);
        }
        else
        {
            SwordPunchScript.OnColorDamage = DamagePrefPercentSPonColor + SwordPunchDamageOnColor;
            SwordPunchScript.OffColorDamage = DamagePrefPercentSPoffColor + SwordPunchDamageOffColor;

            AroundPunchScript.SAPonColorDamage = DamagePrefsPercentSAPonColor + SwordAroundPunchDamageOnColor;
            AroundPunchScript.SAPoffColorDamage = DamagePrefsPercentSAPonColor + SwordAroundPunchDamageOffColor;

            SwordWavePunchDamegePercentOnColor = DamagePrefsPercentSWPonColor + SwordWavePunchDamageOnColor;
            SwordWavePunchDamegePercentOffColor = DamagePrefsPercentSWPoffColor + SwordWavePunchDamageOffColor;
            MaxDamageInWeapon = DamagePrefsPercentSAPonColor + SwordAroundPunchDamageOnColor + (SwordAroundPunchDamageOnColor / 100 * damagePercentSpecs);
        }      
    }
    // Менеджер урона для способностей персонажа

    // Менеджер скорости передвижения персонажа
   void SpeedMoveManager()
   {
        float MoveSpeedPrefsPercent = moveSpeed / 100 * (float)MoveSpeedPrefs;
        if (moveSpeedPercentSpecs != 0)
        {
            moveSpeedPercent = moveSpeed + (moveSpeed / 100 * moveSpeedPercentSpecs) + MoveSpeedPrefsPercent;
            MaxSpeedInRun = moveSpeed + (moveSpeed / 100 * moveSpeedPercentSpecs) + MoveSpeedPrefsPercent;
        }
        else
        {
            moveSpeedPercent = moveSpeed + MoveSpeedPrefsPercent;
            MaxSpeedInRun = moveSpeed + (moveSpeed / 100 * moveSpeedPercentSpecs) + MoveSpeedPrefsPercent;
        }
   }
    // Менеджер скорости передвижения персонажа

    // Менеджер скорости атаки персонажа
    void AttackSpeedManager()
    {
       float AttackSpeedPrefsSP = DelaySwordPunch / 100 * (float)AttackSpeedPrefs;
       float AttackSpeedPrefsSAP = DelaySwordPunch / 100 * (float)AttackSpeedPrefs;
       float AttackSpeedPrefsSWP = DelaySwordPunch / 100 * (float)AttackSpeedPrefs;

        if (attackSpeedPercentSpecs != 0)
        {
            DelaySPpercent = DelaySwordPunch - (DelaySwordPunch / 100 * attackSpeedPercentSpecs) - AttackSpeedPrefsSP;
            DelaySAPpercent = DelaySwordAroundPunch - (DelaySwordAroundPunch / 100 * attackSpeedPercentSpecs) - AttackSpeedPrefsSAP;
            DelaySWPpercent = DelaySwordWavePunch - (DelaySwordWavePunch / 100 * attackSpeedPercentSpecs) - AttackSpeedPrefsSWP;
        }
        else
        {
            DelaySPpercent = DelaySwordPunch - AttackSpeedPrefsSP;
            DelaySAPpercent = DelaySwordAroundPunch - AttackSpeedPrefsSAP;
            DelaySWPpercent = DelaySwordWavePunch - AttackSpeedPrefsSWP;
        }
    }
    // Менеджер скорости атаки персонажа

    // Менеджер скорости переключения цветов атаки персонажа
    void ColorSwitchSpeedManager()
    {
        if (SwitchColorSpeedPercentSpecs != 0)
        {
            DelayColorSwitchPercent = DelayColorSwitch - (DelayColorSwitch / 100 * SwitchColorSpeedPercentSpecs);
        }
        else
        {
            DelayColorSwitchPercent = DelayColorSwitch;
        }
    }
    // Менеджер скорости переключения цветов атаки персонажа

    // Механика переключения цветов атаки персонажа
    void ColorSwircher() 
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RedColorOn = true;
            BlueColorOn = false;
            GreenColorOn = false;
            PlayerRed.SetActive(true);
            PlayerBlue.SetActive(false);
            PlayerGreen.SetActive(false);
            ShardRed.SetActive(true);
            ShardBlue.SetActive(false);
            ShardGreen.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            RedColorOn = false;
            BlueColorOn = true;
            GreenColorOn = false;
            PlayerRed.SetActive(false);
            PlayerBlue.SetActive(true);
            PlayerGreen.SetActive(false);
            ShardRed.SetActive(false);
            ShardBlue.SetActive(true);
            ShardGreen.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            RedColorOn = false;
            BlueColorOn = false;
            GreenColorOn = true;
            PlayerRed.SetActive(false);
            PlayerBlue.SetActive(false);
            PlayerGreen.SetActive(true);
            ShardRed.SetActive(false);
            ShardBlue.SetActive(false);
            ShardGreen.SetActive(true);
        }

        if (RedColorOn == true)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            ColorNowPlayer = "red";
            AnimatorRed.SetBool("Walk", PlayerWalk);
        }
        else if (BlueColorOn == true)
        {
            animator.SetBool("Walk", PlayerWalk);
            GetComponent<SpriteRenderer>().color = Color.blue;
            ColorNowPlayer = "blue";
        }
        else if (GreenColorOn == true)
        {
            AnimatorGreen.SetBool("Walk", PlayerWalk);
            GetComponent<SpriteRenderer>().color = Color.green;
            ColorNowPlayer = "green";
        }
    }
    // Механика переключения цветов атаки персонажа

    // Получение характиристик с главного меню

    void GetPrefsOnMenu()
    {
        DamagePrefs = PlayerPrefs.GetInt("DamageSpec_Key");
        MoveSpeedPrefs = PlayerPrefs.GetInt("MoveSpeedSpec_Key");
        LuckyPrefs = PlayerPrefs.GetInt("LuckySpec_Key");
        AttackSpeedPrefs = PlayerPrefs.GetInt("AttackSpeedSpec_Key");
        ExpPrefs = PlayerPrefs.GetInt("ExpSpec_Key");
        HealPrefs = PlayerPrefs.GetInt("HealSpec_Key");
    }

    // Получение характиристик с главного меню

    void DiePlayer()
    {
        if (PlayerHP <= 0)
        {
            DieTimePlayer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>().ULTseconds;
            DieStatsToPrefs();
            TimeScaleManager TSM = GameObject.FindGameObjectWithTag("TimeScaleManager").GetComponent<TimeScaleManager>();
            PlayerPrefs.SetInt("PlayerMoney_Key", MoneyCount);
            DieMenu.SetActive(true);
            TSM.TimeScaleOnOff = true;
        }
    }

    void DieStatsToPrefs()
    {
        PlayerPrefs.SetFloat("DamageEndRun", MaxDamageInWeapon);
        PlayerPrefs.SetFloat("HpEndRun", MaxHpInRun);
        PlayerPrefs.SetFloat("SpeedInRun", MaxSpeedInRun);
        PlayerPrefs.SetInt("TimeDieInRun", DieTimePlayer);
    }

    void MaxHpInThisRun()
    {
        if (PlayerHP > MaxHpInRun)
        {
            MaxHpInRun = PlayerHP;
        }
        else
        {
            return;
        }
    }

    //void Animations()
    //{

    //    animator.SetBool("Walk", PlayerWalk);
    //    AnimatorGreen.SetBool("Walk", PlayerWalk);
    //    AnimatorRed.SetBool("Walk", PlayerWalk);
    //}

    void CheckWAalk()
    {
        
        if(OldPos != new Vector2(transform.position.x, transform.position.y))
        {
            PlayerWalk = true;
        }
        else
        {
            PlayerWalk = false;
        }
    }
}
