using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUIManager : MonoBehaviour
{
    static Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public static void SetActive(string name, bool b)
    {
        foreach (Transform child in _canvas.transform)
        {
            // 子の要素をたどる
            if (child.name == name)
            {
                // 指定した名前と一致
                // 表示フラグを設定
                child.gameObject.SetActive(b);
               
                return;
            }

        }
    }
}
