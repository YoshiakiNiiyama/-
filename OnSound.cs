using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSound: MonoBehaviour
{
    public GameObject gameManager;
       
    public void OnClick()
    {
        AudioListener.volume = 0;
        gameObject.SetActive(false);
        // Button2を表示する
        CanvasUIManager.SetActive("SoundOff", true);
    }
}
