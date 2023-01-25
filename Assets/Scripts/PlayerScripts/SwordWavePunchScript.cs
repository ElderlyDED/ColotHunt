using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWavePunchScript : MonoBehaviour
{
    [SerializeField] Vector2 WaveDirection;
    [SerializeField] float WaveSpeed;
    public float WaveDamage;
    [SerializeField] float DestroyWaveTime;
    [SerializeField] List<GameObject> Enems = new List<GameObject>();
    [SerializeField] PlayerMechanics PlayerScript;

    [Header("Colors Damage")]
    public float SWPonColorDamage;
    public float SWPoffColorDamage;

    [Header("ColorsSlashPrefs")]
    [SerializeField] GameObject BlueSlash;
    [SerializeField] GameObject GreenSlash;
    [SerializeField] GameObject RedSlash;

    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        WaveDirection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>().LastPlayerDirect;
        StartCoroutine(DestroyWave());
        if (PlayerScript.ColorNowPlayer == "red")
        {
            BlueSlash.SetActive(false);
            GreenSlash.SetActive(false);
            RedSlash.SetActive(true);
        }
        else if(PlayerScript.ColorNowPlayer == "blue")
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

    void Update()
    {
        transform.Translate(WaveDirection * WaveSpeed * Time.deltaTime);
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
                    Enems[i].GetComponent<EnemyMechanics>().EnemyHP -= SWPonColorDamage;
                    Enems[i].GetComponent<EnemyMechanics>().PushAway(transform.position, 1f);
                }
                else
                {
                    Enems[i].GetComponent<EnemyMechanics>().EnemyHP -= SWPoffColorDamage;
                    Enems[i].GetComponent<EnemyMechanics>().PushAway(transform.position, 1f);
                }
                
            }
            Enems.Clear();
        }
    }

    IEnumerator DestroyWave()
    {
        yield return new WaitForSeconds(DestroyWaveTime);
        Destroy(this.gameObject);
    }
}
