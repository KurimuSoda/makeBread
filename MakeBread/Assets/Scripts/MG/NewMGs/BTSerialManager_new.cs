using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TasteMG;
using TemperatureFunc;
using UnityEngine.SceneManagement;
using SceneMG.support;


public class BTSerialManager_new : MonoBehaviour
{
    public SerialHandler serialHandler;
    [SerializeField ]private NFCChecks _nfcChecks;
    /*
    [SerializeField] private BreadInstantiate _breadInstantiate;
    [SerializeField] private BreadDate _breadData;
    */
    [SerializeField] private GameMG_new _gameMG;
    
    private TasteManager _tastMG = new TasteManager();
    private ThermometerController _thermometerCon = new ThermometerController();
    private SceneNameMG _sceneNameMG = new SceneNameMG();

    
    //[SerializeField] private GameMG _imputMG;

    /// <summary>
    /// M5からのシリアル通信を受け取るかどうかの状態を入れる
    /// </summary>
    public ReadStatus  readStatus = ReadStatus.ReadOK;

    //private bool rfidReader = false;

    public string nowGameScene = "TitleScene";
    public SceneNames _nowScene;
    
    //private string[] _oldMessage = new string[5] { "", "", "", "", ""}; //あまり意味がないかも
    public string readUid = "";

    /// <summary>
    /// Null検知用
    /// </summary>
    private string _strNull = "\r";
    //private int _countImput = 1;

    //public bool isEnter = false;
    //private string _enterNFC = "04D1BEAF790000\r";  //Enter(Return)NFC UID
    //private string _backSpaceNFC = "048E69B2790000\r";  //BackSpaceNFC UID
    private string _strShaked = "Shaked\r";
    private string _strButtonA = "ButtonA\r";

    //複数回送られてくるシリアルデータの1回目を判別する用。もしかしたら同じもの連続で読めるようになるかも。
    private int _receiveStrCount = 0;

   
    // Start is called before the first frame update
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
        //_thermometerCon.GetThermoDistance();
        
    }


    /// <summary>
    /// シリアルメッセージを確認する
    /// </summary>
    /// <param name="message">送られてきたメッセージ</param>
    void OnDataReceived(string message)
    {
        //Read Serial
        if(readStatus == ReadStatus.ReadOK)
        {
            if (_strNull == message)    //Null = "" が送られてきたとき
            {
                _receiveStrCount = 0;
                return;
            }
            
            //Null = "" 以外が送られてきたとき
            _receiveStrCount++;
            if (_receiveStrCount > 1) return;

            if (_nowScene == SceneNames.CookingPotBT)
            {
                if(_strButtonA == message)
                {
                    ItemSelectMG.IsButtonAPrs = true;
                    return;
                }
                else if (_strShaked == message)
                {
                    M5Shaked();
                    //OvenMG.IsShaked = true;
                    return;
                }
                else
                {
                    //送られてきた文字列の後ろに"\r"がついてるので消す
                    readUid = message.Replace("\r", "");

                    _gameMG.SearchItemDataUID(readUid);
                }

                
            }
            

        }
        else if(readStatus == ReadStatus.StopRead)
        {
            if (_strNull == message)    //Null = "" が送られてきたとき
            {
                _receiveStrCount = 0;
                return;
            }

            if(_strButtonA == message)
            {
                M5ButtonAPrs();
                return;
            }

            if(_strShaked == message)
            {
                _receiveStrCount++;

                if (_receiveStrCount > 1) return;

                Debug.Log("Shaked!!");
                M5Shaked();
                
            }

            return;
        }
    }

    /// <summary>
    /// M5が振られた「shaked」が送られてきた時の処理
    /// </summary>
    private void M5Shaked()
    {
        if(_nowScene == SceneNames.OvenFire)
        {
            //振られたフラグを立てる
            OvenMG.IsShaked = true;
        }
        else if(_nowScene == SceneNames.TitleScene)
        {
            TitleMG.IsShaked = true;
        }
        else if (_nowScene == SceneNames.CookingPotBT)
        {
            ItemSelectMG.IsShaked = true;
        }
        else if(_nowScene == SceneNames.FermentScene)
        {
            FermentMG.IsShaked = true;
        }
        else if (_nowScene == SceneNames.ResultScene)
        {
            ResultSceneMG.IsShaked = true;
        }
    }

    /// <summary>
    /// M5のボタンを押された時の「ButtonA」が送られてきた時の処理
    /// </summary>
    private void M5ButtonAPrs()
    {
        if(_nowScene == SceneNames.OvenFire)
        {
            OvenMG.IsButtonAPrs = true;
        }
        else if(_nowScene == SceneNames.FermentScene)
        {
            FermentMG.IsButtonAPrs = true;
        }

    }

    /*
    /// <summary>
    /// NFCでBackSpaceの代わりをするための関数。最新のアイテムを1つ消す。2回連続では使えない。
    /// </summary>
    private void PutBackSpace()
    {
        _breadInstantiate.DeleteBreadObj();
        _countImput--;
    }
    */

    public void RFIDReader(bool isOn)
    {
        if(isOn == true)
        {
            //rfidReader = true;
            serialHandler.Write("rOn");
            serialHandler.Write("");
        }
        else if(isOn == false)
        {
            //rfidReader = false;
            serialHandler.Write("rOf");
            serialHandler.Write("");
        }
    }

    /// <summary>
    /// 現在 PortName に設定されているポート名を string で返す
    /// </summary>
    /// <returns>現在設定されているポート名</returns>
    public string SendNowPortName()
    {
        string ptNm = serialHandler.portName;
        
        return ptNm;
    }

    /// <summary>
    /// 設定したいポート名を受け取って書き換え、成功したらtrueを返し再接続を試みる
    /// </summary>
    /// <param name="NewPortName">新しいポート名</param>
    /// <returns>書き換え成功はtrue</returns>
    public bool isWritePortName(string NewPortName)
    {
        bool isWrite = false;
        string oldPoNm = serialHandler.portName;
        string newPoNm = NewPortName;
        
        if(newPoNm != oldPoNm)
        {
            //serialHandler.portName = newPoNm;
            serialHandler.ReOpen(newPoNm);
            isWrite = true;
        }
        else if(newPoNm == oldPoNm)
        {
            isWrite = false;
        }

        return isWrite;
    }

    public void ModeChengeWithScene(string nextScene)
    {
        if (nextScene == _sceneNameMG.gameSceneNames[0])
        {
            _nowScene = SceneNames.TitleScene;
            //_nfcChecks.ItemDataLengthCount();
        }
        else if (nextScene == _sceneNameMG.gameSceneNames[1])
        {
            _nowScene = SceneNames.CookingPotBT;
            //_readStatus = ReadStatus.ReadOK;

            
        }
        else if (nextScene == _sceneNameMG.gameSceneNames[2])
        {
            _nowScene = SceneNames.OvenFire;
            readStatus = ReadStatus.StopRead;
            //_thermometerCon.GetThermoDistance();

            
        }
        else if (nextScene == _sceneNameMG.gameSceneNames[3])
        {
            _nowScene = SceneNames.ResultScene;
            //_readStatus = ReadStatus.ReadOK;
        }
    }

    public void ChangeNowSceneName(string nowName)
    {
        if(nowName == "TitleScene")
        {
            _nowScene = SceneNames.TitleScene;
        }
        else if(nowName == "CookingPotBT")
        {
            _nowScene = SceneNames.CookingPotBT;
        }
        else if(nowName == "OvenFire")
        {
            _nowScene = SceneNames.OvenFire;
        }
        else if(nowName == "ResultScene")
        {
            _nowScene = SceneNames.ResultScene;
        }
    }
}
