using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingBtnController : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private SettingAssist _settingAssist;
    [SerializeField] private TMP_InputField _inputFieldTMPr;

    private string _inputKeep = "";
    private bool _isInputKeeping = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SummonStteingObj()
    {
        Debug.Log("PPPPP");
        Instantiate(_gameObject, this.transform);
    }

    public void InputText()
    {
        _inputKeep = _inputFieldTMPr.text;
        Debug.Log("Keep string: " + _inputKeep);

        if(_inputKeep != "")
        {
            _isInputKeeping = true;
            Debug.Log("Input Keeping is --> " + _isInputKeeping);
        }
        else { return; }
    }

    /// <summary>
    /// 現在設定されているポート名を表示する
    /// </summary>
    public void ReturnNowPortName()
    {
        string portname = _settingAssist.SendNowPortName();

        Debug.Log(portname);
    }

    public void ConnectToNewPort()
    {
        if(_isInputKeeping == false) { return; }

        _settingAssist.ChangePortName(_inputKeep);
    }
    
}
