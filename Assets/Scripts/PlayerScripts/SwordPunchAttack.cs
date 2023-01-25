using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPunchAttack : MonoBehaviour
{
    public float SwordPunchDamage;
    [SerializeField] List<GameObject> Enems = new List<GameObject>();
    [SerializeField] PlayerMechanics PlayerScript;

    [Header("Color Damages")]
    public float OnColorDamage;
    public float OffColorDamage;

    [Header("ColorsSlashPrefs")]
    [SerializeField] GameObject BlueSlash;
    [SerializeField] GameObject GreenSlash;
    [SerializeField] GameObject RedSlash;
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.ColorNowPlayer == "red")
        {
            BlueSlash.SetActive(false);
            GreenSlash.SetActive(false);
            RedSlash.SetActive(true);
        }
        else if (PlayerScript.ColorNowPlayer == "blue")
        {
            BlueSlash.SetActive(true);
            GreenSlash.SetActive(false);
            RedSlash.SetActive(false);
        }
        else if (PlayerScript.ColorNowPlayer == "green")
        {
            BlueSlash.SetActive(false);
            GreenSlash.SetActive(true);
            RedSlash.SetActive(false);
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
                    Enems[i].GetComponent<EnemyMechanics>().EnemyHP -= OnColorDamage;
                    Enems[i].GetComponent<EnemyMechanics>().PushAway(transform.position, 1f);
                }
                else
                {
                    Enems[i].GetComponent<EnemyMechanics>().EnemyHP -= OffColorDamage;
                    Enems[i].GetComponent<EnemyMechanics>().PushAway(transform.position, 1f);
                }
            }
            Enems.Clear();
        }
       
    }
}
