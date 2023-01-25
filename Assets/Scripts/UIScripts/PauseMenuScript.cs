using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeBtnClick()
    {
        TimeScaleManager TSM = GameObject.FindGameObjectWithTag("TimeScaleManager").GetComponent<TimeScaleManager>();
        TSM.OnOffPauseMenu = false;
        TSM.TimeScaleOnOff = false;
        gameObject.SetActive(false);
    }
     public void MainMenuBtnClick()
     {
        SceneManager.LoadScene(0);
     }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
