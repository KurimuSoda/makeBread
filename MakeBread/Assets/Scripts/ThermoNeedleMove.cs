using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TemperatureFunc;

public class ThermoNeedleMove : MonoBehaviour
{
    private ThermometerController _thermoCon = new ThermometerController();
    private Vector3 _needleAngles;
    private Vector3 _motionAngles = new Vector3(0.0f, 0.0f, -0.5f);
    private GameObject _needleObj;
    private int _temperature = 0;
    private int _angelZMoveCount = 0;
    private bool _isAnimation = false;
    private float _nowNeedleAngle = 0.0f;

    
    public void Start()
    {
        _needleObj = this.gameObject;
        _thermoCon.GetThermoDistance();
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
        TemperatureJadge();


        //_needleObj.transform.eulerAngles = _needleAngles;
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

    private void TemperatureJadge()
    {
        //float nowRotZ = _needleObj.transform.localEulerAngles.z;
        float rotRate = _thermoCon.ReturnThermoRate();
        if(rotRate >= 0.7f && rotRate < 0.85f)
        {
            Debug.Log("Good!");
        }
        else if(rotRate >= 0.85f)
        {
            Debug.Log("Too Hot!!");
        }
    }
}
