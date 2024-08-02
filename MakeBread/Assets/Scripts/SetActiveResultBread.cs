using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveResultBread : MonoBehaviour
{
    //TODO change MG script Do!

    /// <summary>
    /// リザルトに表示するパンのID
    /// </summary>
    [SerializeField]private string _firstItemID = "";
    private GameMG_new _gameMG;

    /// <summary>
    /// リザルトに表示するパンのオブジェクト。オブジェクトの名前は小文字＋番号
    /// </summary>
    [SerializeField]private GameObject _resultBread;

    
    // Start is called before the first frame update
    void Start()
    {
        _firstItemID = "";

        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
        //GameMGからリザルトに表示するパンのIDを受け取る
        _firstItemID = _gameMG.SendItemID();

        //_firstItemID = "spi_01";  //for Debug

        if(_firstItemID == "")  //IDと同じ名前のオブジェクトが見つからなかったとき
        {
            _resultBread = transform.Find("FishBread").gameObject;
            //Debug.Log("result bread dont find...");
        }
        else
        {
            _resultBread = transform.Find(_firstItemID).gameObject;
        }
        
        _resultBread.SetActive(true);
    }

}
