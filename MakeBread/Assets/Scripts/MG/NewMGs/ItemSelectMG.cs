using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO 1度に2回設置してしまうのをなんとかする
//ItemSelectSceneに設置する
public class ItemSelectMG : MonoBehaviour
{
    private GameMG_new _gameMG;
    [SerializeField] private static int _randomBaseItem = 0;

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
            IsShaked = false;

            _gameMG.ItemSelectFinish();
            
        }
    }

    /// <summary>
    /// 入力した回数(選んだアイテム)の数を受け取って、その中からランダムに抽出した値をGMに渡す
    /// </summary>
    /// <param name="imputCount">入力した回数(選んだアイテム)の数</param>
    public static void RandomItemChose(int imputCount)
    {
        _randomBaseItem = 0;
        _randomBaseItem = Random.Range(0, imputCount);
        GameMG_new._RandomItem = _randomBaseItem;
        Debug.Log("Random ItemNumber is ---> " + _randomBaseItem);
    }
    
}
