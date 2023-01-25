using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItemScript : MonoBehaviour
{
    [SerializeField] float ExpCount;
    void Start()
    {
        PlusExpCountWithPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMechanics>().AllExpPlayer += ExpCount;
            collision.gameObject.GetComponent<PlayerMechanics>().LimitLvl += ExpCount;
            Destroy(this.gameObject);
        }
    }

    void PlusExpCountWithPrefs()
    {
        PlayerMechanics PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        ExpCount += PlayerScript.ExpPrefs;
    }
}
