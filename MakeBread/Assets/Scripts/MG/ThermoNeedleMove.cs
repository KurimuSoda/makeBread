using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TemperatureFunc;

public class ThermoNeedleMove : MonoBehaviour
{
    private ThermometerController _thermoCon = new ThermometerController();
    private Vector3 _needleAngles;
    private Vector3 _motionAngles = new Vector3(0.0f, 0.0f, -0.5f);
    private Vector3 _downMotionAngles = new Vector3(0.0f, 0.0f, 0.2f);
    private GameObject _needleObj;
    private int _temperature = 0;
    private int _angelZMoveCount = 0;
    private bool _isAnimation = false;
    
    private float _nowNeedleAngle = 0.0f;

    /*
    private void Awake()
    {
        _thermoCon.GetThermoDistance();
        
    }
    */

    public void Start()
    {
        //_needleObj = this.gameObject;
        //_thermoCon.GetThermoDistance();
        _temperature = (int)_thermoCon.upTemperature * 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAnimation)
        {
            NeedleUPMotion();
        }
    }
    private void FixedUpdate()
    {
        

        _thermoCon.TemperatureRate();
        _needleAngles = _thermoCon.RotateAngles();
        if(_needleAngles.z != _nowNeedleAngle)
        {
            _nowNeedleAngle = _needleAngles.z;
            _isAnimation = true;
        }
        /*
        if (_isAnimation == false)
        {
            NeedleDownMotion();
        }
        */

        TemperatureJadge();
        


        //_needleObj.transform.eulerAngles = _needleAngles;
    }

    /// <summary>
    /// 温度計の針の初期化と、温度計算用オブジェクトの距離を計算する関数を呼び出す関数。ALLの場合はアタッチしたオブジェクトも取得する。
    /// </summary>
    /// <param name="isAllInit">アタッチしているオブジェクトを取得するかどうか</param>
    public void ThermoNeedleInit(bool isAllInit)
    {
        if (isAllInit)
        {
            //オブジェクトを取得
            _needleObj = this.gameObject;
        }
        _thermoCon.GetThermoDistance();

        //針の回転を初期化
        _needleObj.transform.eulerAngles = new Vector3(21.0f, 0.0f, 0.0f);

    }

    private void NeedleUPMotion()
    {
        
        if (_angelZMoveCount >= _temperature)
        {
            _angelZMoveCount = 0;
            
            _isAnimation = false;
            return;
        }

        _needleObj.transform.Rotate(_motionAngles);
        _angelZMoveCount++;

        
    }

    private void NeedleDownMotion()
    {
        _thermoCon.TemperatureDown();
        
        _needleObj.transform.Rotate(_downMotionAngles);
        

    }

    private void TemperatureJadge()
    {
        //float nowRotZ = _needleObj.transform.localEulerAngles.z;
        float rotRate = _thermoCon.ReturnThermoRate();
        if(rotRate < 0.7f)
        {
            if(OvenTimers.isGoodTemperature == true || OvenTimers.isTooHot == true)
            {
                OvenTimers.isGoodTemperature = false;
                OvenTimers.isTooHot = false;
            }
        }
        if(rotRate >= 0.7f && rotRate < 0.85f)
        {
            if(OvenTimers.isGoodTemperature == false)
            {
                Debug.Log("Good!");
                OvenTimers.isTooHot = false;
                OvenTimers.isGoodTemperature = true;
            }
            
        }
        else if(rotRate >= 0.85f)
        {
            
            if(OvenTimers.isTooHot == false)
            {
                Debug.Log("Too Hot!!");
                OvenTimers.isGoodTemperature = false;
                OvenTimers.isTooHot = true;
            }
            
        }
    }
}
