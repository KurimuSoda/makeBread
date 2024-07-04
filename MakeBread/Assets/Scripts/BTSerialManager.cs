using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TasteMG;
using TemperatureFunc;
using UnityEngine.SceneManagement;
using SceneMG.support;


public class BTSerialManager : MonoBehaviour
{
    public SerialHandler serialHandler;
    [SerializeField ]private NFCChecks _nfcChecks;
    [SerializeField] private BreadInstantiate _breadInstantiate;
    [SerializeField] private BreadDate _breadData;
    
    private TasteManager _tastMG = new TasteManager();
    private ThermometerController _thermometerCon = new ThermometerController();
    private SceneNameMG _sceneNameMG = new SceneNameMG();

    /// <summary>
    /// デバッグしやすいようにキーボードでも選択できるようにしている
    /// </summary>
    [SerializeField] private GameMG _imputMG;

    [SerializeField]private ReadStatus _readStatus = ReadStatus.ReadOK;

    public string nowGameScene = "TitleScene";
    private SceneNames _nowScene;
    
    private string[] _oldMessage = new string[5] { "", "", "", "", ""}; //あまり意味がないかも
    public string readUid = "";

    /// <summary>
    /// Null検知用
    /// </summary>
    private string _strNull = "\r";
    private int _countImput = 1;

    //public bool isEnter = false;
    private string _enterNFC = "04D1BEAF790000\r";  //Enter(Return)NFC UID
    private string _backSpaceNFC = "048E69B2790000\r";  //BackSpaceNFC UID
    private string _strShaked = "Shaked\r";

    //複数回送られてくるシリアルデータの1回目を判別する用。もしかしたら同じもの連続で読めるようになるかも。
    private int _receiveStrCount = 0;

   
    // Start is called before the first frame update
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;

        /*
        nowGameScene = SceneManager.GetActiveScene().name;
        if(nowGameScene == _sceneNameMG.gameSceneNames[1])
        {
            _nowScene = SceneNames.CookingPotBT;
            _readStatus = ReadStatus.ReadOK;
        }
        else if(nowGameScene == _sceneNameMG.gameSceneNames[2])
        {
            _nowScene = SceneNames.OvenFire;
            _readStatus = ReadStatus.StopRead;
            _thermometerCon.GetThermoDistance();
        }
        else if(nowGameScene == _sceneNameMG.gameSceneNames[3])
        {
            _nowScene = SceneNames.ResultScene;
            _readStatus = ReadStatus.ReadOK;
        }
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _breadInstantiate.DeleteBreadObj();
            _countImput--;
        }
    }

    /// <summary>
    /// シリアルメッセージを確認する
    /// </summary>
    /// <param name="message">送られてきたメッセージ</param>
    void OnDataReceived(string message)
    {
        if(_readStatus == ReadStatus.ReadOK)
        {
            
            if (_strNull == message || _oldMessage[0] == message) return;

            //同じものが連続して送られてくるため、2回目以降を弾く用。同じものを連続して読み取れない
            _oldMessage[0] = message;
            
            if (message == _enterNFC)
            {
                _tastMG.PushEnter();
                _breadInstantiate.SelectionFixing();
                _readStatus = ReadStatus.StopRead;
                _countImput = 0;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //Debug.Log("Enter was received!");
                return;
            }
            else if (message == _backSpaceNFC)
            {
                PutBackSpace();
                return;
            }

            if(_nowScene == SceneNames.CookingPotBT)
            {
                if (_countImput > 4) return;

                //送られてきた文字列を記録
                _oldMessage[_countImput] = message;
                //_oldMessage[0] = message;

                //送られてきた文字列の後ろに"\r"がついてるので消す
                readUid = message.Replace("\r", "");

                ItemDataSend(readUid);
            }
            

        }
        else if(_readStatus == ReadStatus.StopRead)
        {
            if (_strNull == message)    //Null = "" が送られてきたとき
            {
                _receiveStrCount = 0;
                return;
            }

            if(_strShaked == message)
            {
                _receiveStrCount++;

                if (_receiveStrCount > 2) return;

                Debug.Log("Shaked!!");
                M5Shaked();
                
            }

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
        _countImput++;
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

    private void M5Shaked()
    {
        _thermometerCon.TemperatureUP();
    }

    /// <summary>
    /// NFCでBackSpaceの代わりをするための関数。最新のアイテムを1つ消す。2回連続では使えない。
    /// </summary>
    private void PutBackSpace()
    {
        _breadInstantiate.DeleteBreadObj();
        _countImput--;
    }


    public void ModeChengeWithScene(string nextScene)
    {
        if (nextScene == _sceneNameMG.gameSceneNames[0])
        {
            _nfcChecks.ItemDataLengthCount();
        }
        else if (nextScene == _sceneNameMG.gameSceneNames[1])
        {
            _nowScene = SceneNames.CookingPotBT;
            _readStatus = ReadStatus.ReadOK;
        }
        else if (nextScene == _sceneNameMG.gameSceneNames[2])
        {
            _nowScene = SceneNames.OvenFire;
            _readStatus = ReadStatus.StopRead;
            _thermometerCon.GetThermoDistance();
        }
        else if (nextScene == _sceneNameMG.gameSceneNames[3])
        {
            _nowScene = SceneNames.ResultScene;
            _readStatus = ReadStatus.ReadOK;
        }
    }
}
