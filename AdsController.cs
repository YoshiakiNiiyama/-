using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using System;

/// <summary>
/// 広告表示とButton再表示を制御する
/// 常にActiveなGameObjectにアタッチして使用する
/// </summary>
public class AdsController: MonoBehaviour
{
    //表示周期(編集可能)

   [SerializeField] int DisplaySpan = 1;
    TimeSpan lastTime;
    TimeSpan NowTime;
    //表示ボタン
    [SerializeField] GameObject AdsButton;
    //リワード処理
    public UnityEvent _UnityEvent;

    /// <summary>
    /// リワード広告を表示する
    /// Buttonイベントに登録するなどして実行する
    /// </summary>
    public void ShowRewardedAd()
    {
        if (_UnityEvent == null)
        {
            return;
        }
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    /// <summary>
    /// 広告を全て見るとリワード報酬を獲得する
    /// </summary>
    /// <param name="result">Result.</param>
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                _UnityEvent.Invoke();
                AdsButton.SetActive(false);
                lastTime = DateTime.UtcNow.TimeOfDay;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

     void Update()
    {
        DisplayCheck();
    }

    /// <summary>
    /// 表示周期が過ぎたらButtonを表示
    /// </summary>
    void DisplayCheck()
    {
        //Activeな時は処理を行わない
        if (AdsButton.activeSelf)
        {
            return;
        }
        NowTime = DateTime.UtcNow.TimeOfDay;
        var span = NowTime - lastTime;
        //Debug.Log("span.TotalMinutes " + span.TotalMinutes);
        if (span.TotalMinutes > DisplaySpan)
        {
            AdsButton.SetActive(true);
        }
    }
}