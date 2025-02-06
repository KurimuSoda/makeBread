using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OvenTimers : MonoBehaviour
{
    private GameMG_new _gameMG;
    [SerializeField] private OvenMG _ovenMG;
    [SerializeField] private GameObject _finishPanel;
    //public static bool isGoodTemperature = false;
    //public static bool isTooHot = false;

    
    //public float coldTime = 10.0f;
    //public float tooHotTime = 8.0f;
    

    /// <summary>
    /// string Status
    /// </summary>
    private string _breadStatus = "";

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
            _isTimeUp = false;
            _finishPanel.SetActive(true);
            _breadStatus = _ovenMG.JadgeBreadStatus();
            _gameMG.BreadStatusPutArray(_breadStatus);
            StartCoroutine("GoToResult");
            Debug.Log("Kansei!");
            _timeUpCount++;
        }
        else if(_ovenMG._isGameStart == true)
        {
            HalfMinutesTimer();
        }
        /*
        if (isGoodTemperature)
        {
            coldTime += Time.deltaTime;
            
        }
        else if (isTooHot)
        {
            tooHotTime += Time.deltaTime;
        }
        */
        
    }

    public void OvenTimerInit()
    {
        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
        //coldTime = 0.0f;
        //tooHotTime = 0.0f;
        _breadStatus = "";
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

    

}
