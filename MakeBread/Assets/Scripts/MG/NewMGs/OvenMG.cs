using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenMG : MonoBehaviour
{
    public static bool IsShaked = false;

    [SerializeField] private GameObject _fireObj;
    [SerializeField] private GameObject _efectObj;

    private Vector3 _fireObjTrans = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _addScale = 0.1f;
    [SerializeField] private float _downScale = 0.01f;

    private bool _isGameStart = false;

    //火力強すぎのライン
    private float _hotLine = 1.7f;
    /// <summary>
    /// 火力強すぎの時の時間の合計
    /// </summary>
    [SerializeField]private float _overCookCount = 0.0f;

    //火力弱すぎのライン
    private float _coldLine = 0.6f;
    /// <summary>
    /// 火力弱すぎの時の時間の合計
    /// </summary>
    [SerializeField]private float _coldCookCount = 0.0f;

    /// <summary>
    /// 生焼けになる時間。この時間以上火力弱すぎが続くと生焼け
    /// </summary>
    private float _coldTime = 10.0f;

    /// <summary>
    /// 焼きすぎになる時間。この時間以上火力強すぎが続くと黒焦げ
    /// </summary>
    private float _tooHotTime = 8.0f;

    private float _goodLineUpper = 1.7f;
    private float _goodLineLower = 1.3f;
    private float _goodCookCount = 0.0f;

    private float _upperLimit = 1.9f;
    private float _lowerLimit = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        IsShaked = false;
        _efectObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameStart == false) return;

        //M5からShakeが送られてきたらBTSerialMGでtrueにする。trueの時に行う処理
        if (IsShaked || Input.GetKeyDown(KeyCode.S))
        {
            FireObjAddScale();
            IsShaked = false;
        }

        ZoneJadge();
    }

    private void OvenGameStart()
    {
        _isGameStart = true;
    }

    private void FixedUpdate()
    {
        FireObjDownScale();
    }

    public void OvenMGInit()
    {
        IsShaked = false;
        _fireObjTrans = new Vector3(0f, 0f, 0f);
        _hotLine = 1.7f;
        _overCookCount = 0.0f;
        _coldLine = 0.6f;
        _coldCookCount = 0.0f;
        _coldTime = 10.0f;
        _tooHotTime = 8.0f;
    }

    /// <summary>
    /// FIreObjのScaleを_addScale分上げる。大きくする。
    /// </summary>
    private void FireObjAddScale()
    {
        _fireObjTrans = _fireObj.transform.localScale;
        if (_fireObjTrans.x >= _upperLimit) return;

        _fireObjTrans.x = _fireObjTrans.x + _addScale;
        _fireObjTrans.y = _fireObjTrans.y + _addScale;
        _fireObj.transform.localScale = new Vector3(_fireObjTrans.x, _fireObjTrans.y, 0.0f);

    }

    /// <summary>
    /// FireObjのScaleを_downScale分下げる。小さくする。
    /// </summary>
    private void FireObjDownScale()
    {
        _fireObjTrans = _fireObj.transform.localScale;
        if (_fireObjTrans.x <= _lowerLimit) return;

        _fireObjTrans.x = _fireObjTrans.x - _downScale;
        _fireObjTrans.y = _fireObjTrans.y - _downScale;
        _fireObj.transform.localScale = new Vector3(_fireObjTrans.x, _fireObjTrans.y, 0.0f);
    }

    private void ZoneJadge()
    {
        if(_fireObj.transform.localScale.x >= _hotLine)
        {
            _overCookCount += Time.deltaTime;
            _efectObj.SetActive(false);
        }
        else if(_fireObj.transform.localScale.x <= _coldLine)
        {
            _coldCookCount += Time.deltaTime;
            _efectObj.SetActive(false);
        }
        else if(_fireObj.transform.localScale.x < _goodLineUpper && _fireObj.transform.localScale.x > _goodLineLower)
        {
            _goodCookCount += Time.deltaTime;
            _efectObj.SetActive(true);
        }
        
    }

    private void OnDestroy()
    {
        Debug.Log(JadgeBreadStatus());
        Debug.Log("hot time: " + _overCookCount + "cold time: " + _coldCookCount + "good time: " + _goodCookCount);
    }

    public string JadgeBreadStatus()
    {
        string _breadStatus = "";
        if (_overCookCount >= _tooHotTime)
        {
            _breadStatus = "OverCoocked";
        }
        else if (_coldCookCount >= _coldTime && _overCookCount < _tooHotTime)
        {
            _breadStatus = "Raw";
        }
        else if (_coldCookCount < _coldTime && _overCookCount < _tooHotTime)
        {
            if (_goodCookCount >= 10.0f)
            {
                _breadStatus = "Perfect";
            }
            else
            {
                _breadStatus = "Good";
            }
            
        }
        return _breadStatus;

    }
}
