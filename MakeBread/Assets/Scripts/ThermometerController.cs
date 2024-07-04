using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemperatureFunc
{
    public class ThermometerController
    {
        /// <summary>
        /// 温度計の隠しオブジェクト左。右に移動すると温度が上がり、左に移動すると温度が下がる。
        /// </summary>
        private GameObject _leftObj;
        private float _leftX = 0.0f;
        private float _nowLeftX = 0.0f;

        /// <summary>
        /// 温度計の隠しオブジェクト左。動かない。
        /// </summary>
        private GameObject _rightObj;
        private float _rightX = 0.0f;

        /// <summary>
        /// 隠しオブジェクト同士の距離。複雑にならないよう、隠しオブジェクト左は初期位置x=0,右は初期位置x=18に置く。
        /// </summary>
        private float _thermoDistance = 0.0f;

        /// <summary>
        /// 温度の割合。隠しオブジェクト左の移動率。
        /// </summary>
        private float _thermoRate = 0.0f;

        /// <summary>
        /// 温度計の針の角度。
        /// </summary>
        private Vector3 _needleAngles = new Vector3();

        /// <summary>
        /// 隠しオブジェクト左の移動率から求めたZ軸の角度
        /// </summary>
        private float _needleAngleZ = 0.0f;

        /// <summary>
        /// 2回以上実行されないようにするためのカウント用
        /// </summary>
        private int _getDistanceCount = 0;

        /// <summary>
        /// 上昇する温度。
        /// </summary>
        public float upTemperature = 20.0f;


        /// <summary>
        /// 一度振った時に増える値を変更する
        /// </summary>
        /// <param name="newTemperature">新たに設定する温度。Float</param>
        public void ChangeUPTemperature(float newTemperature)
        {
            upTemperature = newTemperature;
        }

        /// <summary>
        /// 最初に一度実行すること。基準オブジェクト間の距離を計算する。
        /// </summary>
        public void GetThermoDistance()
        {
            if (_getDistanceCount > 1) return;
            _leftObj = GameObject.FindWithTag("LeftObj");
            _rightObj = GameObject.FindWithTag("RightObj");
            _leftX = _leftObj.transform.position.x;
            _rightX = _rightObj.transform.position.x;

            _thermoDistance = _rightX - _leftX;

            _getDistanceCount++;
        }

        /// <summary>
        /// 温度を上げるための関数。振られた時にLeftObjをupTemperature分右に移動させる
        /// </summary>
        public void TemperatureUP()
        {
            upTemperature = upTemperature * 0.1f;
            if (_leftObj.transform.position.x >= 18) return;
            for (int i = 0; i < upTemperature; i++)
            {
                _leftObj.transform.Translate(1.0f, 0, 0);
            }
        }

        /// <summary>
        /// 温度を下げるための関数。LeftObjを左に移動させる
        /// </summary>
        public void TemperatureDown()
        {
            _leftObj.transform.Translate(1.0f, 0, 0.0f, 0f);
        }

        /// <summary>
        /// 現在の割合(温度)を計算する
        /// </summary>
        public void TemperatureRate()
        {
            _nowLeftX = _leftObj.transform.position.x;
            _thermoRate = Mathf.Clamp01(_nowLeftX / _thermoDistance);
        }

        public float ReturnThermoRate()
        {
            return _thermoRate;
        }

        /// <summary>
        /// 温度計の針を回転させる値を計算する
        /// </summary>
        /// <returns>針のZ軸回転に代入する値</returns>
        public Vector3 RotateAngles()
        {
            _needleAngleZ = -180 * _thermoRate;
            _needleAngles.z = _needleAngleZ;

            return _needleAngles;
        }
    }
}