using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuLvlUpGameScript : MonoBehaviour
{
    public bool OnOffLVLMenu;

    [SerializeField] TextMeshProUGUI ScoreText;
    
    [SerializeField] PlayerMechanics PlayerScript;

    [Header("Spec LVL's")]
    [SerializeField] int DamageLVL;
    [SerializeField] int MoveSpeedLVL;
    [SerializeField] int AttackSpeedLVL;
    [SerializeField] int SwitchColorSpeedLVL;

    [Header("Max Spec's Lvl's")]
    [SerializeField] int DamageMaxLVL;
    [SerializeField] int MoveSpeedMaxLVL;
    [SerializeField] int AttackSpeedMaxLVL;

    [Header("Percents for Player")]
    [SerializeField] float DamagePercent;
    [SerializeField] float MoveSpeedPercent;
    [SerializeField] float AttackSpeedPercent;
    [SerializeField] float SwitchColorSpeedPercent;

    [Header("UI obj")]
    [SerializeField] GameObject LightObj;
    [SerializeField] Transform LightBoxDamageBTN;
    [SerializeField] Transform LightBoxMoveSpeedBTN;
    [SerializeField] Transform LightBoxAttackSpeedBTN;
    [SerializeField] Transform LightBoxSwitchColorSpeedBTN;
    //[SerializeField] AnimationClip NoPointsAnim;
    [SerializeField] TextMeshProUGUI DamageMaxText;
    [SerializeField] TextMeshProUGUI MoveSpeedMaxText;
    [SerializeField] TextMeshProUGUI AttackSpeedMaxText;

    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
    }

    void Update()
    {
        ScoreText.text = PlayerScript.ScoreCount.ToString();
    }

    public void PlusLVLclickBTNdamage()
    {
        if (PlayerScript.ScoreCount <= 0)
        {
            Animation anim = ScoreText.GetComponent<Animation>();
            anim["NoPointsAnim"].speed = 1000000f;
            anim.Play("NoPointsAnim");
          
        }
        else if(PlayerScript.ScoreCount > 0 && DamageLVL < DamageMaxLVL)
        {
            PlayerScript.ScoreCount -= 1;
            PlayerScript.damagePercentSpecs += DamagePercent;
            DamageLVL += 1;
            GameObject newlight = Instantiate(LightObj) as GameObject;
            newlight.transform.SetParent(LightBoxDamageBTN, false);
        }
        else if (DamageLVL >= DamageMaxLVL)
        {
            Animation anim = DamageMaxText.GetComponent<Animation>();
            anim["MaxSpecLvlAnim"].speed = 1000000f;
            anim.Play("MaxSpecLvlAnim");
            return;
        }
    }

    public void PlusLVLclickBTNmoveSpeed()
    {
        if (PlayerScript.ScoreCount <= 0)
        {
            Animation anim = ScoreText.GetComponent<Animation>();
            anim["NoPointsAnim"].speed = 1000000f;
            anim.Play("NoPointsAnim");

        }
        else if(PlayerScript.ScoreCount > 0 && MoveSpeedLVL < MoveSpeedMaxLVL)
        {
             PlayerScript.ScoreCount -= 1;
             PlayerScript.moveSpeedPercentSpecs += MoveSpeedPercent;
             MoveSpeedLVL += 1;
             GameObject newlight = Instantiate(LightObj) as GameObject;
             newlight.transform.SetParent(LightBoxMoveSpeedBTN, false);
        }
        else if (MoveSpeedLVL >= MoveSpeedMaxLVL)
        {
            Animation anim = MoveSpeedMaxText.GetComponent<Animation>();
            anim["MaxSpecLvlAnim"].speed = 1000000f;
            anim.Play("MaxSpecLvlAnim");
            return;
        }

    }

    public void PlusLVLclickBTNattackSpeed()
    {
        if (PlayerScript.ScoreCount <= 0)
        {
            Animation anim = ScoreText.GetComponent<Animation>();
            anim["NoPointsAnim"].speed = 1000000f;
            anim.Play("NoPointsAnim");

        }
        else if (PlayerScript.ScoreCount > 0 && AttackSpeedLVL < AttackSpeedMaxLVL) 
        {
             PlayerScript.ScoreCount -= 1;
             PlayerScript.attackSpeedPercentSpecs += AttackSpeedPercent;
             AttackSpeedLVL += 1;
             GameObject newlight = Instantiate(LightObj) as GameObject;
             newlight.transform.SetParent(LightBoxAttackSpeedBTN, false);
        }
        else if (AttackSpeedLVL >= AttackSpeedMaxLVL)
        {
            Animation anim = AttackSpeedMaxText.GetComponent<Animation>();
            anim["MaxSpecLvlAnim"].speed = 1000000f;
            anim.Play("MaxSpecLvlAnim");
            return;
        }
    }

}
