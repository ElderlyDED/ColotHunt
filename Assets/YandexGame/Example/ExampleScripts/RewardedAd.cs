using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class RewardedAd : MonoBehaviour
{
    [SerializeField] int AdID;
    [SerializeField] TextMeshProUGUI textMoney;

    

    void Start() => AdMoney(0);

    private void OnEnable() => YandexGame.CloseVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Rewarded;

    void Rewarded(int id)
    {
        if (id == AdID)
            AdMoney(1);
    }

    void AdMoney(int count)
    {
        int GetPlayerMoney = PlayerPrefs.GetInt("PlayerMoney_Key");
        GetPlayerMoney += count;
        PlayerPrefs.SetInt("PlayerMoney_Key", GetPlayerMoney);
    }
}