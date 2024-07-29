using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TasteMG;
using SceneMG.support;

//実質的なImputManager
public class GameMG_new : MonoBehaviour
{
    [SerializeField] private BreadDate breadData;
    [SerializeField] private BreadInstantiate _breadInstantiate;
    [SerializeField] private BTSerialManager_new _btSerialMG;

    /// <summary>
    /// NFC関係の処理を行う用の関数を集めたスクリプト
    /// </summary>
    [SerializeField] private NFCChecks _nfcChecks;

    //[SerializeField] private GameObject _thermoMeter;
    //[SerializeField] private GameObject _itemObj;

    /// <summary>
    /// BGMなどを管理するスクリプト
    /// </summary>
    //[SerializeField] private SoundMG _soundMG;
    /// <summary>
    /// 初期化用関数をまとめたMG
    /// </summary>
    //[SerializeField] private InitsMG _initMG;

    private TasteManager _tasteMG = new TasteManager();
    private SceneNameMG _sceneNameMG = new SceneNameMG();
    
    
    private KeyCode[] _numbersKey = new KeyCode[]
    {
        KeyCode.Alpha1,KeyCode.Alpha2,
        KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,
        KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9
    };

    public  static string _firstItem = "";
    private static int _lastItemTaste = 0;

    /// <summary>
    /// アイテム選択のシーンに置いてあるImageオブジェクトからのみ変更する。アイテム画像を生成するための親オブジェクトを取得するフラグを立てる用。
    /// </summary>
    public static bool isItemsObjExit = false;
    //public static bool isResultBreadActive = false;

    /// <summary>
    /// [3]に焼いたパンの結果を入れる。状態は普通、上等、焼きすぎの3種類
    /// </summary>
    private string[] breadStatuses = new string[4] { "Nomal", "Good", "OverCoocked", "" };

    public string nowSceneName = "";

    private void Awake()
    {
        //_initMG.AllInit();
        GameMGInit();
    }


    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.sceneLoaded += SceneLoaded;
        /*
        //SceneManager.activeSceneChanged += ActiveSceneChanged;

        //_nowSceneName = SceneManager.GetActiveScene().name;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
           _breadInstantiate.DeleteBreadObj();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _breadInstantiate.SelectionFixing();
            _tasteMG.PushEnter();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */

        
        if (Input.anyKeyDown)
        {
            for(int i = 0; i < _numbersKey.Length; i++)
            {
                if (Input.GetKeyDown(_numbersKey[i]))
                {
                    SetBread(i);
                }
            }
        }
        

        if (isItemsObjExit)
        {
            _breadInstantiate.GetItemsParent();
            isItemsObjExit = false;
        }
        
    }

    
    /*
    /// <summary>
    /// シーン遷移するときに次のシーンを読み込んだ後に実行される
    /// </summary>
    /// <param name="nextScene">遷移後のシーン</param>
    /// <param name="mode">シーンの読み込みモード</param>
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        
        nowSceneName = nextScene.name;
        _btSerialMG.ModeChengeWithScene(nextScene.name);
        _soundMG.ChangeBGM(nextScene.name);
        ArrangementChangeEachScene();

        if(nextScene.name == "TitleScene")
        {
            _initMG.TitleInit();
            GameMGInit();
        }
            
    }
    */

    
    /// <summary>
    /// パンの状態を受け取って、配列に格納する
    /// </summary>
    /// <param name="breadStatus">Nomal or Good or OverCoockedのどれかを文字列で渡す</param>
    public void BreadStatusPutArray(string breadStatus)
    {
        breadStatuses[3] = breadStatus;
    }

    /// <summary>
    /// リザルトのパンを表示するのに使うIDを返す
    /// </summary>
    /// <returns>最初の素材のID</returns>
    public string SendItemID()
    {
        return _firstItem;
    }

    /// <summary>
    /// パンの焼き加減の状態を返す
    /// </summary>
    /// <returns></returns>
    public string SendBakeStatus()
    {
        return breadStatuses[3];
    }

    private void GameMGInit()
    {
        _firstItem = "";
        _lastItemTaste = 0;
        breadStatuses[3] = "";
        nowSceneName = SceneManager.GetActiveScene().name;
    }

    
    /// <summary>
    /// コルーチンなのでStartCoroutinを使って呼び出す
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsync (string sceneName)
    {
        
        //遷移後のシーンにあるImputMGに最初に選んだアイテムのIDと決定された味を渡す
        //_firstItem = _breadinstantiate.ReturnFirstItemID();
        //_lastItemTaste = _tasteMG.ReturnLastItemTaste();
        
        if (nowSceneName == "CookingPotBT")
        {
            _firstItem = _breadInstantiate.ReturnFirstItemID();
            _lastItemTaste = _tasteMG.ReturnLastItemTaste();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    /*
    public void ArrangementChangeEachScene()
    {
        if (nowSceneName == _sceneNameMG.gameSceneNames[0])    //TitleScene
        {
            _itemObj.SetActive(false);
            _thermoMeter.SetActive(false);
        }
        else if (nowSceneName == _sceneNameMG.gameSceneNames[1])   //SelectItemScene
        {
            _thermoMeter.SetActive(false);
            _itemObj.SetActive(false);
        }
        else if (nowSceneName == _sceneNameMG.gameSceneNames[2])   //OvenFireScene
        {
            _itemObj.SetActive(false);
            _thermoMeter.SetActive(true);
        }
        else if(nowSceneName == _sceneNameMG.gameSceneNames[3])    //ResultScene
        {
            _itemObj.SetActive(false);
            _thermoMeter.SetActive(false);
        }
        
    }*/

    /// <summary>
    /// Sceneが開始された時に他のオブジェクトが呼ぶ。現在のSceneを教えてもらうための関数。
    /// </summary>
    /// <param name="thisSceneName">現在のSceneの名前</param>
    public void CollByChangeScene(string thisSceneName)
    {
        nowSceneName = thisSceneName;

        if(nowSceneName == _sceneNameMG.gameSceneNames[1])  //SelectItemScene
        {
            _btSerialMG._readStatus = ReadStatus.ReadOK;
        }
        else
        {
            _btSerialMG._readStatus = ReadStatus.StopRead;

            if (nowSceneName == "TitleScene")
            {
                //_initMG.TitleInit();
                GameMGInit();
            }
        }
    }

    /// <summary>
    /// アイテムを選択し終わったあとに実行する
    /// </summary>
    public void ItemSelectFinish()
    {
        _tasteMG.PushEnter();
        _lastItemTaste = _tasteMG.ReturnLastItemTaste();
        //選んだアイテムからランダムでベースにするパンを決める関数を作る
    }


    /// <summary>
    /// UIDをもとにアイテムを検索し、画像付きオブジェクト生成の関数を実行する。
    /// </summary>
    /// <param name="readuid">messageで送られてきたUIDを渡す</param>
    public void ItemDataSend(string readuid)
    {
        //_btSerialMG._readStatus = ReadStatus.StopRead;
        int itemNo = _nfcChecks.UIDtoArrayNo(readuid);
        SetBread(itemNo);
        //_countImput++;
    }


    /// <summary>
    /// 配列番号を引数に受け取り該当するアイテムのオブジェクトを生成する
    /// </summary>
    /// <param name="number">アイテムの配列番号</param>
    private void SetBread(int number)
    {
        Debug.Log(breadData.Bread_date[number].name);
        _breadInstantiate.SummonBreadObj(breadData.Bread_date[number].id, breadData.Bread_date[number].taste);
        //_btSerialMG._readStatus = ReadStatus.ReadOK;
    }



}
