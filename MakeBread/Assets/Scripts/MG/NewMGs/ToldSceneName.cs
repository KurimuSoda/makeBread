using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneMG.support;

public class ToldSceneName : MonoBehaviour
{
    //現在のSceneを取得しGameMG&BTSerialMGに伝えるためのスクリプト。Scene内に一つだけ設置する。

    private GameMG_new _gameMG;
    private BTSerialManager_new _BTSerialMG;
    private GameObject _managerObj;

    private string _thisSceneName = "";
    private SceneNames _thisScene = SceneNames.TitleScene;
    private void Start()
    {
        _managerObj = GameObject.Find("Manager");
        _gameMG = _managerObj.GetComponent<GameMG_new>();
        _thisSceneName = SceneManager.GetActiveScene().name;
        _gameMG.CollByChangeScene(_thisSceneName);

        _BTSerialMG = _managerObj.GetComponent<BTSerialManager_new>();

        if(_thisSceneName == "TitleScene")
        {
            _thisScene = SceneNames.TitleScene;
        }
        else if(_thisSceneName == "CookingPotBT")
        {
            _thisScene = SceneNames.CookingPotBT;
        }
        else if(_thisSceneName == "OvenFire")
        {
            _thisScene = SceneNames.OvenFire;
        }
        else if(_thisSceneName == "ResultScene")
        {
            _thisScene = SceneNames.ResultScene;
        }

        _BTSerialMG._nowScene = _thisScene;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            switch (_thisSceneName)
            {
                case "TitleScene":
                    SceneManager.LoadScene("CookingPotBT");
                    break;

                case "CookingPotBT":
                    SceneManager.LoadScene("OvenFire");
                    break;

                case "OvenFire":
                    SceneManager.LoadScene("ResultScene");
                    break;

                case "ResultScene":
                    SceneManager.LoadScene("TitleScene");
                    break;
            }
        }
    }
}
