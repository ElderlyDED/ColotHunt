using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    [Header("On Off Bool")]
    [SerializeField] bool OnOffLVLMenu;
    public bool OnOffPauseMenu;
    public bool TimeScaleOnOff;

    [Header("Links Time objects")]
    [SerializeField] GameObject MenuLVLobj;
    [SerializeField] MenuLvlUpGameScript MenuLVLscript; // Получается DragAndDrop в инспекторе
    [SerializeField] GameObject PauseMenu;

    void Start()
    {
        
    }
    void Update()
    {
        SwitcherMenuLvl();
        SwitcherPauseMenu();
        TimeScaler();
    }
    void TimeScaler()
    {
        if (TimeScaleOnOff == true)
        {
            Time.timeScale = 0.000001f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    void SwitcherMenuLvl()
    {
        if(Input.GetKeyDown(KeyCode.I) && OnOffLVLMenu == false && OnOffPauseMenu == false)
        {
            TimeScaleOnOff = true;
            OnOffLVLMenu = true;
            MenuLVLobj.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.I) && OnOffLVLMenu == true)
        {
            TimeScaleOnOff = false;
            OnOffLVLMenu = false;
            MenuLVLobj.SetActive(false);
        }
    }

    void SwitcherPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && OnOffPauseMenu == false && OnOffLVLMenu == false)
        {
            PauseMenu.SetActive(true);
            OnOffPauseMenu = true;
            TimeScaleOnOff = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && OnOffPauseMenu == true)
        {
            PauseMenu.SetActive(false);
            OnOffPauseMenu = false;
            TimeScaleOnOff = false;
        }
    }
}
