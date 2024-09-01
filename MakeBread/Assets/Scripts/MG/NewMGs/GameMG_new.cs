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
    //[SerializeField] private InitsMG _initMG;

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
    
    /// <summary>
    /// 選んだアイテムの中からランダムで一つを選ぶ(string ID)
    /// </summary>
    public  static string _FirstItem = "";
    /// <summary>
    /// ランダムで番号を入れる
    /// </summary>
    public static int _RandomItem = 0;
    private static int _lastItemTaste = 0;

    public int _countImput = 0;

    private int _inputUPLimit = 4;
    private int _inputLowerLimit = 0;

    /// <summary>
    /// アイテム選択のシーンに置いてあるImageオブジェクトからのみ変更する。アイテム画像を生成するための親オブジェクトを取得するフラグを立てる用。
    /// </summary>
    public static bool isItemsObjExit = false;
    //public static bool isResultBreadActive = false;

    public string score_Ferment = "C";

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
        
        if (Input.GetKeyDown(KeyCode.Backspace) || ItemSelectMG.IsShaked)
        {
            if (nowSceneName != "CookingPotBT") return;
            _breadInstantiate.DeleteBreadObj();
            _countImput--;
            ItemSelectMG.IsShaked = false;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _breadInstantiate.SelectionFixing();
            _tasteMG.PushEnter();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */

        if (Input.GetKeyDown(KeyCode.R))
        {
            //_initMG.TitleInit();
            GameMGInit();
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
        return _FirstItem;
    }

    /// <summary>
    /// パンの焼き加減の状態を返す
    /// </summary>
    /// <returns></returns>
    public string SendBakeStatus()
    {
        return breadStatuses[3];
    }

    /// <summary>
    /// GMのInit、初期化
    /// </summary>
    private void GameMGInit()
    {
        _FirstItem = "";
        _lastItemTaste = 0;
        _RandomItem = 0;
        _countImput = 0;
        breadStatuses[3] = "";
        nowSceneName = SceneManager.GetActiveScene().name;

        _nfcChecks.NFCChecksInit();
        _breadInstantiate.BreadInstantiateInit();
    }

    
    /// <summary>
    /// コルーチンなのでStartCoroutinを使って呼び出す
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsync (string sceneName)
    {
        
        //遷移後のシーンにあるImputMGに最初に選んだアイテムのIDを渡す
        //_firstItem = _breadinstantiate.ReturnFirstItemID();
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    

    /// <summary>
    /// Sceneが開始された時に他のオブジェクトが呼ぶ。現在のSceneを教えてもらうための関数。
    /// </summary>
    /// <param name="thisSceneName">現在のSceneの名前</param>
    public void CollByChangeScene(string thisSceneName)
    {
        nowSceneName = thisSceneName;
        _btSerialMG.ChangeNowSceneName(thisSceneName);

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
    /// タイトル画面から次の画面に移動する時に実行する
    /// </summary>
    public void TitleFinish()
    {
        StartCoroutine(LoadSceneAsync("CookingPotBT"));
    }


    /// <summary>
    /// アイテムを選択し終わったあとに実行する
    /// </summary>
    public void ItemSelectFinish()
    {
        if(_countImput > 3) { _countImput = 3; }
        _tasteMG.PushEnter(_countImput);
        _lastItemTaste = _tasteMG.ReturnLastItemTaste();

        //何番目のアイテムをベースにするかランダムで選出し_FirstItemにIDを記録する
        ItemSelectMG.RandomItemChose(_countImput);
        _FirstItem = _breadInstantiate.ReturnSelectItemID(_RandomItem);

        StartCoroutine(LoadSceneAsync("OvenFire"));
    }

    public string ArrayNumTOItemID(int arrayNum)
    {
        string itemID = breadData.Bread_date[arrayNum].id;
        return itemID;
    }

    /// <summary>
    /// UIDをもとにアイテムを検索する。
    /// </summary>
    /// <param name="readuid">messageで送られてきたUIDを渡す</param>
    public void SearchItemDataUID(string readuid)
    {
        //if (_countImput > 3) return;
        //_btSerialMG._readStatus = ReadStatus.StopRead;
        int itemNo = _nfcChecks.UIDtoArrayNo(readuid);
        string itemID = breadData.Bread_date[itemNo].id;

        ItemSelectMG.PopUpItemArrNum = itemNo;
        ItemSelectMG.PopUpItemID = itemID;
    }

    /// <summary>
    /// UIDをもとにアイテムを検索し、画像付きオブジェクト生成の関数を実行する。
    /// </summary>
    /// <param name="readuid">messageで送られてきたUIDを渡す</param>
    public void ItemDataSend(string readuid)
    {
        if (_countImput > 3) return;
        //_btSerialMG._readStatus = ReadStatus.StopRead;
        int itemNo = _nfcChecks.UIDtoArrayNo(readuid);
        SetBread(itemNo);

        _countImput++;
        _countImput = Mathf.Clamp(_countImput, _inputLowerLimit, _inputUPLimit);
    }


    /// <summary>
    /// 配列番号を引数に受け取り該当するアイテムのオブジェクトを生成する
    /// </summary>
    /// <param name="number">アイテムの配列番号</param>
    public void SetBread(int number)
    {
        Debug.Log(breadData.Bread_date[number].name);
        _breadInstantiate.SummonBreadObj(breadData.Bread_date[number].id, breadData.Bread_date[number].taste);
        //_btSerialMG._readStatus = ReadStatus.ReadOK;
    }



}
