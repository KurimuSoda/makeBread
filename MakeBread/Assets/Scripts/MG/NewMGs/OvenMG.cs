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

    //火力強すぎのライン
    private float _hotLine = 1.75f;
    private float _overCookCount = 0.0f;

    private float _coldLine = 0.5f;
    private float _rawCookCount = 0.0f;

    private float _coldTime = 10.0f;
    private float _tooHotTime = 8.0f;

    private float _upperLimit = 1.9f;
    private float _lowerLimit = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        IsShaked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //M5からShakeが送られてきたらBTSerialMGでtrueにする。trueの時に行う処理v
        if (IsShaked || Input.GetKeyDown(KeyCode.S))
        {
            FireObjAddScale();
            IsShaked = false;
        }

        BadZoneJadge();
    }

    private void FixedUpdate()
    {
        FireObjDownScale();
    }

    public void OvenMGInit()
    {
        IsShaked = false;
        _fireObjTrans = new Vector3(0f, 0f, 0f);
        _hotLine = 1.75f;
        _overCookCount = 0.0f;
        _coldLine = 0.5f;
        _rawCookCount = 0.0f;
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

    private void BadZoneJadge()
    {
        if(_fireObj.transform.localScale.x >= _hotLine)
        {
            _overCookCount += Time.deltaTime;
            _efectObj.SetActive(false);
        }
        else if(_fireObj.transform.localScale.x <= _coldLine)
        {
            _rawCookCount += Time.deltaTime;
            _efectObj.SetActive(false);
        }
        else
        {
            _efectObj.SetActive(true);
        }
    }

    public string JadgeBreadStatus()
    {
        string _breadStatus = "";
        if (_overCookCount >= _tooHotTime)
        {
            _breadStatus = "OverCoocked";
        }
        else if (_rawCookCount >= _coldTime || _overCookCount <= _tooHotTime)
        {
            _breadStatus = "Nomal";
        }
        else if (_rawCookCount > _coldTime && _overCookCount < _tooHotTime)
        {
            _breadStatus = "Good";
        }

        return _breadStatus;

    }
}
