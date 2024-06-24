using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSerialManager : MonoBehaviour
{
    public SerialHandler serialHandler;
    [SerializeField] private NFCChecks _nfcChecks;
    [SerializeField] private BreadInstantiate _breadInstantiate;
    [SerializeField] private BreadDate _breadData;

    /// <summary>
    /// デバッグしやすいようにキーボードでも選択できるようにしている
    /// </summary>
    [SerializeField] private ImputManager _imputMG;

    private ReadStatus _readStatus = ReadStatus.ReadOK;

    private string _oldMessage = "";
    public string readUid = "";
    
    // Start is called before the first frame update
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _oldMessage = "";
            _breadInstantiate.DeleteBreadObj();
        }
    }

    void OnDataReceived(string message)
    {
        if(_readStatus == ReadStatus.ReadOK)
        {
            if (_oldMessage == message) return;

            //送られてきた文字列を記録
            _oldMessage = message;

            //送られてきた文字列の後ろに"\r"がついてるので消す
            readUid = message.Replace("\r", "");
            
            ItemDataSend(readUid);

        }
        else if(_readStatus == ReadStatus.StopRead)
        {
            return;
        }
    }

    /// <summary>
    /// UIDをもとにアイテムを検索し、画像付きオブジェクト生成の関数を実行する。
    /// </summary>
    /// <param name="readuid">messageで送られてきたUIDを渡す</param>
    private void ItemDataSend(string readuid)
    {

        int itemNo = _nfcChecks.UIDtoArrayNo(readuid);
        SetBread(itemNo);
        
    }

    /// <summary>
    /// 配列番号を引数に受け取り該当するアイテムのオブジェクトを生成する
    /// </summary>
    /// <param name="number">アイテムの配列番号</param>
    public void SetBread(int number)
    {
        Debug.Log(_breadData.Bread_date[number].name);
        _breadInstantiate.SummonBreadObj(_breadData.Bread_date[number].id, _breadData.Bread_date[number].taste);
    }

}
