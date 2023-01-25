using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEstSlider : MonoBehaviour
{
    [SerializeField] float timeDelay;
    [SerializeField] float TimeMinus;
    [SerializeField] Slider slider;

   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("StartReload")]
    public void TestReload()
    {
        slider.maxValue = timeDelay;
        if (TimeMinus <= 0)
        {
            TimeMinus = timeDelay;
        }
        else
        {
            TimeMinus -= Time.deltaTime;
            slider.value = TimeMinus;
        }

    }
}
