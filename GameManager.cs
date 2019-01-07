using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const int MAX_LEVEL = 4; //最大進化


    //オブジェクト参照
    public GameObject canvasGame;
    public GameObject imageKodoku;
    public GameObject expBar;

    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLevel;

    //メンバ変数
    public float score = 0; //現在のスコア
    public float nextScore = 10; //レベルアップまでに必要なスコア

    //データセーブ用キー
    private const string KEY_SCORE = "score"; //スコア
    private const string KEY_LEVEL = "level"; //レベル

    public AudioClip getScoreSE;　//タップ時効果音
    public AudioClip levelUpSE;
    public AudioClip clearSE;

    private int kodokuLevel = 0; //コドクのレベル

    public AudioSource audioSource; //BGM

    bool One;

    private float[] nextScoreTable = new float[] { 1000, 3000, 5000, 10000, 30000}; //スコア設定
    

// Start is called before the first frame update
void Start()
    {
        One = true;
        FadeManager.FadeIn(); //シーン変更時フェードイン
        //データ初期化時コメントアウト
        //PlayerPrefs.DeleteAll();


        //オーディオソース取得
        audioSource = this.gameObject.GetComponent<AudioSource>(); //BGM再生

        //初期設定
        score = PlayerPrefs.GetFloat(KEY_SCORE);
        kodokuLevel = PlayerPrefs.GetInt(KEY_LEVEL);

        nextScore = nextScoreTable[kodokuLevel];
        imageKodoku.GetComponent<KodokuManager>().SetKodokuPicture(kodokuLevel);
        imageKodoku.GetComponent<KodokuManager>().SetKodokuScale(score, nextScore);

        RefreshScoreText();
        GetExp();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //コドク入手

    public void GetKodoku()
    {
        audioSource.PlayOneShot(getScoreSE);　//タップ時効果音

        //コドクアニメ再生
        AnimatorStateInfo stateInfo =
            imageKodoku.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.fullPathHash ==
            Animator.StringToHash("Base Layer.Tap@ImageKodoku")) //すでに再生中なら先頭から
        {
            imageKodoku.GetComponent<Animator>().Play(stateInfo.fullPathHash, 0, 0.0f);
        }
        else
        {
            imageKodoku.GetComponent<Animator>().SetTrigger("isGetScore");

        }

        if (score < nextScore)
        {
            score += 1;
        }

        if (score > nextScore)
        {
            score = nextScore;
        }

            KodokuLevelUp();
            imageKodoku.GetComponent<KodokuManager>().SetKodokuScale(score, nextScore);
            RefreshScoreText();
            GetExp();
        

        if ((score == nextScore) && (kodokuLevel == MAX_LEVEL))
        {
            ClearEffect();
        }
        
        if (One)
        {
            SaveGameData();
        }
    }

    //スコアテキスト更新
    void RefreshScoreText()
    {
        textScore.GetComponent<TextMeshProUGUI>().text =
         score + " / " + nextScore;



        textLevel.GetComponent<TextMeshProUGUI>().text =
        "LV." + kodokuLevel;

    }

    //　ゲームデータをセーブ
    void SaveGameData()
    {
        PlayerPrefs.SetFloat(KEY_SCORE, score);
        PlayerPrefs.SetInt(KEY_LEVEL, kodokuLevel);

        PlayerPrefs.Save();
    }

    void KodokuLevelUp()
    {
        if (score >= nextScore)
        {
            if (kodokuLevel < MAX_LEVEL)
            {
                kodokuLevel++;
                score = 0;

                audioSource.PlayOneShot(levelUpSE);
                FadeManager.FadeOut(kodokuLevel);
                nextScore = nextScoreTable[kodokuLevel];
                imageKodoku.GetComponent<KodokuManager>().SetKodokuPicture(kodokuLevel);
            }
        }
    }

    void GetExp()
    {
        expBar.GetComponent<Image>().fillAmount = score / nextScore;
    }

    void ClearEffect()
    {
        audioSource.PlayOneShot(clearSE);
        PlayerPrefs.DeleteAll();
        FadeManager.FadeOut(5);
        One = false;
    }
    public void Reward(float reward)
    {
        reward = nextScore / 10; 
        score += reward;
        GetKodoku();
        RefreshScoreText();
        GetExp();
        
    }
}