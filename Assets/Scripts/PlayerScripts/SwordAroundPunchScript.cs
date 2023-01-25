using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAroundPunchScript : MonoBehaviour
{
    public float SwordAroundPunchDamage;
    [SerializeField] List<GameObject> Enems = new List<GameObject>();
    [SerializeField] PlayerMechanics PlayerScript;

    [Header("Colors Damages")]
    public float SAPonColorDamage;
    public float SAPoffColorDamage;

    [Header("ColorsSlashPrefs")]
    [SerializeField] GameObject BlueRing;
    [SerializeField] GameObject GreenRing;
    [SerializeField] GameObject RedRing;
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
    }
    void Update()
    {
        if (PlayerScript.ColorNowPlayer == "red")
        {
            BlueRing.SetActive(false);
            GreenRing.SetActive(false);
            RedRing.SetActive(true);
        }
        else if (PlayerScript.ColorNowPlayer == "blue")
        {
            BlueRing.SetActive(true);
            GreenRing.SetActive(false);
            RedRing.SetActive(false);
        }
        else if (PlayerScript.ColorNowPlayer == "green")
        {
            BlueRing.SetActive(false);
            GreenRing.SetActive(true);
            RedRing.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enems.Add(collision.gameObject);
            for (int i = 0; i < Enems.Count; i++)
            {
                if (PlayerScript.ColorNowPlayer == Enems[i].GetComponent<EnemyMechanics>().EnemyColorNow)
                {
                    Enems[i].GetComponent<EnemyMechanics>().EnemyHP -= SAPonColorDamage;
                    Enems[i].GetComponent<EnemyMechanics>().PushAway(transform.position, 1f);
                }
                else
                {
                    Enems[i].GetComponent<EnemyMechanics>().EnemyHP -= SAPoffColorDamage;
                    Enems[i].GetComponent<EnemyMechanics>().PushAway(transform.position, 1f);
                }
                
            }
            Enems.Clear();
        }
    }
}
