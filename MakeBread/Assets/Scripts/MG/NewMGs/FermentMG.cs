using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FermentMG : MonoBehaviour
{
    /// <summary>
    /// 生地オブジェクトの膨張をするかどうか
    /// </summary>
    public bool isBigStart = false;
    [SerializeField] private GameObject _BreadKiji;

    private float teas = 0.01f;
    [SerializeField] private float _time = 0.2f;

    [SerializeField] private float score_Ferment = 0.0f;

    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private RectTransform scCan;
    [SerializeField] private TextMeshProUGUI comment;
    [SerializeField] private TextMeshProUGUI scoreTx;

    private Vector3 scoreSize = new Vector3(1.0f, 1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        scoreCanvas.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        scCan.localScale = scoreSize;
        scoreCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scoreCanvas.SetActive(false);
            scCan.transform.DOKill();
            BreadAnimStart();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreadAnimKill();
        }
       
    }

    private void BreadAnimStart()
    {
        _BreadKiji.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        _time = Random.Range(1.0f, 3.1f);
        teas = Random.Range(2.0f, 3.6f) * 10;
        teas = Mathf.Floor(teas) * 0.1f;
        _BreadKiji.transform.DOScale(new Vector3(teas, teas, teas), _time)
            .SetEase(Ease.OutSine);
    }

    private void BreadAnimKill()
    {
        _BreadKiji.transform.DOKill();
        FermentJadge();
    }

    private void FermentJadge()
    {
        float breadScale = 1.0f;
        breadScale = _BreadKiji.transform.localScale.x * 10;
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

    }
}
