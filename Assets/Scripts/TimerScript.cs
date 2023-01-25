using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerString;
    public int ULTseconds;
    public int Seconds;
    public int Minutes;
    void Start()
    {
        StartCoroutine(Timer());
    }
    void Update()
    {
        CheckSecond();
        StringTime();
    }
    IEnumerator Timer()
    {
        while (true)
        {
            ULTseconds += 1;
            Seconds += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    void CheckSecond()
    {
        if (Seconds == 60)
        {
            Seconds = 0;
            Minutes += 1;
        }
    }
    void StringTime()
    {
        if (Seconds <= 9)
        {
            if (Minutes <= 9)
            {
                TimerString.text = "0" + Minutes.ToString() + ":" + "0" + Seconds.ToString();
            }
            else
            {
                TimerString.text = Minutes.ToString() + ":" + "0" + Seconds.ToString();
            }
        }
        else
        {
            if (Minutes <= 9)
            {
                TimerString.text = "0" + Minutes.ToString() + ":"  + Seconds.ToString();
            }
            else
            {
                TimerString.text = Minutes.ToString() + ":" +  Seconds.ToString();
            }
        } 
    }
}
