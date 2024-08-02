using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveResultBread : MonoBehaviour
{
    //TODO change MG script Do!
    [SerializeField]private string _firstItemID = "";
    private GameMG_new _gameMG;
    [SerializeField]private GameObject _resultBread;

    
    // Start is called before the first frame update
    void Start()
    {
        _firstItemID = "";

        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
        _firstItemID = _gameMG.SendItemID();

        //_firstItemID = "spi_01";  //for Debug

        if(_firstItemID == "")
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
