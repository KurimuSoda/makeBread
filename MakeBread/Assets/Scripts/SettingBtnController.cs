using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingBtnController : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;
    [SerializeField, Tooltip("現在の接続状態を表示するText")] private TextMeshProUGUI _connectTx;
    [SerializeField, Tooltip("現在のポートを表示するText")] private TextMeshProUGUI _nowportTx;
    [SerializeField] private SettingAssist _settingAssist;
    [SerializeField] private TMP_InputField _inputFieldTMPr;

    private string _inputKeep = "";
    private bool _isInputKeeping = false;
    private string _nowport = "";

    private bool _isM5Connect = false;



    public void ActiveStteingPanel()
    {
        //Debug.Log("Setting Panel ON");
        _isM5Connect = GameMG_new.isM5Serial;
        _settingPanel.SetActive(true);
        if(_isM5Connect == true)
        {
            _connectTx.text = "Connecting Now!";
        }
        else if(_isM5Connect == false)
        {
            _connectTx.text = "Not Connect";
        }
    }

    public void NotActiveSettingPanel()
    {
        _settingPanel.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
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
    /// 現在設定されているポート名を返す
    /// </summary>
    /// <returns>now port name</returns>
    private string ReturnNowPortName()
    {
        string portname = _settingAssist.SendNowPortName();

        Debug.Log(portname);
        return portname;
    }

    public void ConnectToNewPort()
    {
        if(_isInputKeeping == false) { return; }

        _settingAssist.ChangePortName(_inputKeep);
        _isM5Connect = GameMG_new.isM5Serial;
    }

    /// <summary>
    /// 
    /// </summary>
    public void forCheckNowPortBtn()
    {
        _nowport = ReturnNowPortName();
        _nowportTx.text = "Now Port : " + _nowport;
        _isM5Connect = GameMG_new.isM5Serial;
    }
    
}
