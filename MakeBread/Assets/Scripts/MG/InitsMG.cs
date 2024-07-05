using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TemperatureFunc;



public class InitsMG : MonoBehaviour
{
    [SerializeField] private NFCChecks _nfcChecks;
    [SerializeField] private ThermoNeedleMove _thremoNeedle;
    [SerializeField] private SoundMG _soundMG;
    private ThermometerController _thermoCon = new ThermometerController();

    /// <summary>
    /// 各スクリプトの初期化を集めた関数。
    /// </summary>
    public void AllInit()
    {
        _nfcChecks.NFCChecksInit();
        _soundMG.BGMInit();
        _thermoCon.ThermoConObjInit(true);
        _thremoNeedle.ThermoNeedleInit(true);
    }
}

