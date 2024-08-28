using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneMG : MonoBehaviour
{
    private GameMG_new _gameMG;
    public static bool IsShaked = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || IsShaked == true)
        {
            StartCoroutine(_gameMG.LoadSceneAsync("TitleScene"));
        }
    }
}
