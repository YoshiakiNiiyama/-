using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

using UnityEngine.UI;

using SocialConnector;

public class ShareController: MonoBehaviour
{
   

    public void Share()
    {
        StartCoroutine(ShareScreenShot());
    }

    IEnumerator ShareScreenShot()
    {
        // Shareするメッセージを設定
        string text = "実は孤独を育ててるんだ！面白いからやってみて！\n#孤独そだて ";
        string url = "https://play.google.com/store/apps/details?id=com.ASOBICATIONLtd.KodokuSodate";

        yield return new WaitForSeconds(1);

            //Shareする
            SocialConnector.SocialConnector.Share(text, url);
    }
}


