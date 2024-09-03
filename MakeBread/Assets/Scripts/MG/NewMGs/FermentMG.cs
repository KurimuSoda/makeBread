using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FermentMG : MonoBehaviour
{
    private GameMG_new _gameMG;

    public static bool IsButtonAPrs = false;
    public static bool IsShaked = false;

    /// <summary>
    /// 生地オブジェクトの膨張をするかどうか
    /// </summary>
    [SerializeField] private bool isBigStart = false;
    [SerializeField] private GameObject _BreadKiji;

    /// <summary>
    /// 生地オブジェクトのScaleに代入する変数
    /// </summary>
    private float _kijiScale = 0.01f;

    /// <summary>
    /// 生地オブジェクトが_kijiScaleに達するまでの時間
    /// </summary>
    [SerializeField] private float _time = 0.2f;

    /// <summary>
    /// スコアの評価を決めるために使用する変数
    /// </summary>
    [SerializeField] private float score_Ferment = 0.0f;

    /// <summary>
    /// 操作説明を表示するオブジェクト
    /// </summary>
    [SerializeField] private GameObject _howtoObj;

    /// <summary>
    /// リザルトスコアUIをまとめてオンオフするためのオブジェクト
    /// </summary>
    [SerializeField] private GameObject scoreCanvas;

    /// <summary>
    /// スコアの文字をアニメーションするのに使う
    /// </summary>
    [SerializeField] private RectTransform scCan;

    //スコアに対してコメントを表示するためのTMP
    [SerializeField] private TextMeshProUGUI comment;

    /// <summary>
    /// スコアの文字を表示するためのTMP。S+, S, A, B, C の5つ
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreTx;

    private Vector3 scoreSize = new Vector3(1.0f, 1.0f, 1.0f);

    private bool _isFermentEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        _howtoObj.SetActive(true);
        Random.InitState(System.DateTime.Now.Millisecond);
        IsButtonAPrs = false;
        _isFermentEnd = false;
        scoreCanvas.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        scCan.localScale = scoreSize;
        scoreCanvas.SetActive(false);
        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFermentEnd && IsButtonAPrs == true)
        {
            IsButtonAPrs = false;
            _gameMG.FermentFinish();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || IsShaked == true)
        {
            IsShaked = false;
            FermentGameStart();
        }
        if (Input.GetKeyDown(KeyCode.B) || IsButtonAPrs == true)
        {
            IsButtonAPrs = false;
            BreadAnimKill();
        }
        if(Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.F))
        {
            _gameMG.FermentFinish();
        }
       
    }

    /*
    private void FixedUpdate()
    {
        if (_isFermentEnd && IsButtonAPrs == true)
        {
            IsButtonAPrs = false;
            _gameMG.FermentFinish();
        }
    }*/

    /// <summary>
    /// 発酵ゲームを始める。アニメーション再生の関数を呼び出す。
    /// </summary>
    public void FermentGameStart()
    {
        IsButtonAPrs = false;
        _isFermentEnd = false;
        _howtoObj.SetActive(false);
        scoreCanvas.SetActive(false);
        scCan.transform.DOKill();
        BreadAnimStart();
    }

    /// <summary>
    /// 生地の拡大アニメーションを再生する
    /// </summary>
    private void BreadAnimStart()
    {
        _BreadKiji.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        _time = Random.Range(1.0f, 3.1f);
        _kijiScale = Random.Range(2.2f, 3.6f) * 10;
        _kijiScale = Mathf.Floor(_kijiScale) * 0.1f;
        _BreadKiji.transform.DOScale(new Vector3(_kijiScale, _kijiScale, _kijiScale), _time)
            .SetEase(Ease.OutSine);
    }

    /// <summary>
    /// 生地の拡大アニメーションを(強制)終了させる
    /// </summary>
    private void BreadAnimKill()
    {
        _BreadKiji.transform.DOKill();
        FermentJadge();
    }

    /// <summary>
    /// 生地のサイズからスコアを表示する
    /// </summary>
    private void FermentJadge()
    {
        //float breadScale = 1.0f;
        float breadScale = _BreadKiji.transform.localScale.x * 10;
        breadScale = Mathf.Floor(breadScale) * 0.1f;
        score_Ferment = breadScale - 1.0f;

        Sequence sequence = DOTween.Sequence();

        if(score_Ferment < 1.0f)
        {
            Debug.Log("Too Fast!");
            comment.text = "Too Fast!";
            scoreTx.text = "C";
        }
        else if(score_Ferment == 1.0f)
        {
            Debug.Log("What!? Fooooooooo!!!!!");
            comment.text = "What!? Fooooooo!!!!";
            scoreTx.text = "S+";
        }
        else if (score_Ferment < 1.2f && score_Ferment > 1.0f)
        {
            Debug.Log("Amazing!!!");
            comment.text = "Amazing!!!";
            scoreTx.text = "S";
        }
        else if (score_Ferment >= 1.2f && score_Ferment < 1.5f)
        {
            Debug.Log("great!!");
            comment.text = "Great!!";
            scoreTx.text = "A";
        }
        else if (score_Ferment >= 1.5f)
        {
            Debug.Log("good!");
            comment.text = "Good!";
            scoreTx.text = "B";
        }
        scoreCanvas.SetActive(true);
        scCan.localScale = scoreSize;
        /*
        sequence.Append(scCan.transform.DOScale(1.2f, 1.2f)
            .SetEase(Ease.OutBack, 3.0f).SetDelay(1.5f)).SetLoops(-1, LoopType.Yoyo);*/

        scCan.transform.DOScale(1.2f, 1.2f)
            .SetEase(Ease.OutBack, 3.0f)
            .SetDelay(0.8f);

        _isFermentEnd = true;
    }

    // このオブジェクトが破棄される時に呼び出される。今回はシーン遷移直前の想定
    private void OnDestroy()
    {
        IsButtonAPrs = false;
        _gameMG.score_Ferment = scoreTx.text;
    }
}
