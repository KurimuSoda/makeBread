using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMG : MonoBehaviour
{
    private GameMG_new _gameMG;

    public static bool IsShaked = false;
    // Start is called before the first frame update
    void Start()
    {
        _gameMG = GameObject.Find("Manager").GetComponent<GameMG_new>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || IsShaked == true)
        {
            _gameMG.TitleFinish();
            IsShaked = false;
        }
    }


}
