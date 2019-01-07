using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class KodokuManager : MonoBehaviour
{ 
    //オブジェクト
    public GameObject gameManager; //ゲームマネージャー
    public Sprite[] KodokuPicture = new Sprite[5];



    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //孤独をタップ
    public void TapKodoku ()
    {
        gameManager.GetComponent<GameManager>().GetKodoku();
    }

    public void SetKodokuPicture(int kodokuLevel)
    {
        GetComponent<Image>().sprite = KodokuPicture[kodokuLevel];
    }

    public void SetKodokuScale (float score, float nextScore)
    {
        float scale = 0.5f + (((float)score / (float)nextScore) / 2.0f);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
