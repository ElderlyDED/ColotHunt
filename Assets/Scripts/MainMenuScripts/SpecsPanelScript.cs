using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SpecsPanelScript : MonoBehaviour
{
    

    [Header("UI light")]
    [SerializeField] GameObject LightObj;
    [SerializeField] TextMeshProUGUI TextLVLDamageBtn;
    [SerializeField] TextMeshProUGUI TextLVLMoveSpeedBtn;
    [SerializeField] TextMeshProUGUI TextLVLLuckyPlusBtn;
    [SerializeField] TextMeshProUGUI TextLVLAttackSpeedBtn;
    [SerializeField] TextMeshProUGUI TextLVLExpPlusBtn;
    [SerializeField] TextMeshProUGUI TextLVLHealPlusBtn;

    [Header("Specs Dop")]
    [SerializeField] int DamagePercent;
    [SerializeField] int MoveSpeedPercent;
    [SerializeField] int LuckyPlus;
    [SerializeField] int AttackSpeedPercent;
    [SerializeField] int ExpPlus;
    [SerializeField] int HealPlus;

    [Header("Light Count's")]
    [SerializeField] int DamagePercentLights;
    [SerializeField] int MoveSpeedPercentLights;
    [SerializeField] int LuckyPlusLights;
    [SerializeField] int AttackSpeedPercentLights;
    [SerializeField] int ExpPlusLights;
    [SerializeField] int HealPlusLights;

    [Header("LightCount Now")]
    [SerializeField] int DamageLights;
    [SerializeField] int MoveSpeedLights;
    [SerializeField] int LuckyLights;
    [SerializeField] int AttackSpeedLights;
    [SerializeField] int ExpLights;
    [SerializeField] int HealLights;

    [Header("Max Lvls")]
    [SerializeField] int MaxDamageLvl;
    [SerializeField] int MaxMoveSpeedLvl;
    [SerializeField] int MaxLuckyLvl;
    [SerializeField] int MaxAttackSpeedLvl;
    [SerializeField] int MaxExpLvl;
    [SerializeField] int MaxHealLvl;

    [Header("Costs")]
    [SerializeField] int PlayerMoney;

    [SerializeField] int NowCostDamage;
    [SerializeField] int NowCostMoveSpeed;
    [SerializeField] int NowCostLucky;
    [SerializeField] int NowCostAttackSpeed;
    [SerializeField] int NowCostExp;
    [SerializeField] int NowCostHeal;

    [SerializeField] TextMeshProUGUI DamageCostText;
    [SerializeField] TextMeshProUGUI MoveSpeedCostText;
    [SerializeField] TextMeshProUGUI LuckyCostText;
    [SerializeField] TextMeshProUGUI AttackSpeedCostText;
    [SerializeField] TextMeshProUGUI ExpCostText;
    [SerializeField] TextMeshProUGUI HealCostText;
    void Start()
    {
        
        if (PlayerPrefs.HasKey("DamageSpec_Key") && PlayerPrefs.HasKey("MoveSpeedSpec_Key") && PlayerPrefs.HasKey("LuckySpec_Key")
            && PlayerPrefs.HasKey("AttackSpeedSpec_Key") && PlayerPrefs.HasKey("ExpSpec_Key") && PlayerPrefs.HasKey("HealSpec_Key")
            && PlayerPrefs.HasKey("DamageLight_Key") && PlayerPrefs.HasKey("MoveSpeedLight_Key") && PlayerPrefs.HasKey("LuckyLight_Key")
            && PlayerPrefs.HasKey("AttackSpeedLight_Key") && PlayerPrefs.HasKey("ExpLight_Key") && PlayerPrefs.HasKey("HealLight_Key"))
        {
            return;
        }
        else
        {
            PlayerPrefs.SetInt("DamageSpec_Key", 0);
            PlayerPrefs.SetInt("DamageLight_Key", 0);

            PlayerPrefs.SetInt("MoveSpeedSpec_Key", 0);
            PlayerPrefs.SetInt("MoveSpeedLight_Key", 0);

            PlayerPrefs.SetInt("LuckySpec_Key", 0);
            PlayerPrefs.SetInt("LuckyLight_Key", 0);

            PlayerPrefs.SetInt("AttackSpeedSpec_Key", 0);
            PlayerPrefs.SetInt("AttackSpeedLight_Key", 0);

            PlayerPrefs.SetInt("ExpSpec_Key", 0);
            PlayerPrefs.SetInt("ExpLight_Key", 0);

            PlayerPrefs.SetInt("HealSpec_Key", 0);
            PlayerPrefs.SetInt("HealLight_Key", 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerMoney = PlayerPrefs.GetInt("PlayerMoney_Key");
       

        DamageLights = PlayerPrefs.GetInt("DamageLight_Key");
        MoveSpeedLights = PlayerPrefs.GetInt("MoveSpeedLight_Key");
        LuckyLights = PlayerPrefs.GetInt("LuckyLight_Key");
        AttackSpeedLights = PlayerPrefs.GetInt("AttackSpeedLight_Key");
        ExpLights = PlayerPrefs.GetInt("ExpLight_Key");
        HealLights = PlayerPrefs.GetInt("HealLight_Key");

        PrintLvlSpec(TextLVLDamageBtn, DamageLights, MaxDamageLvl);
        PrintLvlSpec(TextLVLMoveSpeedBtn, MoveSpeedLights, MaxMoveSpeedLvl);
        PrintLvlSpec(TextLVLLuckyPlusBtn, LuckyLights, MaxLuckyLvl);
        PrintLvlSpec(TextLVLAttackSpeedBtn, AttackSpeedLights, MaxAttackSpeedLvl);
        PrintLvlSpec(TextLVLExpPlusBtn, ExpLights, MaxExpLvl);
        PrintLvlSpec(TextLVLHealPlusBtn, HealLights, MaxHealLvl);

        DamageCostText.text = NowCostDamage.ToString();
        MoveSpeedCostText.text = NowCostMoveSpeed.ToString();
        LuckyCostText.text = NowCostLucky.ToString();
        AttackSpeedCostText.text = NowCostAttackSpeed.ToString();
        ExpCostText.text = NowCostExp.ToString();
        HealCostText.text = NowCostHeal.ToString();
    }

    void PrintLvlSpec(TextMeshProUGUI TextObj, int LvlNow, int MaxLvl)
    {
        TextObj.text = LvlNow.ToString() + "/" + MaxLvl.ToString();
    }
    public void DamageClickBTN()
    {
        if (PlayerMoney >= NowCostDamage)
        {
            
            if (DamageLights < MaxDamageLvl)
            {
                int PM = PlayerPrefs.GetInt("PlayerMoney_Key");
                PM -= NowCostDamage;
                PlayerPrefs.SetInt("PlayerMoney_Key", PM);

                int ppINT = PlayerPrefs.GetInt("DamageSpec_Key");
                ppINT += DamagePercent;
                PlayerPrefs.SetInt("DamageSpec_Key", ppINT);

                int llINT = PlayerPrefs.GetInt("DamageLight_Key");
                llINT += DamagePercentLights;
                PlayerPrefs.SetInt("DamageLight_Key", llINT);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

    }
    public void MoveSpeedClickBTN()
    {
        if (PlayerMoney >= NowCostMoveSpeed)
        {
            
            if (MoveSpeedLights < MaxMoveSpeedLvl)
            {
                int PM = PlayerPrefs.GetInt("PlayerMoney_Key");
                PM -= NowCostMoveSpeed;
                PlayerPrefs.SetInt("PlayerMoney_Key", PM);

                int ppINT = PlayerPrefs.GetInt("MoveSpeedSpec_Key");
                ppINT += MoveSpeedPercent;
                PlayerPrefs.SetInt("MoveSpeedSpec_Key", ppINT);

                int llINT = PlayerPrefs.GetInt("MoveSpeedLight_Key");
                llINT += MoveSpeedPercentLights;
                PlayerPrefs.SetInt("MoveSpeedLight_Key", llINT);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    } 
    public void LuckyPlusClickBTN()
    {
        if (PlayerMoney >= NowCostLucky)
        {         
            if (LuckyLights < MaxLuckyLvl)
            {
                int PM = PlayerPrefs.GetInt("PlayerMoney_Key");
                PM -= NowCostLucky;
                PlayerPrefs.SetInt("PlayerMoney_Key", PM);

                int ppINT = PlayerPrefs.GetInt("LuckySpec_Key");
                ppINT += LuckyPlus;
                PlayerPrefs.SetInt("LuckySpec_Key", ppINT);

                int llINT = PlayerPrefs.GetInt("LuckyLight_Key");
                llINT += LuckyPlusLights;
                PlayerPrefs.SetInt("LuckyLight_Key", llINT);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
    public void AttackSpeedClickBTN()
    {
        if (PlayerMoney >= NowCostAttackSpeed)
        {
            if (AttackSpeedLights < MaxAttackSpeedLvl)
            {
                int PM = PlayerPrefs.GetInt("PlayerMoney_Key");
                PM -= NowCostAttackSpeed;
                PlayerPrefs.SetInt("PlayerMoney_Key", PM);

                int ppINT = PlayerPrefs.GetInt("AttackSpeedSpec_Key");
                ppINT += AttackSpeedPercent;
                PlayerPrefs.SetInt("AttackSpeedSpec_Key", ppINT);

                int llINT = PlayerPrefs.GetInt("AttackSpeedLight_Key");
                llINT += AttackSpeedPercentLights;
                PlayerPrefs.SetInt("AttackSpeedLight_Key", llINT);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
    public void ExpPlusClickBTN()
    {
        if (PlayerMoney >= NowCostExp)
        {
            if (ExpLights < MaxExpLvl)
            {
                int PM = PlayerPrefs.GetInt("PlayerMoney_Key");
                PM -= NowCostExp;
                PlayerPrefs.SetInt("PlayerMoney_Key", PM);

                int ppINT = PlayerPrefs.GetInt("ExpSpec_Key");
                ppINT += ExpPlus;
                PlayerPrefs.SetInt("ExpSpec_Key", ppINT);

                int llINT = PlayerPrefs.GetInt("ExpLight_Key");
                llINT += ExpPlusLights;
                PlayerPrefs.SetInt("ExpLight_Key", llINT);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
    public void HealPlusClickBTN()
    {
        if (PlayerMoney >= NowCostHeal)
        {
            if (HealLights < MaxExpLvl)
            {
                int PM = PlayerPrefs.GetInt("PlayerMoney_Key");
                PM -= NowCostHeal;
                PlayerPrefs.SetInt("PlayerMoney_Key", PM);

                int ppINT = PlayerPrefs.GetInt("HealSpec_Key");
                ppINT += HealPlus;
                PlayerPrefs.SetInt("HealSpec_Key", ppINT);

                int llINT = PlayerPrefs.GetInt("HealLight_Key");
                llINT += HealPlusLights;
                PlayerPrefs.SetInt("HealLight_Key", llINT);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

    }
    public void ResetUPClickBTN()
    {

        PlayerPrefs.SetInt("DamageSpec_Key", 0);
        PlayerPrefs.SetInt("DamageLight_Key", 0);

        PlayerPrefs.SetInt("MoveSpeedSpec_Key", 0);
        PlayerPrefs.SetInt("MoveSpeedLight_Key", 0);

        PlayerPrefs.SetInt("LuckySpec_Key", 0);
        PlayerPrefs.SetInt("LuckyLight_Key", 0);

        PlayerPrefs.SetInt("AttackSpeedSpec_Key", 0);
        PlayerPrefs.SetInt("AttackSpeedLight_Key", 0);

        PlayerPrefs.SetInt("ExpSpec_Key", 0);
        PlayerPrefs.SetInt("ExpLight_Key", 0);

        PlayerPrefs.SetInt("HealSpec_Key", 0);
        PlayerPrefs.SetInt("HealLight_Key", 0);

    }

}
