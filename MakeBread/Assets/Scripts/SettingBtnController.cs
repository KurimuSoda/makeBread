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
    [SerializeField] private GameObject _conecctIcon;

    private string _inputKeep = "";
    private bool _isInputKeeping = false;
    private string _nowport = "";

    /// <summary>
    /// M5が繋がっているかどうか
    /// </summary>
    private bool _isM5Connect = false;

    /// <summary>
    /// 接続を確認する
    /// </summary>
    public void ConnctCheck()
    {
        _isM5Connect = GameMG_new.isM5Serial;
        if(_isM5Connect == true)
        {
            _conecctIcon.SetActive(true);
        }
        else if(_isM5Connect == false)
        {
            _conecctIcon.SetActive(false);
        }
    }

    /// <summary>
    /// 設定画面を表示する
    /// </summary>
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

    /// <summary>
    /// 設定画面を非表示
    /// </summary>
    public void NotActiveSettingPanel()
    {
        _settingPanel.SetActive(false);
    }

    /// <summary>
    /// ポート名の入力受け取り
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

    /// <summary>
    /// 入力されたポート名に接続する
    /// </summary>
    public void ConnectToNewPort()
    {
        if(_isInputKeeping == false) { return; }

        _settingAssist.ChangePortName(_inputKeep);
        _isM5Connect = GameMG_new.isM5Serial;
    }

    /// <summary>
    /// 現在のポート名をチェックする
    /// </summary>
    public void forCheckNowPortBtn()
    {
        _nowport = ReturnNowPortName();
        _nowportTx.text = "Now Port : " + _nowport;
        _isM5Connect = GameMG_new.isM5Serial;
    }
    
}
