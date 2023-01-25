using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuBtnScripts : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MoneyCountText;
    [SerializeField] GameObject GuidePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoneyCountText.text = PlayerPrefs.GetInt("PlayerMoney_Key").ToString();
    }

    public void PlayBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToGuideBtn()
    {
        GuidePanel.SetActive(true);
    }

    public void OffGuideBtn()
    {
        GuidePanel.SetActive(false);
    }
}
