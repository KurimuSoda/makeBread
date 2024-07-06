using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveResultBread : MonoBehaviour
{
    [SerializeField]private string _firstItemID = "";
    private GameMG _gameMG;
    [SerializeField]private GameObject _resultBread;

    
    // Start is called before the first frame update
    void Start()
    {
        _firstItemID = "";

        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG>();
        _firstItemID = _gameMG.SendItemID();

        
        _resultBread = transform.Find(_firstItemID).gameObject;
        if(_resultBread == null)
        {
            _resultBread = transform.Find("FishBread").gameObject;
        }
        _resultBread.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
