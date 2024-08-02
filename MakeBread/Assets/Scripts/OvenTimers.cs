using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OvenTimers : MonoBehaviour
{
    private GameMG_new _gameMG;
    public static bool isGoodTemperature = false;
    public static bool isTooHot = false;

    public float goodTime = 0.0f;
    public float tooHotTime = 0.0f;
    private string _beadStatus = "";

    private bool _isTimeUp = false;
    private float timer = 0.0f;
    private int _timeUpCount = 0;

    private int _timerInt = 30;
    [SerializeField] private TextMeshProUGUI _timerText;
    // Start is called before the first frame update
    void Start()
    {
        OvenTimerInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimeUp)
        {
            if (_timeUpCount > 1) return;
            JadgeBreadStatus();
            _gameMG.BreadStatusPutArray(_beadStatus);
            StartCoroutine("GoToResult");
            Debug.Log("Kansei!");
            _timeUpCount++;
        }
        else if(_isTimeUp == false)
        {
            HalfMinutesTimer();
        }
        if (isGoodTemperature)
        {
            goodTime += Time.deltaTime;
            
        }
        else if (isTooHot)
        {
            tooHotTime += Time.deltaTime;
        }
        
        
    }

    public void OvenTimerInit()
    {
        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
        goodTime = 0.0f;
        tooHotTime = 0.0f;
        _beadStatus = "";
        _isTimeUp = false;
        _timeUpCount = 0;
        timer = 0.0f;
    }

    /// <summary>
    /// 30s Timer
    /// </summary>
    private void HalfMinutesTimer() {
        timer += Time.deltaTime;

        //小数点切り捨てでカウントダウン
        _timerInt = 30 - Mathf.FloorToInt(timer);
        _timerText.text = _timerInt.ToString();

        if(timer >= 30.0f)
        {
            Debug.Log("TimeUp!");
            timer = 0.0f;
            _isTimeUp = true;
        }
        else
        {
            _isTimeUp = false;
        }

    }

    private IEnumerator GoToResult()
    {
        yield return new WaitForSeconds(3.0f);
        //Debug.Log("BreadStatus is --> " + _beadStatus);
        StartCoroutine(_gameMG.LoadSceneAsync("ResultScene"));
    }

    private void JadgeBreadStatus()
    {
        if(tooHotTime >= 8.0f)
        {
            _beadStatus = "OverCoocked";
        }
        else if (goodTime >= 15.0f && tooHotTime < 8.0f)
        {
            _beadStatus = "Good";
        }
        else if(goodTime < 15.0f && tooHotTime < 8.0f)
        {
            _beadStatus = "Nomal";
        }

    }


}
