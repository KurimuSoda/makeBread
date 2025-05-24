using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingAssist : MonoBehaviour
{
    private BTSerialManager_new _btserialMG;

    private string _nowPortName;


    private void Awake()
    {
        _btserialMG = GameObject.FindWithTag("GameManager").GetComponent<BTSerialManager_new>();
        _NowPortName();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 現在設定されているポート名を _nowPortName に入れる
    /// </summary>
    private void _NowPortName()
    {
        _nowPortName = _btserialMG.SendNowPortName();
    }

    public void ChangePortName(string newportname)
    {
        bool isWrited = false;
        isWrited = _btserialMG.isWritePortName(newportname);

        Debug.Log(isWrited);
    }

    /// <summary>
    /// 現在のポート名を返す
    /// </summary>
    /// <returns>現在のポート名</returns>
    public string SendNowPortName()
    {
        _NowPortName();

        string nowport = _nowPortName;

        return nowport;
    }

    //ポート名が取得できてるか確認用
    /* public void testPortNameCheck()
    {
        Debug.Log(_nowPortName);
    } */
}
