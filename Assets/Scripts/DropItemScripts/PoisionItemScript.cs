using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisionItemScript : MonoBehaviour
{
    [SerializeField] float PlusHP;
    void Start()
    {
        PlusHPprefs();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMechanics>().PlayerHP += PlusHP;
            Destroy(this.gameObject);
        }
    }
     void PlusHPprefs()
    {
        PlayerMechanics PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        PlusHP += PlayerScript.HealPrefs;
    }
}
