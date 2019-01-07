using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSound: MonoBehaviour
{
    public GameObject gameManager;

    public void OnClick()
    {
        AudioListener.volume = 1;
        // 非表示にする
        gameObject.SetActive(false);
        // Buttonを表示する
        CanvasUIManager.SetActive("SoundOn", true);
    }
}
