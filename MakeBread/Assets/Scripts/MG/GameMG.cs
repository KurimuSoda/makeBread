using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TasteMG;
using SceneMG.support;

public class GameMG : MonoBehaviour
{
    [SerializeField] private BreadDate breadData;
    [SerializeField] private BreadInstantiate _breadinstantiate;
    [SerializeField] private BTSerialManager _btSerialMG;
    [SerializeField] private GameObject _thermoMeter;
    [SerializeField] private GameObject _itemObj;
    [SerializeField] private SoundMG _soundMG;
    [SerializeField] private InitsMG _initMG;

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
    /// アイテム選択のシーンに置いてあるImageオブジェクトからのみ変更する
    /// </summary>
    public static bool isItemsObjExit = false;
    //public static bool isResultBreadActive = false;

    /// <summary>
    /// [3]に焼いたパンの結果を入れる。状態は普通、上等、焼きすぎの3種類
    /// </summary>
    private string[] breadStatuses = new string[4] { "Nomal", "Good", "OverCoocked", "" };

    private string _nowSceneName = "";

    private void Awake()
    {
        _initMG.AllInit();
        GameMGInit();
    }


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        //SceneManager.activeSceneChanged += ActiveSceneChanged;

        //_nowSceneName = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
           _breadinstantiate.DeleteBreadObj();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _breadinstantiate.SelectionFixing();
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(_nowSceneName == "CookingPotBT")
            {
                _firstItem = _breadinstantiate.ReturnSelectItemID(3);
                _lastItemTaste = _tasteMG.ReturnLastItemTaste();
            }
            
            //SceneManager.LoadScene("OvenFire");

            
            StartCoroutine(LoadSceneAsync("OvenFire"));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //SceneManager.LoadScene("CookingPotBT");
            StartCoroutine(LoadSceneAsync("CookingPotBT"));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(LoadSceneAsync("TitleScene"));
        }

        if (isItemsObjExit)
        {
            _breadinstantiate.GetItemsParent();
            isItemsObjExit = false;
        }
        
        

    }

    
    
    /// <summary>
    /// シーン遷移するときに次のシーンを読み込んだ後に実行される
    /// </summary>
    /// <param name="nextScene">遷移後のシーン</param>
    /// <param name="mode">シーンの読み込みモード</param>
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        
        _nowSceneName = nextScene.name;
        _btSerialMG.ModeChengeWithScene(nextScene.name);
        _soundMG.ChangeBGM(nextScene.name);
        ArrangementChangeEachScene();

        if(nextScene.name == "TitleScene")
        {
            _initMG.TitleInit();
            GameMGInit();
        }
            
    }
    /*
    private void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        if(thisScene.name == "CookingPotBT")
        {
            //_breadinstantiate.BreadInstantiateInit();
            for (int i = 1; i < 5; i++)
            {
                GameObject oldItem = GameObject.FindWithTag("Item" + i);
                Destroy(oldItem);
            }
        }
    }
    */

    public void SetBread(int number)
    {
        Debug.Log(breadData.Bread_date[number].name);
        _breadinstantiate.SummonBreadObj(breadData.Bread_date[number].id, breadData.Bread_date[number].taste);
    }

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

    public string SendBakeStatus()
    {
        return breadStatuses[3];
    }

    private void GameMGInit()
    {
        _firstItem = "";
        _lastItemTaste = 0;
        breadStatuses[3] = "";
        _nowSceneName = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// コルーチンなのでStartCoroutinを使って呼び出す
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsync (string sceneName)
    {
        /*
        //遷移後のシーンにあるImputMGに最初に選んだアイテムのIDと決定された味を渡す
        _firstItem = _breadinstantiate.ReturnFirstItemID();
        _lastItemTaste = _tasteMG.ReturnLastItemTaste();
        */
        if (_nowSceneName == "CookingPotBT")
        {
            _firstItem = _breadinstantiate.ReturnSelectItemID(3);
            _lastItemTaste = _tasteMG.ReturnLastItemTaste();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ArrangementChangeEachScene()
    {
        if (_nowSceneName == _sceneNameMG.gameSceneNames[0])    //TitleScene
        {
            _itemObj.SetActive(false);
            _thermoMeter.SetActive(false);
        }
        else if (_nowSceneName == _sceneNameMG.gameSceneNames[1])   //SelectItemScene
        {
            _thermoMeter.SetActive(false);
            _itemObj.SetActive(false);
        }
        else if (_nowSceneName == _sceneNameMG.gameSceneNames[2])   //OvenFireScene
        {
            _itemObj.SetActive(false);
            _thermoMeter.SetActive(true);
        }
        else if(_nowSceneName == _sceneNameMG.gameSceneNames[3])    //ResultScene
        {
            _itemObj.SetActive(false);
            _thermoMeter.SetActive(false);
        }
        
    }


}
