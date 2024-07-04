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

    private TasteManager _tasteMG = new TasteManager();
    private SceneNameMG _sceneNameMG = new SceneNameMG();

    private KeyCode[] _numbersKey = new KeyCode[]
    {
        KeyCode.Alpha1,KeyCode.Alpha2,
        KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,
        KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9
    };

    [SerializeField]private  static string _firstItem = "";
    private static int _lastItemTaste = 0;

    private string _nowSceneName = "";
    
   

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        //_nowSceneName = SceneManager.GetActiveScene().name;
        if(_nowSceneName == _sceneNameMG.gameSceneNames[2])
        {
            _thermoMeter.SetActive(true);
        }
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
            _firstItem = _breadinstantiate.ReturnFirstItemID();
            _lastItemTaste = _tasteMG.ReturnLastItemTaste();
            //SceneManager.LoadScene("OvenFire");

            
            StartCoroutine(LoadSceneAsync("OvenFire"));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("CookingPotBT");
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

        /*
        GameObject nextGM = GameObject.FindWithTag("GameManager");
        GameMG nextGameMG = nextGM.GetComponent<GameMG>();
        BTSerialManager nextBTSeriarlMG = nextGM.GetComponent<BTSerialManager>();


        //遷移後のシーンにあるImputMGに最初に選んだアイテムのIDと決定された味を渡す
        nextGameMG._firstItem = _breadinstantiate.ReturnFirstItemID();
        nextGameMG._lastItemTaste = _tasteMG.ReturnLastItemTaste();

        nextBTSeriarlMG.nowGameScene = nextScene.name;
        nextGameMG._nowSceneName = nextScene.name;
        */

    }
    

    public void SetBread(int number)
    {
        Debug.Log(breadData.Bread_date[number].name);
        _breadinstantiate.SummonBreadObj(breadData.Bread_date[number].id, breadData.Bread_date[number].taste);
    }

    
    public IEnumerator LoadSceneAsync (string sceneName)
    {
        _firstItem = _breadinstantiate.ReturnFirstItemID();
        _lastItemTaste = _tasteMG.ReturnLastItemTaste();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
}
