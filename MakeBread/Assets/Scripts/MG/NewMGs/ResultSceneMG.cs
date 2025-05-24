using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultSceneMG : MonoBehaviour
{
    private GameMG_new _gameMG;
    public static bool IsShaked = false;

    //各スコア用のUIテキスト
    [SerializeField] private TextMeshProUGUI _ovenScore;
    [SerializeField] private TextMeshProUGUI _fermentScore;
    [SerializeField] private TextMeshProUGUI _breadNameTx;

    /// <summary>
    /// リザルトに表示するパンの名前
    /// </summary>
    private string _breadName = "";

    private void Awake()
    {
        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
        _breadName =  _gameMG.SendBreadName();
        if(_breadName == "")
        {
            _breadName = "おさかなぱん";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //_ovenScore.text = _gameMG.SendBakeStatus();
        //_fermentScore.text = _gameMG.score_Ferment;
        _ovenScore.text = ScoreTxChange(_gameMG.SendBakeStatus());
        _breadNameTx.text = _breadName;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || IsShaked == true)
        {
            IsShaked = false;
            StartCoroutine(_gameMG.LoadSceneAsync("TitleScene"));
        }
    }

    /// <summary>
    /// 焼き加減に応じてテキストを返す
    /// </summary>
    /// <param name="bakeStatus">焼き加減</param>
    /// <returns>表示用OvenScoreテキスト</returns>
    private string ScoreTxChange(string bakeStatus)
    {
        string afterText = "";

        if(bakeStatus == "Raw")
        {
            afterText = "なまやけ...";
        }
        else if(bakeStatus == "OverCoocked")
        {
            afterText = "こげこげ...";
        }
        else if(bakeStatus == "Good")
        {
            afterText = "おいしい!!";
        }
        else if(bakeStatus == "Perfect")
        {
            afterText = "かんぺき!!!";
        }

        return afterText;
    }
}
